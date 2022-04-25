namespace HoleDesignation.Helpers
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI.Selection;

    /// <summary>
    /// Класс для фильтрации элементов при выборе
    /// </summary>
    public class FloorSelection : ISelectionFilter
    {
        /// <inheritdoc/>
        public bool AllowElement(Element elem)
        {
            return elem is Floor;
        }

        /// <inheritdoc/>
        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
