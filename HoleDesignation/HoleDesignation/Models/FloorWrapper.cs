namespace HoleDesignation.Models
{
    using System;
    using Autodesk.Revit.DB;
    using Extensions;

    /// <summary>
    /// Обертка над перекрытием
    /// </summary>
    public class FloorWrapper
    {
        private Lazy<Solid> _solid;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="floor">Перекрытие</param>
        /// <param name="revitLinkInstance">Связь из которого взято перекрытие</param>
        public FloorWrapper(Floor floor, RevitLinkInstance revitLinkInstance = null)
        {
            RevitLinkInstance = revitLinkInstance;
            Doc = revitLinkInstance?.GetLinkDocument() ?? floor.Document;
            _solid = new Lazy<Solid>(() => floor.GetSolid(revitLinkInstance: revitLinkInstance));
            Element = floor;
        }

        /// <summary>
        /// Элемент ревита
        /// </summary>
        public Floor Element { get; }

        /// <summary>
        /// Солид плиты
        /// </summary>
        public Solid Solid => _solid.Value;

        /// <summary>
        /// Связанный файл для перекрытия
        /// </summary>
        public RevitLinkInstance RevitLinkInstance { get; }

        /// <summary>
        /// Документ к которому принадлежит элемент
        /// </summary>
        public Document Doc { get; }
    }
}