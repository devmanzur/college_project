using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Snapkart.Domain.Dto.Request;
using Snapkart.Domain.Dto.Response;
using Snapkart.Domain.Entities;

namespace Snapkart.Domain.Interfaces
{
    public interface IAppUserService
    {
        Task<Result<AppUserDto>> RegisterCustomer(CustomerRegisterDto dto);

        Task<Result<AppUserDto>> RegisterMerchant(MerchantRegisterDto dto);
        Task<Result<AppUser>> SignIn(UserSignInDto dto);
        Task<AppUserDto> GetProfile(SignedInUser user);
    }
}