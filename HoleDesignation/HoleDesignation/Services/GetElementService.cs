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
        public Result<Floor> GetPreSelectedFloor()
        {
            var selectedFloor = _uiDoc.Selection.GetElementIds()
                .Select(id => _uiDoc.Document.GetElement(id)).OfType<Floor>().FirstOrDefault();
            return selectedFloor ?? Result.Failure<Floor>("Не выбрано перекрытие");
        }

        /// <summary>
        /// Выбирает перекрытие
        /// </summary>
        /// <returns>Перекрытие</returns>
        public Result<Floor> SelectFloor()
        {
            try
            {
                var referance = _uiDoc.Selection.PickObject(ObjectType.Element, new FloorSelection());
                var floor = _uiDoc.Document.GetElement(referance.ElementId) as Floor;
                return floor ?? Result.Failure<Floor>("Не удалось выбрать плиту");
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Failure<Floor>($"Пользователь отменил выбор плиты");
            }
            catch (Exception e)
            {
                return Result.Failure<Floor>($"При выборе элемента возникла непредвиденная ошибка: {e.Message}");
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
        public Result<List<WindowModel>> GetWindowsFromFloor(Floor floor)
        {
            return Result.Try(
                () =>
                {
                    var floorBb = floor.get_BoundingBox(null);
                    var outLine = new Outline(floorBb.Min, floorBb.Max);
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
    }
}
