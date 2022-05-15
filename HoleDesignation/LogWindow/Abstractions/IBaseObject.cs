namespace LogWindow.Abstractions
{
    using System;

    /// <summary>
    /// Представляет базовый тип для ошибки журнала.
    /// </summary>
    public interface IBaseObject : IComparable
    {
        /// <summary>
        /// Описание объекта.
        /// </summary>
        object DescObject { get; }

        /// <summary>
        /// Получить Id объекта.
        /// </summary>
        long GetId();
    }
}
