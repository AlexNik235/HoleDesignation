using GENPRO_Design.WpfHelper;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GENPRO_Design.DialogWindow
{
    /// <summary>
    /// Логика взаимодействия для GenproProgressBar.xaml
    /// </summary>
    public partial class GenproProgressBarWindow : Window
    {
        public delegate void OperationCanceled();
        public event OperationCanceled Canceled;
        public GenproProgressBarWindow()
        {
            MaterialDesignTools.SetUp();
            InitializeComponent();
        }
        public void UpdateProgress(int current, int total, string message)
        {
            this.Dispatcher.Invoke(new Action<string, int, int>(

            delegate (string m, int v, int t)
            {
                this.progressBar.Maximum = System.Convert.ToDouble(t);
                this.progressBar.Value = System.Convert.ToDouble(v);
                if (!string.IsNullOrWhiteSpace(message))
                    this.messageLbl.Text = m;                
                this.TextBlockProgressBar.Text = $"{Convert.ToInt32(Math.Round(Convert.ToDouble(current * 100) / total))}/100%";                
            }),
            System.Windows.Threading.DispatcherPriority.Background,
            message, current, total);            

            if (current == total)
                this.Dispatcher.Invoke(new Action(this.Close));
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Cancel();            
        }

        private void Cancel()
        {
            Canceled?.Invoke();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
