using System.Security.Claims;

namespace Snapkart.Domain.Dto.Request
{
    public class SignedInUser
    {
        public ClaimsPrincipal Claims { get; set; }
    }
}