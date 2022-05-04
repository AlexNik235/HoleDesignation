namespace HoleDesignation.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Extensions;
    using Models;
    using Models.Parameters;
    using Result = CSharpFunctionalExtensions.Result;

    /// <summary>
    /// Сервис проверок и валидаций
    /// </summary>
    public class ValidationService
    {
        private readonly UIDocument _uiDoc;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="uiDocument">UIDocument</param>
        public ValidationService(UIDocument uiDocument)
        {
            _uiDoc = uiDocument;
        }

        /// <summary>
        /// Проверяет является ли текущий вид планом
        /// </summary>
        public Result IsViewPlan()
        {
            var activeViewType = _uiDoc.Document.ActiveView.ViewType;
            return Result.SuccessIf(
                activeViewType.ToString().EndsWith("Plan"),
                "Текущий вид не является планом, перейдите на план");
        }

        /// <summary>
        /// Проверяет более одного контура в перекрытии
        /// </summary>
        /// <param name="floor">Перекрытие</param>
        public Result ValidateByContourCount(FloorWrapper floor)
        {
            try
            {
                var sketchId = floor.Element.GetDependentElements(new ElementClassFilter(typeof(Sketch))).FirstOrDefault();
                if (sketchId == null)
                    return Result.Success();

                var sketch = (Sketch)floor.Doc.GetElement(sketchId);
                var allProfileLines = new List<Line>();
                foreach (var curveArray in sketch.Profile.OfType<CurveArray>())
                {
                    var curves = curveArray.OfType<Line>().ToList();
                    if (!curves.Any())
                        continue;
                    var centralPoint = curves.GetCentralPoint();
                    allProfileLines.AddRange(curves.Select(i => i.AntiClockWizeDirectionLine(centralPoint)));
                }

                var outSideCurves = 0;
                foreach (CurveArray curveArray in sketch.Profile)
                {
                    var curves = curveArray.OfType<Line>().ToList();
                    if (!curves.Any())
                        continue;
                    var centralPoint = curves.GetCentralPoint();
                    var lines = curves.Select(i => i.AntiClockWizeDirectionLine(centralPoint)).ToList();
                    if (!lines.All(l => HasIntersect(l, allProfileLines)))
                        outSideCurves++;
                }

                return Result.SuccessIf(outSideCurves < 2, "В плита разделена на два контура или более");
            }
            catch (Exception e)
            {
                return Result.Failure($"При проверки контура перекрытия на кол-во возникла непредвиденная ошибка: {e.Message}");
            }
        }

        private bool HasIntersect(Line line, List<Line> lines)
        {
            var firstPoint = line.GetEndPoint(0);
            var secondPoint = line.GetEndPoint(1);
            var rightDirection = line.Direction.Normalize().CrossProduct(XYZ.BasisZ);
            var centralPoint = line.Evaluate(line.ApproximateLength / 2, false);
            var secondRayPoint = centralPoint + rightDirection * 10000;
            var ray = Line.CreateBound(centralPoint, secondRayPoint);
            foreach (var comparisonLine in lines)
            {
                var firs = comparisonLine.GetEndPoint(0);
                var sec = comparisonLine.GetEndPoint(1);
                if (firstPoint.IsAlmostEqualTo(firs, PluginSettings.Tolerance)
                    && secondPoint.IsAlmostEqualTo(sec, PluginSettings.Tolerance))
                    continue;

                var result = ray.Intersect(comparisonLine);
                if (result == SetComparisonResult.Overlap)
                    return true;
            }

            return false;
        }
    }
}