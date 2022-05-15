namespace LogWindow.Abstractions
{
    using System.Collections.Generic;

    /// <summary>
    /// Предоставляет методы для сохранения данных.
    /// </summary>
    public interface ILogSaver
    {
        /// <summary>
        /// Показать текст в блокноте (без сохранения на диск)
        /// </summary>
        /// <param name="message">Отображаемый текст.</param>
        /// <param name="title">Заголовок окна.</param>
        void ShowTextWithNotepad(string message, string title = null);

        /// <summary>
        /// Сохраняет ошибки в строку.
        /// </summary>
        /// <param name="messages">Коллекция ошибок.</param>
        /// <returns>Форматированная строка.</returns>
        string SaveToString(IEnumerable<BaseMessage> messages);
    }
}
