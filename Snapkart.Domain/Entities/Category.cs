using System.Collections.Generic;

namespace Snapkart.Domain.Entities
{
    public class Category : BaseEntity
    {
        public Category(string name, List<int> tagIds)
        {
            Name = name;
            TagIds = tagIds;
        }

        public string Name { get; private set; }
        public List<UserSubscription> Subscriptions { get; set; }
        public List<int> TagIds { get;private set; }

    }
}