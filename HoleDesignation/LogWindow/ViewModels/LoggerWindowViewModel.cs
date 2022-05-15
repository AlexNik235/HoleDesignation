namespace LogWindow.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;
    using Abstractions;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GENPRO_Design.DialogWindow;

    /// <inheritdoc />
    public class LoggerWindowViewModel : ViewModelBase
    {
        private readonly ILogStorage _logStorage;
        private readonly IElementsDisplay _elementsDisplay;
        private readonly ILogSaver _logSaver;

        private bool _isShowElement;
        private string _title;

        /// <inheritdoc />
        public LoggerWindowViewModel(
            IElementsDisplay elementsDisplay,
            ILogSaver logSaver,
            ILogStorage logStorage)
        {
            _elementsDisplay = elementsDisplay;
            _logSaver = logSaver;
            _logStorage = logStorage;
            _logStorage.ElementStorageChanged += LogStorageOnLogStorageChanged;
        }

        /// <summary>
        /// Заголовок окна.
        /// </summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        /// <summary>
        /// Показывать элемент.
        /// </summary>
        public bool IsShowElement
        {
            get => _isShowElement;
            set
            {
                _isShowElement = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Коллекция ошибок.
        /// </summary>
        public ObservableCollection<BaseMessage> Messages { get; } = new ();

        /// <summary>
        /// Выбирает элемент по Id.
        /// </summary>
        public ICommand SelectedIdCommand => new RelayCommand<IBaseObject>(objectBase =>
        {
            try
            {
                var elementId = (int)objectBase.GetId();

                _elementsDisplay.SetSelectedElement(elementId);
                if (_isShowElement)
                    _elementsDisplay.ZoomElement(elementId, 1);
            }
            catch (Exception ex)
            {
                GenproWindow.Error(
                    "Выделение элемента.",
                    $"Произошла ошибка при выделении элемента.\nОшибка: {ex}");
            }
        });

        /// <summary>
        /// Копирует Id элемента.
        /// </summary>
        public ICommand CopyIdCommand => new RelayCommand<IBaseObject>(objectBase =>
        {
            try
            {
                Clipboard.SetText(objectBase.ToString());
            }
            catch
            {
                // ignored
            }
        });

        /// <summary>
        /// Запускает блокнот, в котором отображается лог.
        /// </summary>
        public ICommand OpenNotepadCommand => new RelayCommand(() =>
        {
            try
            {
                var errors = _logSaver.SaveToString(Messages);
                _logSaver.ShowTextWithNotepad(errors);
            }
            catch (Exception ex)
            {
                GenproWindow.Error(
                    "Открыть в блокноте.",
                    $"Произошла ошибка при открытии лога в блокноте. \nОшибка: {ex}");
            }
        });

        private void LogStorageOnLogStorageChanged(object sender, EventArgs e) => GetMessages();

        private void GetMessages()
        {
            Messages.Clear();
            foreach (var item in _logStorage.GetMessages())
                Messages.Add(item);
        }
    }
}
