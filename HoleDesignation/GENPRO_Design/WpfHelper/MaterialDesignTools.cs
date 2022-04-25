using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;

namespace GENPRO_Design.WpfHelper
{
    public static class MaterialDesignTools
    {
        public static void SetUp()
        {
            ColorZoneAssist.SetMode(new GroupBox(), ColorZoneMode.Custom);
            new Hue("name", System.Windows.Media.Color.FromArgb(1, 2, 3, 4), System.Windows.Media.Color.FromArgb(1, 5, 6, 7));
        }
    }
}