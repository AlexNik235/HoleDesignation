namespace HoleDesignation.Services
{
    using Autodesk.Revit.UI;
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
    }
}