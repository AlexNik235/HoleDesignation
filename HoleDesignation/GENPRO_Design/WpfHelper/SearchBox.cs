using System;
using System.Windows.Controls;

namespace GENPRO_Design.WpfHelper
{
    public static class SearchBox
    {
        public static void SetUpPlaceHolder(TextBox textBox, string placeHolderText)
        {
            textBox.Text = placeHolderText;
            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == placeHolderText)
                    textBox.Text = "";
            };
            textBox.LostFocus += (s, e) =>
            {
                if (String.IsNullOrEmpty(textBox.Text))
                    textBox.Text = placeHolderText;
            };
        }
    }
}