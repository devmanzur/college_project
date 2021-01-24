using Snapkart.Domain.Entities;

namespace Snapkart.Domain.Dto.Request
{
    public class ContactDetailDto
    {
        public ContactDetailDto(AppUser user)
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