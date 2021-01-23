using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Snapkart.Domain.Dto.Request;
using Snapkart.Domain.Dto.Response;
using Snapkart.Domain.Entities;

namespace Snapkart.Domain.Interfaces
{
    public interface IAppUserService
    {
        Task<Result<AppUserDto>> RegisterCustomer(AccountRegisterDto dto);
        
    }
}