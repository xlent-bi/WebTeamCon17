using System;
using System.Collections.Generic;
using Api.Models;
using Render;

namespace Api.Displays
{
    public static class DisplayHandler
    {
        private static readonly IDictionary<string, Display> AllDisplays = new Dictionary<string, Display>();


        internal static void Register(Display display)
        {
            AllDisplays.Remove(display.Id);
            AllDisplays.Add(display.Id, display);
        }

        public static void RenderText(string displayId, string text)
        {
            // TODO: Take font and colors as arguments
            var display = AllDisplays[displayId];
            if (display == null) throw new ArgumentNullException($"No display setup for '{displayId}'");
            var textToRender = BitmapSerializer.Serialize(display.TextRenderer.Render(text));
            display.Queue.Enqueue(textToRender);
        }
    }
}