using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Mvc;

namespace TestWebApp.Controllers
{
    public class DefaultController : Controller
    {
        private static ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        private static bool stopped = false;

        // GET: Default
        public ActionResult Index()
        {
            Response.Buffer = false;
            while(!stopped)
            {
                string matrix;
                if (queue.TryDequeue(out matrix))
                {                   
                    Response.Output.WriteLine(matrix);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
            Response.End();
            return View();
        }

        public ActionResult PlayVideo(string url)
        {
            var client = new WebClient();
            var gifImg = Image.FromStream(client.OpenRead(url));
            var dimension = new FrameDimension(gifImg.FrameDimensionsList[0]);
            // Number of frames
            int frameCount = gifImg.GetFrameCount(dimension);
            // Return an Image at a certain index
            for (var i = 0; i < frameCount; i++)
            {
                gifImg.SelectActiveFrame(dimension, i);
                var thumb = gifImg.GetThumbnailImage(32, 32, null, IntPtr.Zero);
                var bitmap = new Bitmap(thumb);
                var builder = new StringBuilder();
                for (var x = 0; x < bitmap.Width; x++)
                {
                    for (var y = 0; y < bitmap.Height; y++)
                    {
                        var pixel = bitmap.GetPixel(x, y);
                        builder.AppendFormat("{0}{1}{2}", pixel.R, pixel.G, pixel.B);
                    }   
                }
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult SetMatrix()
        {            
            var input = new StreamReader(Request.InputStream).ReadToEnd();
            //var matrix = JsonConvert.DeserializeObject<int[][]>(input);
            queue.Enqueue(input);
            return View("Index");
        }

        public ActionResult Stop()
        {
            stopped = true;
            Thread.Sleep(200);
            stopped = false;
            return View("Index");
        }
    }
}