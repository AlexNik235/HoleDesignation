namespace LogWindow.Models
{
    using Abstractions;

    /// <inheritdoc />
    public class ResultMessage : BaseMessage
    {
        private readonly int _count;

        /// <summary>
        /// Конструктор сообщения результата
        /// </summary>
        /// <param name="count">Количество отработанных элементов.</param>
        public ResultMessage(int count)
        {
            _count = count;
        }

        /// <inheritdoc />
        public override string ToString()
            => _count > 0
                ? $"Успешно отработан: {_count} шт. Работа плагина завершена."
                : "Плагин завершил работу, ничего не изменив.";
    }
}
