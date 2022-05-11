namespace HoleDesignation.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using CSharpFunctionalExtensions;
    using Enums;
    using Models;
    using Models.Parameters;
    using Result = CSharpFunctionalExtensions.Result;

    /// <summary>
    /// Сервис по установке семейств на места отверстий
    /// </summary>
    public class HoleDesignationService
    {
        private readonly UIDocument _uiDoc;
        private readonly ValidationService _validationService;
        private readonly GetElementService _getElementService;
        private readonly GeometryService _geometryService;
        private readonly Loger _loger;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="uiDocument">UIDocument</param>
        public HoleDesignationService(UIDocument uiDocument)
        {
            _uiDoc = uiDocument;
            _loger = new Loger();
            _validationService = new ValidationService(_uiDoc);
            _getElementService = new GetElementService(_uiDoc, _loger);
            _geometryService = new GeometryService(_uiDoc, _loger);
        }

        /// <summary>
        /// Устанавливает семейство УГО на отверстия
        /// </summary>
        /// <returns>Строку с отчетом</returns>
        public Result<string> Execute()
        {
            FamilySymbol roundFamilySymbol = null;
            FamilySymbol rectangleFamilySymbol = null;
            var edgeArrays = new List<EdgeArray>();
            var transactionGroup = new TransactionGroup(_uiDoc.Document, "Создание УГО");
            transactionGroup.Start();
            var result = _validationService.IsViewPlan()
                .Bind(() => _getElementService.GetPreSelectedFloor()
                    .OnFailureCompensate(() => _getElementService.SelectFloor())
                    .OnFailureCompensate(() => _getElementService.SelectFloorFromLinkedDoc()))

                .Bind(floor => _validationService.ValidateByContourCount(floor)
                    .Bind(() => _getElementService.GetWindowsFromFloor(floor)
                        .Bind(windows => _geometryService.GetInsideСontourFromFloor(floor)
                            .Bind(edgeArr => _geometryService.FiltrateContoursByWindowsInstance(edgeArr, windows))))
                    .Tap(res => edgeArrays = res)

                    .Bind(_ => _getElementService.GetFamilySymbolByFamilyName(PluginSettings.DesignationRoundFamily))
                    .Tap(res => roundFamilySymbol = res)

                    .Bind(_ => _getElementService.GetFamilySymbolByFamilyName(PluginSettings.DesignationRectangleFamily))
                    .Tap(res => rectangleFamilySymbol = res)

                    .Bind(_ => CreateDesignationInstances(edgeArrays, roundFamilySymbol, rectangleFamilySymbol)));

            transactionGroup.Assimilate();

            return result;
        }

        private Result<string> CreateDesignationInstances(
            List<EdgeArray> edgeArrays,
            FamilySymbol roundFamily,
            FamilySymbol rectangleFamily)
        {
            return Result.Try(
                () =>
                {
                    var errors = new Dictionary<string, int>();
                    var transaction = new Transaction(_uiDoc.Document, "Создание элементов");
                    transaction.Start();
                    foreach (var edgeArray in edgeArrays)
                    {
                        var contourData = _geometryService.GetContourData(edgeArray);
                        if (!contourData.IsValid)
                        {
                            continue;
                        }

                        FamilySymbol createdFamilySymbol;
                        switch (contourData.Type)
                        {
                            case ContourType.Rectangle:
                                createdFamilySymbol = rectangleFamily;
                                break;
                            default:
                                createdFamilySymbol = roundFamily;
                                break;
                        }

                        var newFamily = _uiDoc.Document.Create.NewFamilyInstance(
                            contourData.CentralPoint,
                            createdFamilySymbol,
                            _uiDoc.ActiveView);

                        if (!SetParameters(contourData, newFamily))
                        {
                            _loger.AddProblem($"Не удалось установить значения параметров для элемента {newFamily.Id}");
                        }

                        var rotationLine = Line.CreateUnbound(contourData.CentralPoint, XYZ.BasisZ);
                        ElementTransformUtils.RotateElement(_uiDoc.Document, newFamily.Id, rotationLine, contourData.Angle);
                    }

                    transaction.Commit();
                    return _loger.GetConbinatedProblem();
                }, e => $"При создании элементов возникла непредвиденная ошибка: {e.Message}");
        }

        private bool SetParameters(ContourData contourData, FamilyInstance familyInstance)
        {
            switch (contourData.Type)
            {
                case ContourType.Round:
                {
                    var param = familyInstance.LookupParameter(PluginSettings.RoundFamSetParamName);
                    if (param == null)
                        return false;

                    return param.Set(contourData.Height / 2);
                }

                default:
                    var paramWidth = familyInstance.LookupParameter(PluginSettings.RectangleSetWidthParamName);
                    var paramHeight = familyInstance.LookupParameter(PluginSettings.RectangleSetHeightParamName);
                    if (paramHeight == null || paramWidth == null)
                        return false;

                    return paramHeight.Set(contourData.Height) && paramWidth.Set(contourData.Width);
            }
        }
    }
}