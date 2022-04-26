using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using ClassLibrary1.WpfHelper;

namespace ClassLibrary1.DialogWindow
{
    /// <summary>
    /// Interaction logic for Input.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public DialogResult Result;
        public string Input;
        
        public InputWindow(string mainText)
        {
            MaterialDesignTools.SetUp();
            
            InitializeComponent();

            MainText.Text = mainText;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Result = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
        
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void ButtonApprove_OnClick(object sender, RoutedEventArgs e)
        {
            Result = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void InputValue_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Input = InputValue.Text;
        }
    }
}
