using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Snapkart.Contract;
using Snapkart.Domain.Dto.Request;
using Snapkart.Domain.Interfaces;

namespace Snapkart.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAppUserService _appUserService;

        public AccountController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [HttpPost("customer")]
        public async Task<IActionResult> RegisterCustomer([FromForm] CustomerRegisterDto dto)
        {
            var registration = await _appUserService.RegisterCustomer(dto);
            if (registration.IsSuccess)
            {
                return Ok(Envelope.Ok(registration.Value));
            }

            return BadRequest(Envelope.Error(registration.Error));
        }

        [HttpPost("merchant")]
        public async Task<IActionResult> RegisterMerchant([FromForm] MerchantRegisterDto dto)
        {
            var registration = await _appUserService.RegisterMerchant(dto);
            if (registration.IsSuccess)
            {
                return Ok(Envelope.Ok(registration.Value));
            }

            return BadRequest(Envelope.Error(registration.Error));
        }
    }
}