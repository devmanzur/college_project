using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Snapkart.Domain.Constants;

namespace Snapkart.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public AppUser(UserRole role,string name, string phoneNumber, string address, string imageUrl)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            ImageUrl = imageUrl;
            Role = role;
            ApprovalStatus = ApprovalStatus.Pending;
            UserName = phoneNumber;
        }

        public string Name { get; private set; }
        public string Address { get; private set; }
        public string ImageUrl { get; private set; }

        public UserRole Role { get; private set; }
        public ApprovalStatus ApprovalStatus { get; private set; }
        public List<AppNotification> Notifications { get; set; }
        public List<Category> Subscriptions { get; set; }
    }
}