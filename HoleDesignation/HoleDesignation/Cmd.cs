namespace HoleDesignation
{
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using CSharpFunctionalExtensions;
    using GENPRO_Design.DialogWindow;
    using LogWindow.Services;
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
            var logWindow = new DisplayLogger(commandData.Application);
            var holeDesignationService = new HoleDesignationService(
                commandData.Application.ActiveUIDocument, logWindow);

            return holeDesignationService.Execute()
                .Match(
                    () =>
                    {
                        if (logWindow.HasMessages())
                        {
                            logWindow.Show("Отчет");
                        }

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