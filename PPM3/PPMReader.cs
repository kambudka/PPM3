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
            string widths = "", heights = "", red = "", green = "", blue = "";
            char temp = '2';
            char number;

            var reader = new BinaryReader(new FileStream(file, FileMode.Open));
            if (reader.ReadChar() != 'P')
                return null;

            number = reader.ReadChar();

            if(number != '6')
            {
                if(number != '3')
                    return null;
            }
                

            reader.ReadChar();

            if (reader.ReadChar() == '#')
                while (temp != '\n')
                    temp = reader.ReadChar();

            temp = '2';
            while (temp != '\n')
            {
                temp = reader.ReadChar();

                if (widths == "")
                    while (temp != ' ')
                    {
                    widths += temp;
                    temp = reader.ReadChar();
                    }
                    
                if(widths != "")
                    while (temp != ' ')
                    {
                        heights += temp;
                        temp = reader.ReadChar();
                        if (temp == '\n')
                            break;
                    }
            }
            
            while((temp = reader.ReadChar()) != '\n')
            {
                //if (reader.ReadChar() != '2' || reader.ReadChar() != '5' || reader.ReadChar() != '5')
                //    return null;
            }

            int width = int.Parse(widths),
                height = int.Parse(heights);

            Bitmap bitmap = new Bitmap(width, height);

            if (number == '6')
            {
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                        bitmap.SetPixel(x, y, Color.FromArgb(reader.ReadByte(),
                            reader.ReadByte(),
                            reader.ReadByte())
                            );
            }
            else if (number == '3')
            {
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++) {
                        do
                        {

                            while ((temp = reader.ReadChar()) != ' ')
                            {
                                red += temp;
                            }
                            while ((temp = reader.ReadChar()) != ' ')
                            {
                                green += temp;
                            }
                            while ((temp = reader.ReadChar()) != ' ')
                            {
                                blue += temp;
                            }

                            bitmap.SetPixel(x, y, Color.FromArgb(int.Parse(red),
                                            int.Parse(green),
                                            int.Parse(blue)));

                            red = "";
                            green = "";
                            blue = "";
                        } while ((temp = reader.ReadChar()) != '\n');
                    }
            }

            return bitmap;
        }

    }
}
