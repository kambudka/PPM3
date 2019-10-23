namespace PPM3
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;

    public class PPMReader
    {
        public static Bitmap ReadBitmapFromPPM(string file)
        {
            string widths = "", heights = "";
            char temp = '2';
            char number;

            var reader = new BinaryReader(new FileStream(file, FileMode.Open));
            if (reader.ReadChar() != 'P')
                return null;
            number = reader.ReadChar();
            if(number != '6' || number != '3')
                return null;

            reader.ReadChar(); //Eat newline

            if (reader.ReadChar() == '#')
                while (temp != '\n')
                    temp = reader.ReadChar();

            while ((temp = reader.ReadChar()) != ' ')
                widths += temp;
            while ((temp = reader.ReadChar()) >= '0' && temp <= '9')
                heights += temp;
            if (reader.ReadChar() != '2' || reader.ReadChar() != '5' || reader.ReadChar() != '5')
                return null;
            reader.ReadChar(); //Eat the last newline
            int width = int.Parse(widths),
                height = int.Parse(heights);
            Bitmap bitmap = new Bitmap(width, height);
            //Read in the pixels
            if (number == '6')
            {
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                        bitmap.SetPixel(x, y, Color.FromArgb(reader.ReadByte(),
                            reader.ReadByte(),
                            reader.ReadByte())
                            );
            }
            else if(number == '3')
            return bitmap;
        }

    }
}
