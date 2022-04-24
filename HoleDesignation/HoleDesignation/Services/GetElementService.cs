namespace HoleDesignation.Services
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using CSharpFunctionalExtensions;
    using System.Linq;

    /// <summary>
    /// Сервис получеия элементов
    /// </summary>
    public class GetElementService
    {
        private readonly UIDocument _uiDoc;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="uiDoc">UIDocument</param>
        public GetElementService(UIDocument uiDoc)
        {
            _uiDoc = uiDoc;
        }

        /// <summary>
        /// Получает предварительно выбранное перекрытие
        /// </summary>
        /// <returns></returns>
        public Result<Floor> GetPreSelectedFloor()
        {
            _uiDoc.Selection.GetElementIds().Select(id => _uiDoc.Document.GetElement(id)).OfType<Floor>().FirstOrDefault()
        }
    }
}
