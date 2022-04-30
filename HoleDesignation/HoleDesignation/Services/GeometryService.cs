namespace HoleDesignation.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using CSharpFunctionalExtensions;
    using Enums;
    using Extensions;
    using Helpers;
    using Models;
    using Models.Parameters;
    using Result = CSharpFunctionalExtensions.Result;

    /// <summary>
    /// Сервис по работе с геометрией
    /// </summary>
    public class GeometryService
    {
        private const int CountLineInRoundCountur = 2;
        private const int Half = 2;
        private readonly UIDocument _uiDoc;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="uiDocument">UIDocument</param>
        public GeometryService(UIDocument uiDocument)
        {
            _uiDoc = uiDocument;
        }

        /// <summary>
        /// Получает внутренние контура из плиты
        /// </summary>
        /// <param name="floor">Плита перекрытия</param>
        /// <returns>Список контуров</returns>
        public Result<List<EdgeArray>> GetInsideСontourFromFloor(Floor floor)
        {
            if (!floor.Document.Title.Equals(_uiDoc.Document.Title))
            {
                return Result.Failure<List<EdgeArray>>(
                    "На данный момент работа с плитами из связанного файла недоступна");
            }

            var biggestFace = floor.GetSolid().Faces.OfType<Face>().OrderBy(f => f.Area).LastOrDefault();
            if (biggestFace == null)
                return Result.Failure<List<EdgeArray>>("Не удалось определить большую из поверхностей плиты");

            // Берем все замкнутые контура, сортируем их по длине от большего к меньшему, пропускаем первый, т.е. это внешний контур
            return biggestFace.EdgeLoops.OfType<EdgeArray>()
                .OrderByDescending(eA => eA.OfType<Edge>()
                    .Sum(e => e.ApproximateLength)).Skip(1).Where(i => i.Size == 2 || i.Size == 4)
                .ToList();
        }

        /// <summary>
        /// Фильтрует список с гранями, по семействам окон
        /// </summary>
        /// <param name="edgesArrays">Список с гранями</param>
        /// <param name="windows">Список с окнами</param>
        /// <returns>Фильтрованный список</returns>
        public Result<List<EdgeArray>> FiltrateContoursByWindowsInstance(
            List<EdgeArray> edgesArrays, List<WindowModel> windows)
        {
            return Result.Try(
                () =>
                {
                    if (!windows.Any())
                        return edgesArrays;
                    var transaction = new Transaction(_uiDoc.Document, "Создание солидов для сравнения контуров");
                    transaction.Start();
                    var resultList = new List<EdgeArray>();

                    foreach (var edgeArray in edgesArrays)
                    {
                        var edgeCurveLoop = edgeArray.GetCurveLoop();
                        if (edgeCurveLoop == null)
                            continue;

                        var edgeSolid = CreateSolidFromCurveLoop(
                            edgeCurveLoop,
                            PluginSettings.ExtrusionDistance * PluginSettings.OneFt);
                        if (edgeSolid == null)
                            continue;

                        if (!windows.Any(window => IsSolidIntersected(edgeSolid, window.Solid)))
                        {
                            resultList.Add(edgeArray);
                        }
                    }

                    transaction.Commit();
                    return resultList;
                },
                e =>
                    $"При отфильтровать контура плиты по семействам отверстий возникла непредвиденная ошибка: {e.Message}");
        }

        /// <summary>
        /// Получает данные о контуре
        /// </summary>
        /// <param name="edgeArray">Массив линий</param>
        /// <returns>Данные о контуре</returns>
        public ContourData GetContourData(EdgeArray edgeArray)
        {
            var curves = edgeArray.OfType<Edge>().Select(i => i.AsCurve()).ToList();
            var points = curves
                .SelectMany(c => c.Tessellate()).ToList();

            var orderByX = points.OrderBy(p => p.X).ToList();
            var orderByY = points.OrderBy(p => p.Y).ToList();

            var data = new ContourData
            {
                Height = Math.Abs(orderByY.Last().Y - orderByY.First().Y),
                Width = Math.Abs(orderByX.Last().X - orderByX.First().X),
                Type = edgeArray.Size > CountLineInRoundCountur ? ContourType.Rectangle : ContourType.Round,
                IsValid = CheckShapeValid(edgeArray)
            };

            if (!data.IsValid)
                return data;

            data.CentralPoint = new XYZ(
                orderByX.First().X + data.Width / Half,
                orderByY.First().Y + data.Height / Half,
                points.First().Z);

            return data.Type == ContourType.Rectangle
                ? AnalyzeRectangleContour(edgeArray, data, data.CentralPoint)
                : data;
        }

        /// <summary>
        /// Создает солид из CurveLoop
        /// </summary>
        /// <param name="curveLoop">CurveLoop</param>
        /// <param name="extrusionDistance">Расстояние выдавливания</param>
        /// <returns>Солид</returns>
        public Solid CreateSolidFromCurveLoop(CurveLoop curveLoop, double extrusionDistance)
        {
            try
            {
                return GeometryCreationUtilities.CreateExtrusionGeometry(
                    new List<CurveLoop> { curveLoop },
                    XYZ.BasisZ,
                    extrusionDistance);
            }
            catch
            {
                // ignore
            }

            return null;
        }

        /// <summary>
        /// Получает CurveLoop из набора кривых
        /// </summary>
        /// <param name="curves">Кривые</param>
        /// <returns>Curve loop</returns>
        public CurveLoop GetCurveLoopsFromCurves(List<Curve> curves)
        {
            if (curves.Count < 2)
                return null;

            var currentCurve = curves.First();
            var usedCurveList = new List<Curve>
            {
                currentCurve
            };
            var linkedLines = new List<Curve>
            {
                currentCurve
            };
            while (usedCurveList.Count < curves.Count)
            {
                var lastPoint = currentCurve.GetEndPoint(1);
                foreach (var cur in curves.Where(c => !c.Equals(currentCurve) && !usedCurveList.Contains(c)))
                {
                    var firstCurPoint = cur.GetEndPoint(0);
                    var lastCurPoint = cur.GetEndPoint(1);
                    if (lastPoint.IsAlmostEqualTo(firstCurPoint, PluginSettings.Tolerance))
                    {
                        usedCurveList.Add(currentCurve);
                        currentCurve = cur;
                        linkedLines.Add(currentCurve);
                        break;
                    }

                    if (!lastPoint.IsAlmostEqualTo(lastCurPoint, PluginSettings.Tolerance))
                        continue;
                    usedCurveList.Add(currentCurve);
                    currentCurve = cur;
                    linkedLines.Add(currentCurve);
                    break;
                }

                if (!IsLoopIsClose(linkedLines))
                    continue;
                {
                    var curveLoop = new CurveLoop();
                    linkedLines.ForEach(c => curveLoop.Append(c));
                    return curveLoop;
                }
            }

            return null;
        }

        private double GetAngle(Line line, XYZ centralPoint)
        {
            var dirVector = line.AntiClockWizeDirectionLine(centralPoint).Direction.Normalize();
            return XYZ.BasisX.AngleTo(dirVector);
        }

        private ContourData AnalyzeRectangleContour(EdgeArray edgeArray, ContourData data, XYZ centralPoint)
        {
            var lines = edgeArray.OfType<Edge>().Select(i => i.AsCurve()).OfType<Line>()
                .Select(i => i.AntiClockWizeDirectionLine(centralPoint)).ToList();
            var points = lines.SelectMany(i => i.Tessellate()).ToList();
            var minYxPoint = points.OrderBy(x => x, new PointComparer()).LastOrDefault();

            var commonLine = lines.Where(i => ContainsPoint(i, minYxPoint))
                .OrderBy(i => i.Tessellate().Sum(p => p.Y)).ToList();
            var botLine = commonLine.First();
            var sideLine = commonLine.Last();

            if (botLine == null || sideLine == null)
            {
                data.IsValid = false;
                return data;
            }

            data.Angle = GetAngle(botLine, centralPoint);
            data.Height = sideLine.ApproximateLength;
            data.Width = botLine.ApproximateLength;

            return data;
        }

        private bool ContainsPoint(Line line, XYZ checkPoint)
        {
            var lineFirstPoint = line.GetEndPoint(0);
            var lineSecPoint = line.GetEndPoint(1);

            return lineFirstPoint.IsAlmostEqualTo(checkPoint, PluginSettings.Tolerance) ||
                   lineSecPoint.IsAlmostEqualTo(checkPoint, PluginSettings.Tolerance);
        }

        private bool CheckSideLine(Line botLine, Line checkLine)
        {
            var botLineFirP = botLine.GetEndPoint(0);
            var botLineSecP = botLine.GetEndPoint(1);
            var checkLineFirP = checkLine.GetEndPoint(0);
            var checkLineSecP = checkLine.GetEndPoint(1);

            if (botLineFirP.IsAlmostEqualTo(checkLineFirP, PluginSettings.Tolerance)
                && botLineSecP.IsAlmostEqualTo(checkLineSecP, PluginSettings.Tolerance))
                return false;

            return botLineFirP.IsAlmostEqualTo(checkLineFirP, PluginSettings.Tolerance)
                   || botLineSecP.IsAlmostEqualTo(checkLineSecP, PluginSettings.Tolerance);
        }

        private bool CheckShapeValid(EdgeArray edgeArray)
        {
            if (edgeArray.Size < 3)
                return true;

            for (var i = 1; i < edgeArray.Size; i++)
            {
                var prevLine = (Line)edgeArray.get_Item(i - 1).AsCurve();
                var curLine = (Line)edgeArray.get_Item(i).AsCurve();
                var angle = prevLine.Direction.Normalize().AngleTo(curLine.Direction.Normalize());
                if (Math.Abs(Math.PI / 2 - Math.Abs(angle)) > PluginSettings.Tolerance
                    && Math.Abs(Math.PI - Math.Abs(angle)) > PluginSettings.Tolerance)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsSolidIntersected(Solid fistSolid, Solid windowSolid)
        {
            try
            {
                if (windowSolid == null || fistSolid == null)
                    return false;

                var newSolid =
                    BooleanOperationsUtils.ExecuteBooleanOperation(
                        fistSolid, windowSolid, BooleanOperationsType.Union);

                return Math.Abs(newSolid.Volume - fistSolid.Volume - windowSolid.Volume) > PluginSettings.Tolerance;
            }
            catch
            {
                // при пересечении солидов часто возникает ошибка, по причине что возможно их размеры идентично равны
                return true;
            }
        }

        private bool IsLoopIsClose(List<Curve> curves)
        {
            var fistLine = curves.FirstOrDefault();
            var lastLine = curves.LastOrDefault();
            if (fistLine == null || lastLine == null)
                return false;

            return fistLine.GetEndPoint(0).IsAlmostEqualTo(lastLine.GetEndPoint(1), PluginSettings.Tolerance);
        }
    }
}