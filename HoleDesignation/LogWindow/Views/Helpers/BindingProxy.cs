namespace LogWindow.Views.Helpers
{
    using System.Windows;

    /// <inheritdoc />
    public class BindingProxy : Freezable
    {
        /// <summary>
        /// DependencyProperty для хранения Data
        /// </summary>
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));

        /// <summary>
        /// Данные для привязки
        /// </summary>
        public object Data
        {
            get => GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        /// <inheritdoc />
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }
    }
}
