using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Snapkart.Domain.Dto.Request;
using Snapkart.Domain.Interfaces;

namespace Snapkart.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public AccountController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAccount([FromForm] AccountRegisterDto dto)
        {
            var result = await _appUserService.RegisterCustomer(dto);
            return Ok(result);
        }
    }
}