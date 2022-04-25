namespace HoleDesignation.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Autodesk.Revit.DB;

    /// <summary>
    /// Расширения для геометрии
    /// </summary>
    public static class GeometryExtensions
    {
        /// <summary>
        /// Получает солид из элемента
        /// </summary>
        /// <param name="element">Элемент</param>
        /// <param name="includeNonVisibleObjects">Включать невидимую геометрию</param>
        /// <param name="revitLinkInstance">Если элемент из связанного файла, указывается экземпляр связи</param>
        /// <returns></returns>
        public static Solid GetSolid(
            this Element element,
            bool includeNonVisibleObjects = false,
            RevitLinkInstance revitLinkInstance = null)
        {
            var opt = new Options
            {
                DetailLevel = ViewDetailLevel.Fine,
                IncludeNonVisibleObjects = includeNonVisibleObjects
            };

            var solids = new List<Solid>();
            var geometry = element
                .get_Geometry(opt)
                .GetTransformed(revitLinkInstance == null ? Transform.Identity : revitLinkInstance.GetTotalTransform());

            foreach (var geometryElement in geometry)
            {
                if (geometryElement is Solid solid)
                {
                    solids.Add(solid);
                }
            }

            return solids.OrderBy(s => s.Volume).LastOrDefault();
        }

        /// <summary>
        /// Получает кривые из элемента
        /// </summary>
        /// <param name="element">Элемент</param>
        /// <param name="includeNonVisibleObjects">Включать невидимую геометрию</param>
        /// <param name="revitLinkInstance">Если элемент из связанного файла, указывается экземпляр связи</param>
        /// <returns></returns>
        public static List<Curve> GetCurves(
            this Element element,
            bool includeNonVisibleObjects = false,
            RevitLinkInstance revitLinkInstance = null)
        {
            var opt = new Options
            {
                DetailLevel = ViewDetailLevel.Fine,
                IncludeNonVisibleObjects = includeNonVisibleObjects
            };

            var curves = new List<Curve>();
            var geometry = element
                .get_Geometry(opt)
                .GetTransformed(revitLinkInstance == null ? Transform.Identity : revitLinkInstance.GetTotalTransform());

            foreach (var geometryElement in geometry)
            {
                if (geometryElement is Curve curve)
                {
                    curves.Add(curve);
                }
            }

            return curves;
        }

        /// <summary>
        /// Получает CurveLoop из массива граней
        /// </summary>
        /// <param name="edgeArray">Массив граней</param>
        /// <returns>CurveLoop</returns>
        public static CurveLoop GetCurveLoop(this EdgeArray edgeArray)
        {
            var curves = edgeArray.OfType<Edge>().Select(e => e.AsCurve()).ToList();
            var newCurveLoop = new CurveLoop();
            try
            {
                curves.ForEach(c => newCurveLoop.Append(c));
                return newCurveLoop;
            }
            catch
            {
                newCurveLoop = new CurveLoop();
                curves.Reverse();
                curves.ForEach(c => newCurveLoop.Append(c));
                return newCurveLoop;
            }
        }
    }
}