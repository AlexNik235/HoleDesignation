namespace HoleDesignation.Models
{
    using System;
    using Autodesk.Revit.DB;
    using Extensions;
    using Parameters;
    using Services;

    /// <summary>
    /// Модель окна
    /// </summary>
    public class WindowModel
    {
        private readonly Lazy<Solid> _solid;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="familyInstance">Экземпляр окна</param>
        /// <param name="geometryService">Сервис по работе с геометрией</param>
        /// <param name="revitLinkInstance">Связь из которой получен элемент</param>
        public WindowModel(
            FamilyInstance familyInstance, GeometryService geometryService, RevitLinkInstance revitLinkInstance = null)
        {
            _solid = new Lazy<Solid>(() => CreateSolid(familyInstance, geometryService, revitLinkInstance));
        }

        /// <summary>
        /// Созданный солид элемента
        /// </summary>
        public Solid Solid => _solid.Value;

        private Solid CreateSolid(
            FamilyInstance familyInstance, GeometryService geometryService, RevitLinkInstance revitLinkInstance)
        {
            var curves = familyInstance.GetCurves(familyInstance.Document.ActiveView, revitLinkInstance);
            var curveLoop = geometryService.GetCurveLoopsFromCurves(curves);
            if (curveLoop == null)
                return null;

            return geometryService.CreateSolidFromCurveLoop(
                curveLoop,
                PluginSettings.ExtrusionDistance * PluginSettings.OneFt);
        }
    }
}