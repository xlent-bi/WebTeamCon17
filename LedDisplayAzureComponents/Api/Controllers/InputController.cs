using System.Web.Http;
using Api.Displays;
using Api.Models;
using Newtonsoft.Json.Linq;

namespace Api.Controllers
{
    [RoutePrefix("api/Input")]
    public class InputController : ApiController
    {
        [Route("{displayId}")]
        [HttpPost]
        public void Post([FromUri] string displayId, [FromBody] InputText input)
        {
            DisplayHandler.RenderText(displayId, input.Text);
        }
    }
}
