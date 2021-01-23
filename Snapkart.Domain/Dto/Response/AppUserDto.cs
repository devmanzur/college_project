using Snapkart.Domain.Constants;
using Snapkart.Domain.Entities;

namespace Snapkart.Domain.Dto.Response
{
    public class AppUserDto
    {
        public AppUserDto(AppUser user)
        {
            Id = user.Id;
            Name = user.Name;
            PhoneNumber = user.PhoneNumber;
            Role = user.Role.ToString();
            Status = user.ApprovalStatus.ToString();
        }

        public string Status { get; set; }

        public string Role { get; set; }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }
    }
}