using System.Linq;
using System.Security.Claims;

namespace Snapkart.Domain.Extensions
{
    public static class ClaimExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claims)
        {
            return claims.Claims.Where(c => c.Type == ClaimTypes.Sid)
                .Select(c => c.Value).SingleOrDefault();
        }
        
        public static string GetUsername(this ClaimsPrincipal claims)
        {
            return claims.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => c.Value).SingleOrDefault();
        }
        
        public static string GetUserRole(this ClaimsPrincipal claims)
        {
            return claims.Claims.Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).SingleOrDefault();
        }
    }
}