using System;
using System.Collections.Generic;

namespace Snapkart.Domain.Entities
{
    public class SnapQuery: BaseEntity
    {
        public SnapQuery(string description, string imageUrl, List<Tag> tags)
        {
            Description = description;
            ImageUrl = imageUrl;
            Tags = tags;
            CreatedAt = DateTimeOffset.UtcNow;
            Bids = new List<Bid>();
        }

        public string Description { get; private set; }
        public string ImageUrl { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public List<Tag> Tags { get; private set; }
        public List<Bid> Bids { get; private set; }
        
    }
}