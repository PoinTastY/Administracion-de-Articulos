using Microsoft.AspNetCore.Mvc;

namespace ArticleManagement.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExtensionController : ControllerBase
    {
        [HttpGet]
        public IActionResult Hi()
        {
            return Ok("hi");
        }
    }
}
