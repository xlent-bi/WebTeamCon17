using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Render
{
    public class TextRenderer
    {
        /// <summary>
        /// Background color to render text over
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Color to use for the text
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// The font to use when rendering text
        /// </summary>
        public Font Font { get; set; }

        private readonly int _width;
        private readonly int _height;

        /// <summary>
        /// Creates a new instance of TextRenderer that renders to a canvas of width x height.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public TextRenderer(int width, int height)
        {
            BackgroundColor = Color.Black;
            TextColor = Color.White;
            Font = new Font("Consolas", 7);
            _width = width;
            _height = height;
        }

        /// <summary>
        /// Renders text and returns a bitmap.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Bitmap Render(string text)
        {
            var bitmap = new Bitmap(_width, _height, PixelFormat.Format24bppRgb);
            var graphics = Graphics.FromImage(bitmap);
            var canvas = new RectangleF(0, 0, bitmap.Width, bitmap.Height);

            graphics.Clear(BackgroundColor);
            graphics.DrawString(text, Font, new SolidBrush(TextColor), canvas);

            return bitmap;
        }
    }
}
