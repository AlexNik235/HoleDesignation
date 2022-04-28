namespace HoleDesignation.Models
{
    using Autodesk.Revit.DB;
    using Enums;

    /// <summary>
    /// Данные о контуре
    /// </summary>
    public class ContourData
    {
        /// <summary>
        /// Центральная точка
        /// </summary>
        public XYZ CentralPoint { get; set; }

        /// <summary>
        /// Высота
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Ширина
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Форма контура
        /// </summary>
        public ContourType Type { get; set; }

        /// <summary>
        /// Угол поворота отверстия
        /// </summary>
        public double Angle { get; set; } = 0;

        /// <summary>
        /// Является ли форма валидной
        /// </summary>
        public bool IsValid { get; set; }
    }
}