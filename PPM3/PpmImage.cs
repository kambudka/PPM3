namespace PPM3
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class PpmImage
    {
        public int width;
        public int height;
        public int maxVal;
        public byte[][] pixels;

        public PpmImage(int width, int height, int maxVal,
          byte[][] pixels)
        {
            this.width = width;
            this.height = height;
            this.maxVal = maxVal;
            this.pixels = pixels;
        }
    }
}
