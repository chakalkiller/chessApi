using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        [HttpGet]
        public IActionResult Jacky()
        {
            return NoContent();
        }
    }
}
