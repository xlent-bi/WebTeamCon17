using System.Collections.Concurrent;
using System.IO;
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