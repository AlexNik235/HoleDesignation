namespace HoleDesignation.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.UI.Selection;
    using CSharpFunctionalExtensions;
    using Helpers;
    using Models;
    using Models.Parameters;
    using Result = CSharpFunctionalExtensions.Result;

    /// <summary>
    /// Сервис получеия элементов
    /// </summary>
    public class GetElementService
    {
        private readonly UIDocument _uiDoc;
        private readonly GeometryService _geometryService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="uiDoc">UIDocument</param>
        public GetElementService(UIDocument uiDoc)
        {
            _uiDoc = uiDoc;
            _geometryService = new GeometryService(uiDoc);
        }

        /// <summary>
        /// Получает предварительно выбранное перекрытие 
        /// </summary>
        /// <returns>Перекрытие</returns>
        public Result<FloorWrapper> GetPreSelectedFloor()
        {
            var selectedFloor = _uiDoc.Selection.GetElementIds()
                .Select(id => _uiDoc.Document.GetElement(id)).OfType<Floor>().FirstOrDefault();
            return selectedFloor != null
                ? new FloorWrapper(selectedFloor)
                : Result.Failure<FloorWrapper>("Не выбрано перекрытие");
        }

        /// <summary>
        /// Выбирает перекрытие
        /// </summary>
        /// <returns>Перекрытие</returns>
        public Result<FloorWrapper> SelectFloor()
        {
            try
            {
                var reference = _uiDoc.Selection.PickObject(ObjectType.Element, new FloorSelection());
                return _uiDoc.Document.GetElement(reference.ElementId) is Floor floor
                    ? new FloorWrapper(floor)
                    : Result.Failure<FloorWrapper>("Не удалось выбрать плиту");
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Failure<FloorWrapper>("Пользователь отменил выбор плиты");
            }
            catch (Exception e)
            {
                return Result.Failure<FloorWrapper>($"При выборе элемента возникла непредвиденная ошибка: {e.Message}");
            }
        }

        /// <summary>
        /// Получить перекрытие из связанного файла
        /// </summary>
        /// <returns></returns>
        public Result<FloorWrapper> SelectFloorFromLinkedDoc()
        {
            try
            {
                var linkedInstances = new FilteredElementCollector(_uiDoc.Document)
                    .WhereElementIsNotElementType()
                    .OfClass(typeof(RevitLinkInstance))
                    .Cast<RevitLinkInstance>()
                    .Where(i => i.GetLinkDocument() != null)
                    .ToList();
                var reference = _uiDoc.Selection.PickObject(ObjectType.LinkedElement);
                var linkedFile =
                    linkedInstances.FirstOrDefault(linkInstance => linkInstance.Id.Equals(reference.ElementId));
                if (linkedFile == null)
                {
                    return Result.Failure<FloorWrapper>(
                        "Не удалось определить экземпляр связанного файла для выбранной плиты");
                }

                return linkedFile.GetLinkDocument().GetElement(reference.LinkedElementId) is Floor floor
                    ? new FloorWrapper(floor, linkedFile)
                    : Result.Failure<FloorWrapper>("Данный элемент не является перекрытием, выберите перекрытие");
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Failure<FloorWrapper>("Пользователь отменил выбор плиты");
            }
            catch (Exception e)
            {
                return Result.Failure<FloorWrapper>($"При выборе элемента возникла непредвиденная ошибка: {e.Message}");
            }
        }

        /// <summary>
        /// Получает первый тип семейства по имени семейства
        /// </summary>
        /// <param name="familyName">Имя семейства</param>
        /// <returns>Первый тип семейства</returns>
        public Result<FamilySymbol> GetFamilySymbolByFamilyName(string familyName)
        {
            var family = new FilteredElementCollector(_uiDoc.Document)
                .WhereElementIsNotElementType()
                .OfClass(typeof(Family))
                .Cast<Family>()
                .FirstOrDefault(f => f.Name.Equals(familyName));

            if (family == null)
            {
                return Result.Failure<FamilySymbol>(
                    $"Семейство {familyName} не найдено в проекте. Загрузите семейство и перезапустите плагин");
            }

            var typeIds = family.GetFamilySymbolIds();
            if (!typeIds.Any())
            {
                return Result.Failure<FamilySymbol>(
                    $"Семейство {familyName} не содержит ни одного типа. Создайте хотя бы один тип и повторите работу плагина");
            }

            return (FamilySymbol)_uiDoc.Document.GetElement(typeIds.First());
        }

        /// <summary>
        /// Получает окна, которые образуют геометрию плиты
        /// </summary>
        /// <param name="floor">Плита</param>
        /// <returns>Список окон</returns>
        public Result<List<WindowModel>> GetWindowsFromFloor(FloorWrapper floor)
        {
            var outLine = GetOutLine(floor, 0);
            if (outLine == null)
            {
                return Result.Failure<List<WindowModel>>("Не удалось определить outline для выбора семейств окон");
            }

            return Result.Try(
                () =>
                {
                    return new FilteredElementCollector(_uiDoc.Document)
                        .WhereElementIsNotElementType()
                        .WherePasses(new BoundingBoxIntersectsFilter(outLine))
                        .OfType<FamilyInstance>()
                        .Where(instance =>
                            (BuiltInCategory)instance.Category.Id.IntegerValue ==
                            PluginSettings.IgnorableElementCategory)
                        .Select(i => new WindowModel(i, _geometryService))
                        .ToList();
                },
                e =>
                    $"Не удалось получить отверстия категории \"Окна\", которые образуют плиту по причине: {e.Message}");
        }

        private Outline GetOutLine(FloorWrapper floorWrapper, double extraDistance)
        {
            try
            {
                var bb = floorWrapper.Element.get_BoundingBox(null);
                var delta = bb.Max - bb.Min;
                if (delta.GetLength() < PluginSettings.OneFt)
                    return null;

                var boxLine = Line.CreateBound(bb.Min, bb.Max);
                Outline outLine;

                if (floorWrapper.RevitLinkInstance != null)
                {
                    var transform = floorWrapper.RevitLinkInstance.GetTransform();
                    var elementGeometry = floorWrapper.Element.get_Geometry(new Options { DetailLevel = ViewDetailLevel.Fine });
                    var transformedGeometry = elementGeometry.GetTransformed(transform.Inverse);
                    var transformedBb = transformedGeometry.GetBoundingBox();

                    outLine = new Outline(
                        transformedBb.Min - boxLine.Direction * extraDistance,
                        transformedBb.Max + boxLine.Direction * extraDistance);
                }
                else
                {
                    outLine = new Outline(
                        bb.Min - boxLine.Direction * extraDistance,
                        bb.Max + boxLine.Direction * extraDistance);
                }

                return outLine;
            }
            catch
            {
                return null;
            }
        }
    }
}
