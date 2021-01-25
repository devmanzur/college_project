using Snapkart.Domain.Entities;

namespace Snapkart.Domain.Dto.Response
{
    public class ContactDetailResponseDto
    {
        public ContactDetailResponseDto(AppUser user)
        {
            PhoneNumber = user.PhoneNumber;
            Name = user.Name;
            Address = user.Address;
        }

        public string Address { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}