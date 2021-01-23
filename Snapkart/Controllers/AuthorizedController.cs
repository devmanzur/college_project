using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Snapkart.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthorizedController : ControllerBase
    {
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> Check()
        {
            return Ok("You are verified");
        }
    }
}