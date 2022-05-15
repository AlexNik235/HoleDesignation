namespace LogWindow.Abstractions
{
    /// <summary>
    /// Предоставляет методы для отображения окна логирования ошибок.
    /// </summary>
    public interface ILoggerWindow
    {
        /// <summary>
        /// Показать окно логирования.
        /// </summary>
        /// <param name="title">Заголовок окна.</param>
        /// <param name="isModal">Модальное или не модальное окно</param>
        void Show(string title, bool isModal = false);
    }
}
