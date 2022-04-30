namespace HoleDesignation.Helpers
{
    using System;
    using System.Collections.Generic;
    using Autodesk.Revit.DB;
    using HoleDesignation.Models.Parameters;

    /// <summary>
    /// Сортировка по минимальной У и максимальной Х
    /// </summary>
    /// <typeparam name="XYZ">Тип сравниваемого элемента</typeparam>
    public class PointComparer : IComparer<XYZ>
    {
        /// <inheritdoc/>
        public int Compare(XYZ x, XYZ y)
        {
            if (x.IsAlmostEqualTo(y, PluginSettings.Tolerance))
                return 0;

            if (Math.Round(x.X, PluginSettings.RoundValue) > Math.Round(y.X, PluginSettings.RoundValue))
            {
                return 1;
            }

            if (Math.Abs(Math.Round(x.X, PluginSettings.RoundValue) - Math.Round(y.X, PluginSettings.RoundValue)) < PluginSettings.Tolerance)
            {
                if (Math.Round(x.Y, PluginSettings.RoundValue) < Math.Round(y.Y, PluginSettings.RoundValue))
                    return 1;
            }

            return -1;
        }
    }
}