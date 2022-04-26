using System.Windows;

namespace ClassLibrary1.WpfHelper
{
    public class WindowOpener
    {
        static Window currentWindow;
        public void Show(Window window)
        {
            if (currentWindow is null)
            {
                currentWindow = window;
                currentWindow.Show();
                currentWindow.Closed += (s, e) => currentWindow = null;
            }
            else
            {
                currentWindow.WindowState = WindowState.Maximized;
                currentWindow.WindowState = WindowState.Normal;
            }
        }
    }
}
