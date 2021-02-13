using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Snapkart.Domain.Constants;

namespace Snapkart.Domain.Entities
{
    public class AppUser : IdentityUser, IIdentifiable<string>
    {
        public AppUser(UserRole role, string name, string phoneNumber, string address, string imageUrl)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            ImageUrl = imageUrl;
            Role = role;
            ApprovalStatus = ApprovalStatus.Pending;
            UserName = phoneNumber;
            Subscriptions = new List<UserSubscription>();
        }

        public string Name { get; private set; }
        public string Address { get; private set; }
        public string ImageUrl { get; private set; }
        public int CityId { get; private set; }
        public int AreaId { get; private set; }

        public UserRole Role { get; private set; }
        public ApprovalStatus ApprovalStatus { get; private set; }
        public List<AppNotification> Notifications { get; set; } = new List<AppNotification>();
        public List<UserSubscription> Subscriptions { get; set; }
        public List<Bid> Bids { get; set; } = new List<Bid>();
        public List<SnapQuery> Queries { get; set; } = new List<SnapQuery>();

        public void AddSubscriptions(List<int> subscriptionIds)
        {
            subscriptionIds.ForEach(x => Subscriptions.Add(new UserSubscription() {CategoryId = x}));
        }

        public Result Add(Bid bid)
        {
            if (ApprovalStatus == ApprovalStatus.Approved)
            {
                Bids.Add(bid);
                return Result.Success();
            }
            return Result.Failure("your account is not approved");
        }

        public void AddNotification(AppNotification appNotification)
        {
            Notifications.Add(appNotification);
        }

        public void SetArea(int cityCode, int areaCode)
        {
            CityId = cityCode;
            AreaId = areaCode;
        }

        public void Add(SnapQuery query)
        {
            Queries.Add(query);
        }
    }
}