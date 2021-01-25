using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Snapkart.Contract;
using Snapkart.Domain.Dto.Request;
using Snapkart.Domain.Interfaces;
using Snapkart.Helpers;

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

        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromForm] UserSignInDto dto)
        {
            var validator = new UserSignInDtoValidator();
            var validate = await validator.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return BadRequest(Envelope.Error(validate.Errors.FirstOrDefault()?.ErrorMessage));
            }

            var signIn = await _appUserService.SignIn(dto);
            if (signIn.IsSuccess)
            {
                var token = await TokenIssuer.GenerateToken(signIn.Value);
                return Ok(Envelope.Ok(token));
            }

            return BadRequest(Envelope.Error(signIn.Error));
        }

        [HttpPost("customer")]
        public async Task<IActionResult> RegisterCustomer([FromForm] CustomerRegisterDto dto)
        {
            var validator = new CustomerRegisterDtoValidator();
            var validate = await validator.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return BadRequest(Envelope.Error(validate.Errors.FirstOrDefault()?.ErrorMessage));
            }
            
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
            
            var validator = new MerchantRegisterDtoValidator();
            var validate = await validator.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return BadRequest(Envelope.Error(validate.Errors.FirstOrDefault()?.ErrorMessage));
            }
            
            var registration = await _appUserService.RegisterMerchant(dto);
            if (registration.IsSuccess)
            {
                return Ok(Envelope.Ok(registration.Value));
            }

            return BadRequest(Envelope.Error(registration.Error));
        }
    }
}