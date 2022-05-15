namespace LogWindow.Models
{
    /// <inheritdoc />
    public class CommonErrorMessage : InfoMessage
    {
        /// <inheritdoc />
        public CommonErrorMessage(string errorText, string errorTitle)
        : base(errorText)
        {
            ErrorTitle = errorTitle;
        }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string ErrorTitle { get; }
    }
}
