namespace HoleDesignation.Models.Parameters
{
    using Autodesk.Revit.DB;

    /// <summary>
    /// Настройки плагина
    /// </summary>
    public static class PluginSettings
    {
        /// <summary>
        /// Имя круглого семейства обозначения
        /// </summary>
        public static string DesignationRoundFamily { get; set; } = "GEN_Обозначение проема-Круглый";

        /// <summary>
        /// Имя прямоугольного семейства обозначения
        /// </summary>
        public static string DesignationRectangleFamily { get; set; } = "GEN_Обозначение проема-Прямоугольный";

        /// <summary>
        /// Категория элементов, которую следуюет игнорировать при создании отверстий
        /// </summary>
        public static BuiltInCategory IgnorableElementCategory { get; set; } = BuiltInCategory.OST_Windows;

        /// <summary>
        /// Имя заполняемого параметра для круглого отверстия
        /// </summary>
        public static string RoundFamSetParamName { get; set; } = "Разомкнутый радиус";

        /// <summary>
        /// Имя заполняемого параметра Ширины для прямоугольных семейств
        /// </summary>
        public static string RectangleSetWidthParamName { get; set; } = "Ширина";

        /// <summary>
        /// Имя заполняемого параметра Глубина для прямоугольных семейств
        /// </summary>
        public static string RectangleSetHeightParamName { get; set; } = "Глубина";

        /// <summary>
        /// Расстояние выдавливания в мм
        /// </summary>
        public static double ExtrusionDistance { get; set; } = 1000;

        /// <summary>
        /// Один мм в футах
        /// </summary>
        public static double OneFt { get; set; } = 0.3048;

        /// <summary>
        /// Погрешность
        /// </summary>
        public static double Tolerance { get; set; } = 0.000001;

        /// <summary>
        /// Число до которого производить округление
        /// </summary>
        public static int RoundValue { get; set; } = 6;
    }
}