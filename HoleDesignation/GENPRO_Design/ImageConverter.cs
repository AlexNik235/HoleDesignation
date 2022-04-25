using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace GENPRO_Design
{
    public static class ImageConverter
    {
        public static BitmapImage Convert(object value)
        {
            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            byte[] val = (byte[])converter.ConvertTo(value, typeof(byte[]));
            BitmapImage exist = null;
            using (MemoryStream byteStream = new MemoryStream(val))
            {
                BitmapImage ko = new BitmapImage();
                ko.BeginInit();
                ko.CacheOption = BitmapCacheOption.OnLoad;
                ko.StreamSource = byteStream;
                ko.EndInit();
                exist = ko;
                byteStream.Close();
            }
            return exist;
        }

        public static Image ResizeImage(Image imgToResize, int newSize)
        {
            return new Bitmap(imgToResize, new Size(newSize, newSize));
        }
    }
}
