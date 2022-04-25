using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GENPRO_Design.DialogWindow
{
    /// <summary>
    /// Логика взаимодействия для CommonInfoWindow.xaml
    /// </summary>
    public partial class CommonInfoWindow : Window
    {
        private StatusType _statusType;
        public CommonInfoWindow(string content, StatusType statusType, string caption = null, object customIcon = null, string title = null)
        {

            ColorZoneAssist.SetMode(new GroupBox(), ColorZoneMode.Custom);
            new Hue("name", System.Windows.Media.Color.FromArgb(1, 2, 3, 4), System.Windows.Media.Color.FromArgb(1, 5, 6, 7));

            InitializeComponent();
            _statusType = statusType;

            TextBlockContent.Text = content ?? "";
            TextBlockCaption.Text = caption ?? "";

            switch (_statusType)
            {
                case StatusType.Error:
                    {
                        this.Title = "Ошибка";
                        IconStatusType.Source = ImageConverter.Convert(Properties.GenproResource.gp_error);
                        break;
                    }
                case StatusType.Warning:
                    {
                        this.Title = "Предупреждение";
                        IconStatusType.Source = ImageConverter.Convert(Properties.GenproResource.gp_warning);
                        break;
                    }
                case StatusType.Info:
                    {
                        this.Title = "Информация";
                        IconStatusType.Source = ImageConverter.Convert(Properties.GenproResource.gp_info);
                        break;
                    }
                default:
                    {
                        this.Title = title ?? "";
                        IconStatusType.Source = ImageConverter.Convert(customIcon);
                        break;
                    }
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)        
           => this.DragMove();
        

        private void ButtonClose_Click(object sender, RoutedEventArgs e)        
           => this.Close();
        
    }
}
