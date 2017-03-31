using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Api.Displays;
using Api.Models;
using Render;

namespace Api.Controllers
{
    public class ConnectionsController : Controller
    {
        private static readonly List<string> LiveConnections = new List<string>();

        [HttpPost]
        public void Create(string displayId, int width, int height)
        {
            var display = new Display
            {
                Id = displayId,
                Width = width,
                Height = height,
                TextRenderer = new TextRenderer(width, height)
            };
            DisplayHandler.Register(display);
            LiveConnections.Add(displayId);

            Response.Buffer = false;
            while (LiveConnections.Contains(displayId))
            {
                if (display.Queue.Any())
                {
                    string text = display.Queue.Dequeue();
                    Response.Output.Write(text);
                    Response.Output.Flush();
                }
                Thread.Sleep(900);
            }
            Response.End();
        }

        [HttpPost]
        public void Destroy(string displayId)
        {
            LiveConnections.Remove(displayId);
        }
    }
}