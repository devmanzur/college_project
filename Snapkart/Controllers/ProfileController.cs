using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Snapkart.Contract;
using Snapkart.Domain.Dto.Request;
using Snapkart.Domain.Interfaces;

namespace Snapkart.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProfileController : AuthorizedEndpoint
    {
        private readonly IAppUserService _appUserService;

        public ProfileController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var user = new SignedInUser()
            {
                Claims = User
            };
            var profile = await _appUserService.GetProfile(user);
            return Ok(Envelope.Ok(profile));
        }
    }
}