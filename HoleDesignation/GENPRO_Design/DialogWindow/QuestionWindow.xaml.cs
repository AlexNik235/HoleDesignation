using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using GENPRO_Design.WpfHelper;

namespace GENPRO_Design.DialogWindow
{
    /// <summary>
    /// Логика взаимодействия для QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow : Window
    {
        public DialogResult Result;
        
        public QuestionWindow(string question = null, string caption = null)
        {
            MaterialDesignTools.SetUp();

            InitializeComponent();

            if (!string.IsNullOrWhiteSpace(question))
                QuestionTextBlock.Text = question;

            if (!string.IsNullOrWhiteSpace(caption))
                TextBlockCaption.Text = caption;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Result = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            Result = System.Windows.Forms.DialogResult.No;
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Result = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
