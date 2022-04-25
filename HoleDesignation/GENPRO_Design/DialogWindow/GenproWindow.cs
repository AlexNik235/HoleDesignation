using System.Collections.Generic;
using System.Windows.Forms;

namespace GENPRO_Design.DialogWindow
{
    public static class GenproWindow
    {
        public static bool CreateName(List<string> listExistNames, out string newName)
        {
            CreateNameWindow window = new CreateNameWindow(listExistNames);
            bool resDialog = window.ShowDialog().Value;
            newName = window.NewName;
            return resDialog;
        }

        public static bool Rename(string oldName, List<string> listExistNames, out string newName)
        {
            RenameWindow window = new RenameWindow(oldName, listExistNames);
            bool resDialog = window.ShowDialog().Value;
            newName = window.NewName;
            return resDialog;
        }

        public static GenproProgressBar ProgressBar(int total)
        {
            return new GenproProgressBar(total);
        }
        public static bool Question(string questionText = null, string caption = null)
        {
            var window = new QuestionWindow(questionText, caption);
            window.ShowDialog();

            return window.Result == DialogResult.Yes;
        }
        
        public static DialogResult QuestionResult(string questionText = null, string caption = null)
        {
            var window = new QuestionWindow(questionText, caption);
            window.ShowDialog();

            return window.Result;
        }

        public static void Warning(string content, string caption = null)
        {
            new CommonInfoWindow(content, StatusType.Warning, caption).ShowDialog();
        }
        public static void Error(string content, string caption = null)
        {
            new CommonInfoWindow(content, StatusType.Error, caption).ShowDialog();
        }
        public static void Information(string content, string caption = null)
        {
            new CommonInfoWindow(content, StatusType.Info, caption).ShowDialog();
        }
        public static void Custom(string content, string caption, object icon, string title)
        {
            new CommonInfoWindow(content, StatusType.Custom, caption, icon, title).ShowDialog();
        }

    }
}
