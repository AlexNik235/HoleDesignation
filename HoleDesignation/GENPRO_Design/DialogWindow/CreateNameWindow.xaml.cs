using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GENPRO_Design.DialogWindow
{
    /// <summary>
    /// Логика взаимодействия для CreateNameWindow.xaml
    /// </summary>
    public partial class CreateNameWindow : Window
    {
        public string NewName { get; set; }
        List<string> _listExistNames;
        public CreateNameWindow(List<string> listExistNames)
        {
            ColorZoneAssist.SetMode(new GroupBox(), ColorZoneMode.Custom);
            Hue hue = new Hue("name", System.Windows.Media.Color.FromArgb(1, 2, 3, 4), System.Windows.Media.Color.FromArgb(1, 5, 6, 7));
            
            InitializeComponent();
            _listExistNames = listExistNames;
            ButtonApply.IsEnabled = false;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            NewName = TextBoxNewName.Text;
            this.DialogResult = true;
        }

        private void TextBoxNewName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonApply.IsEnabled = false;
            string newName = TextBoxNewName.Text;
            string erroeMessage;

            ButtonApply.IsEnabled = Validator.ValidateName(newName, 2, 100, out erroeMessage, _listExistNames);
            LabelErrorWrite.Text = erroeMessage;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            

            this.DragMove();
        }
    }
}
