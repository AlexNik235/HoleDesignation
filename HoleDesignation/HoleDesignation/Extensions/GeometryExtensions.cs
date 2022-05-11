namespace HoleDesignation.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Autodesk.Revit.DB;
    using HoleDesignation.Models.Parameters;

    /// <summary>
    /// Расширения для геометрии
    /// </summary>
    public static class GeometryExtensions
    {
        private const int Half = 2;

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
                if (geometryElement is Solid solid && solid.Volume > PluginSettings.Tolerance)
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
        /// <param name="view">Вид на котором нужно искать геометрию элемента</param>
        /// <param name="revitLinkInstance">Если элемент из связанного файла, указывается экземпляр связи</param>
        /// <returns></returns>
        public static List<Curve> GetCurves(
            this Element element,
            View view = null,
            RevitLinkInstance revitLinkInstance = null)
        {
            Options opt;
            if (view == null)
            {
                opt = new Options
                {
                    DetailLevel = ViewDetailLevel.Fine
                };
            }
            else
            {
                opt = new Options
                {
                    View = view
                };
            }

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
        /// Смещает кривую
        /// </summary>
        /// <param name="curve">Кривая</param>
        /// <param name="direction">Направление смещения</param>
        /// <param name="offSet">Величина смещения</param>
        /// <returns>Новая кривая</returns>
        public static Curve MoveCurve(this Curve curve, XYZ direction, double offSet)
        {
            switch (curve)
            {
                case Arc arc:
                    var pointOn = arc.Tessellate().Skip(1).FirstOrDefault();
                    return Arc.Create(
                        arc.GetEndPoint(0) + direction * offSet,
                        arc.GetEndPoint(1) + direction * offSet,
                        pointOn + direction * offSet);
                case Line line:
                    return Line.CreateBound(
                        line.GetEndPoint(0) + direction * offSet,
                        line.GetEndPoint(1) + direction * offSet);
            }

            return null;
        }

        /// <summary>
        /// Получает новую линию с направлением против часовой стрелки
        /// </summary>
        /// <param name="line">Кривая</param>
        /// <param name="centralPoint">Центральная точка</param>
        /// <returns>Новая линия</returns>
        public static Line AntiClockWizeDirectionLine(this Line line, XYZ centralPoint)
        {
            // алгоритм определения точки лежащей слева или справа от прямой
            // если d < 0 то точка центра лежит слева, значит линия идет против часовой стрелки
            // если d >= 0 точка лежит справа от линии и нам нужно перевернуть линию
            var firstPoint = line.GetEndPoint(0);
            var secondPoint = line.GetEndPoint(1);
            var d = (centralPoint.X - firstPoint.X) * (secondPoint.Y - firstPoint.Y) -
                    (centralPoint.Y - firstPoint.Y) * (secondPoint.X - firstPoint.X);

            return d < 0 ? line : Line.CreateBound(secondPoint, firstPoint);
        }

        /// <summary>
        /// Получает центральную точку относительно контура
        /// </summary>
        /// <param name="curvesArray">Список кривых контура</param>
        /// <returns>Центарльная точка</returns>
        public static XYZ GetCentralPoint(this IEnumerable<Curve> curvesArray)
        {
            var points = curvesArray
                .SelectMany(c => c.Tessellate()).ToList();

            var orderByX = points.OrderBy(p => p.X).ToList();
            var orderByY = points.OrderBy(p => p.Y).ToList();
            var xDif = Math.Abs(orderByX.Last().X - orderByX.First().X);
            var yDif = Math.Abs(orderByY.Last().Y - orderByY.First().Y);

            return new XYZ(
                orderByX.First().X + xDif / Half,
                orderByY.First().Y + yDif / Half,
                points.First().Z);
        }

        /// <summary>
        /// Получает CurveLoop из массива граней
        /// </summary>
        /// <param name="edgeArray">Массив граней</param>
        /// <param name="zOffset">Смещение линий по Z</param>
        /// <returns>CurveLoop</returns>
        public static CurveLoop GetCurveLoop(this EdgeArray edgeArray, double zOffset = 0)
        {
            var curves = edgeArray.OfType<Edge>().Select(e => e.AsCurve())
                .Select(i => i.MoveCurve(XYZ.BasisZ, -10 * PluginSettings.OneFt)).ToList();
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