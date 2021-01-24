using System.Collections.Generic;

namespace Snapkart.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<UserSubscription> Subscriptions { get; set; }
        public List<int> TagIds { get; set; }

    }
}