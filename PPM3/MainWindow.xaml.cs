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
    using Microsoft.Win32;

    public partial class MainWindow : Window
    {
        private BitmapImage map;
        System.Drawing.Bitmap bitmaptosave;
        public MainWindow()
        {
            InitializeComponent();

        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.ppm;* |" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png|"+
              "Portable Pixel Map (*.ppm)|*.ppm";
            if (op.ShowDialog() == true)
            {
                string ext = System.IO.Path.GetExtension(op.FileName);
                if (ext == ".ppm")
                    ppmFile(op.FileName);
                else
                {

                    map = new BitmapImage(new Uri(op.FileName));
                    Image.Source = map;
                }
                    
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using(MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(map));
                enc.Save(outStream);
                bitmaptosave = new System.Drawing.Bitmap(outStream);   
            }
            JPEGCoder.SaveImage(map, Box.SelectedIndex);
            MessageBox.Show("File Saved");
        }


        private void ppmFile(string fileName)
        {
            Bitmap image = PPMReader.ReadBitmapFromPPM(fileName);
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
            map = input;
        }
    }
}
