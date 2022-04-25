using System;
using System.Threading;

namespace GENPRO_Design.DialogWindow
{
    public class GenproProgressBar
    {
        internal static EventWaitHandle _progressWindowWaitHandle;
        private readonly Thread _newprogWindowThread;
        private GenproProgressBarWindow ProgressBarWindow { get; set; }
        private readonly int _total;

        public delegate void ClosedHandler();
        public event ClosedHandler Cancel;

        public bool IsCancelled { get; private set; } = false;

        public GenproProgressBar(int total)
        {
            _total = total;

            if (_total == 0)
                return;

            using (_progressWindowWaitHandle = new AutoResetEvent(false))
            {
                _newprogWindowThread = new Thread(new ThreadStart(ShowProgressBar));
                _newprogWindowThread.SetApartmentState(ApartmentState.STA);
                _newprogWindowThread.IsBackground = true;
                _newprogWindowThread.Start();

                _progressWindowWaitHandle.WaitOne();
            }
        }
        private void ShowProgressBar()
        {
            if (_total == 0)
                return;

            ProgressBarWindow = new GenproProgressBarWindow();
            ProgressBarWindow.Show();

            ProgressBarWindow.Canceled += ProgressBarWindow_Cenceled;

            System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(new Func<bool>(_progressWindowWaitHandle.Set));
            System.Windows.Threading.Dispatcher.Run();

        }

        private void ProgressBarWindow_Cenceled()
        {
            if (GenproWindow.Question("Вы уверены, что хотите отменить операцию?"))
            {
                Cancel?.Invoke();
                ProgressBarWindow.Close();
                IsCancelled = true;
            }
        }

        public void Update(int current, string message = null)
        {
            ProgressBarWindow.UpdateProgress(current, _total, message);
        }
    }
}

