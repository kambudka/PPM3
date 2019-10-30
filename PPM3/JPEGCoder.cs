namespace PPM3
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Text;
    using System.Windows.Media.Imaging;

    public class JPEGCoder
    {
        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static void SaveImage(BitmapImage image, int quality)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            switch(quality)
            {
                case 1:
                    encoder.QualityLevel = 1;
                    break;
                case 2:
                    encoder.QualityLevel = 25;
                    break;
                case 3:
                    encoder.QualityLevel = 50;
                    break;
                case 4:
                    encoder.QualityLevel = 75;
                    break;
                case 5:
                    encoder.QualityLevel = 100;
                    break;
                default:
                    encoder.QualityLevel = 1;
                    break;
            }      
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (var filestream = new FileStream(@"C:\sc9_install\myfile.jpeg", FileMode.Create))
                encoder.Save(filestream);
        }
    }
}
