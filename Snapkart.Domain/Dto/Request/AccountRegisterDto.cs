using Microsoft.AspNetCore.Http;

namespace Snapkart.Domain.Dto.Request
{
    public class AccountRegisterDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public IFormFile Image { get; set; }
        public string Password { get; set; }
    }
}