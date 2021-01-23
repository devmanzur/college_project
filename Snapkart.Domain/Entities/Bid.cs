using System;

namespace Snapkart.Domain.Entities
{
    public class Bid : BaseEntity
    {
        public Bid(string imageUrl, string description, decimal price)
        {
            ImageUrl = imageUrl;
            Description = description;
            Price = price;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public DateTimeOffset CreatedAt { get; private set; }
        public string ImageUrl { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
    }
}