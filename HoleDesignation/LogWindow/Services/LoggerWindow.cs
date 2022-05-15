namespace LogWindow.Services
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using LogWindow.Abstractions;
    using LogWindow.ViewModels;
    using LogWindow.Views;

    /// <inheritdoc />
    public class LoggerWindow : ILoggerWindow
    {
        private static LoggerWindowView _loggerWindowView;
        private readonly LoggerWindowViewModel _loggerWindowViewModel;
        private readonly IEnumerable<DataTemplateUriAttribute> _additionalMessages;
        private LoggerWindowView _loggerWindowViewFromContainer;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LoggerWindow"/>.
        /// </summary>
        /// <param name="loggerWindowView">View.</param>
        /// <param name="loggerWindowViewModel">ViewModel.</param>
        /// <param name="additionalMessages">Дополнительные сообщения.</param>
        public LoggerWindow(
            LoggerWindowView loggerWindowView,
            LoggerWindowViewModel loggerWindowViewModel,
            IEnumerable<DataTemplateUriAttribute> additionalMessages)
        {
            _loggerWindowViewFromContainer = loggerWindowView;
            _loggerWindowViewModel = loggerWindowViewModel;
            _additionalMessages = additionalMessages;
        }

        /// <inheritdoc />
        public void Show(string title, bool isModal = false)
        {
            if (_loggerWindowView == null)
            {
                _loggerWindowView = _loggerWindowViewFromContainer ?? new LoggerWindowView(_loggerWindowViewModel);
                _loggerWindowViewFromContainer = null;
                _loggerWindowViewModel.Title = title;
                RegisterAdditionalMessages();
                _loggerWindowView.Closed += (sender, args) => _loggerWindowView = null;
                if (isModal)
                {
                    _loggerWindowView.ShowDialog();
                }
                else
                {
                    _loggerWindowView.Show();
                }
            }
            else
            {
                _loggerWindowView.DataContext = _loggerWindowViewModel;
                RegisterAdditionalMessages();
                _loggerWindowView.Activate();
                _loggerWindowView.Show();
            }
        }

        private void RegisterAdditionalMessages()
        {
            var resultDict = new ResourceDictionary();
            foreach (var dataTemplateUriAttribute in _additionalMessages)
            {
                var dict = new Dictionary<Uri, ResourceDictionary>();

                ResourceDictionary resourceDict;
                if (dict.ContainsKey(dataTemplateUriAttribute.Uri))
                {
                    resourceDict = dict[dataTemplateUriAttribute.Uri];
                }
                else
                {
                    resourceDict = new ResourceDictionary() { Source = dataTemplateUriAttribute.Uri };
                    dict[dataTemplateUriAttribute.Uri] = resourceDict;
                }

                if (!resourceDict.Contains(dataTemplateUriAttribute.Key))
                    continue;
                var dataTemplate = (DataTemplate)resourceDict[dataTemplateUriAttribute.Key];
                if (dataTemplate is null)
                    continue;
                dataTemplate.DataType = dataTemplateUriAttribute.Type;
                if (!resultDict.Contains(dataTemplate.DataTemplateKey!))
                {
                    resultDict.Add(dataTemplate.DataTemplateKey, dataTemplate);
                }
            }

            if (resultDict.Count > 0)
            {
                _loggerWindowView.Resources.MergedDictionaries.Add(resultDict);
            }
        }
    }
}
