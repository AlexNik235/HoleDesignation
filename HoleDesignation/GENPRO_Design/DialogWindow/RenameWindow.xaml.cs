using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GENPRO_Design.DialogWindow
{
    /// <summary>
    /// Логика взаимодействия для RenameWindow.xaml
    /// </summary>
    public partial class RenameWindow : Window
    {
        List<string> _listExistNames;
        string _oldName;
        public string NewName { get; set; }
        public RenameWindow(string oldName, List<string> listExistNames)
        {
            ColorZoneAssist.SetMode(new GroupBox(), ColorZoneMode.Custom);
            Hue hue = new Hue("name", System.Windows.Media.Color.FromArgb(1, 2, 3, 4), System.Windows.Media.Color.FromArgb(1, 5, 6, 7));

            InitializeComponent();
            _oldName = oldName;
            _listExistNames = listExistNames;
            ButtonApply.IsEnabled = false;
            TextBlockOldName.Text = _oldName ?? "Старое имя не обнаружено.";
        }

        private void TextBoxNewName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ButtonApply.IsEnabled = false;
            string newName = TextBoxNewName.Text;            

            ButtonApply.IsEnabled = Validator.ValidateName(newName, 2, 100, out string erroeMessage, _listExistNames, _oldName);
            labelErrorWrite.Text = erroeMessage;
        }

        private void ButtonApply_Click(object sender, RoutedEventArgs e)
        {
            NewName = TextBoxNewName.Text;
            this.DialogResult = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
