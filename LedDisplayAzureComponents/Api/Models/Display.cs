using System.Collections.Generic;
using Render;

namespace Api.Models
{
    public class Display
    {
        public string Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public TextRenderer TextRenderer { get; set; }
        public Queue<string> Queue { get; } = new Queue<string>();
    }
}