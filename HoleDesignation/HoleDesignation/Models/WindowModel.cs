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
        private Lazy<Solid> _solid;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="familyInstance">Экземпляр окна</param>
        /// <param name="geometryService">Сервис по работе с геометрией</param>
        public WindowModel(FamilyInstance familyInstance, GeometryService geometryService)
        {
            _solid = new Lazy<Solid>(() =>
            {
                var curves = familyInstance.GetCurves(familyInstance.Document.ActiveView);
                var curveLoop = geometryService.GetCurveLoopsFromCurves(curves);
                if (curveLoop == null)
                    return null;

                return geometryService.CreateSolidFromCurveLoop(
                    curveLoop,
                    PluginSettings.ExtrusionDistance * PluginSettings.OneFt);
            });
        }

        /// <summary>
        /// Созданный солид элемента
        /// </summary>
        public Solid Solid => _solid.Value;
    }
}