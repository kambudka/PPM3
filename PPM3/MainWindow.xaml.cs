namespace PPM3
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using System.IO;
    using System.Drawing.Imaging;
    using System.Windows.Media.Imaging;

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); ;
            Bitmap image = PPMReader.ReadBitmapFromPPM(@"C:\Users\kbudk\Downloads\bell_206.ppm");

            BitmapImage input;

            using (var memory = new MemoryStream())
            {
                image.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                input = bitmapImage;
            }

            Image.Source = input;
        }
    }
}
