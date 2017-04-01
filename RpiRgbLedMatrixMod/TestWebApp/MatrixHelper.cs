using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;

namespace TestWebApp
{
    public static class MatrixHelper
    {
        public static string BitmapToMatrix(Bitmap bitmap)
        {
            var builder = new StringBuilder();
            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    var pixel = bitmap.GetPixel(x, y);
                    builder.AppendFormat("{0:X2}{1:X2}{2:X2},", pixel.R, pixel.G, pixel.B);
                }
            }

            return builder.ToString();
        }
    }
}