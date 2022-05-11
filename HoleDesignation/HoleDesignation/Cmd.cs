namespace HoleDesignation
{
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using CSharpFunctionalExtensions;
    using GENPRO_Design.DialogWindow;
    using Services;
    using Result = Autodesk.Revit.UI.Result;

    /// <summary>
    /// Execute class
    /// </summary>
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    public class Cmd : IExternalCommand
    {
        /// <summary>
        /// Выполняет команду плагина
        /// </summary>
        /// <param name="commandData">Данные</param>
        /// <param name="message">Сообщение</param>
        /// <param name="elements">Список элементов</param>
        /// <returns></returns>
        public Result Execute(
            ExternalCommandData
                commandData,
            ref string message,
            ElementSet elements)
        {
            var holeDesignationService = new HoleDesignationService(commandData.Application.ActiveUIDocument);

            return holeDesignationService.Execute()
                .Match(
                    res =>
                    {
                        var resultMessage = "Работа плагина завершена.\n";
                        if (!string.IsNullOrEmpty(res))
                            resultMessage += res;
                        GenproWindow.Information(resultMessage);
                        return Result.Succeeded;
                    },
                    err =>
                    {
                        GenproWindow.Error(err);
                        return Result.Failed;
                    });
        }
    }
}