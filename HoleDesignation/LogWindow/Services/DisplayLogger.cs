namespace LogWindow.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Abstractions;
    using Autodesk.Revit.UI;
    using LogWindow.ViewModels;
    using LogWindow.Views;

    /// <inheritdoc />
    public class DisplayLogger : IDisplayLogger
    {
        private readonly ILoggerWindow _loggerWindow;
        private readonly ILogStorage _logStorage;

        /// <inheritdoc />
        public DisplayLogger(UIApplication uIApplication)
        {
            _logStorage = new LogStorage();
            var elementDisplayService = new ElementsDisplayService(uIApplication);
            var logerWindowViewModel = new LoggerWindowViewModel(
                elementDisplayService, new LogSaver(), _logStorage);
            var logerWindowView = new LoggerWindowView(logerWindowViewModel);
            _loggerWindow = new LoggerWindow(
                logerWindowView, logerWindowViewModel, GetDataTemplateUriAttributes());
            _logStorage.ElementStorageChanged += (sender, args) => ElementStorageChanged?.Invoke(sender, args);
        }

        /// <inheritdoc />
        public event EventHandler ElementStorageChanged;

        /// <inheritdoc />
        public void Show(string title, bool isModal = false)
        {
            _loggerWindow.Show(title, isModal);
        }

        /// <inheritdoc />
        public void AddMessage<T>(T message)
            where T : BaseMessage
        {
            _logStorage.AddMessage(message);
        }

        /// <inheritdoc />
        public IEnumerable<BaseMessage> GetMessages()
        {
            return _logStorage.GetMessages();
        }

        /// <inheritdoc />
        public bool HasMessages()
        {
            return _logStorage.HasMessages();
        }

        /// <inheritdoc />
        public void Clear()
        {
            _logStorage.Clear();
        }

        private IEnumerable<DataTemplateUriAttribute> GetDataTemplateUriAttributes()
        {
            var result = Assembly.GetCallingAssembly()
                        .GetTypes()
                        .Where(t => t.IsClass
                                    && !t.IsAbstract
                                    && t.IsSubclassOf(typeof(BaseMessage)))
                        .Select(t => t.GetCustomAttribute(typeof(DataTemplateUriAttribute)))
                        .Where(a => a != null)
                        .Cast<DataTemplateUriAttribute>();
            return result.AsEnumerable();
        }
    }
}
