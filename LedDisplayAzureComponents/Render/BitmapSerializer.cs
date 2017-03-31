using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Render
{
    public class BitmapSerializer
    {
        /// <summary>
        /// Serializes a bitmap into a commaseparated string of web-colors
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public string Serialize(Bitmap bitmap)
        {
            var window = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            var data = bitmap.LockBits(window, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var bufferSize = data.Stride * data.Height;
            var buffer = new byte[bufferSize];

            Marshal.Copy(data.Scan0, buffer, 0, bufferSize);

            return string.Join(",", buffer.Group(3).Select(ToHexGroup));
        }

        private static string ToHexGroup(IEnumerable<byte> values)
        {
            return string.Join(string.Empty, values.Select(ToHex));
        }

        private static string ToHex(byte value)
        {
            return value.ToString("X").PadLeft(2, '0');
        }
    }
}
