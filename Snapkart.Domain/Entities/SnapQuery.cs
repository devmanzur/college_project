using System;
using System.Collections.Generic;
using System.Linq;

namespace Snapkart.Domain.Entities
{
    public class SnapQuery : BaseEntity
    {
        protected SnapQuery()
        {
        }

        public SnapQuery(string description, string imageUrl, int categoryId, List<int> tags)
        {
            Description = description;
            ImageUrl = imageUrl;
            CategoryId = categoryId;
            TagIds = tags;
            CreatedAt = DateTimeOffset.UtcNow;
            Bids = new List<Bid>();
        }

        public int AcceptedBidId { get; set; }
        public string Description { get; private set; }
        public string ImageUrl { get; private set; }
        public int CategoryId { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public List<int> TagIds { get; private set; }
        public List<Bid> Bids { get; private set; } = new List<Bid>();

        public void MakeBid(Bid bid)
        {
            Bids.Add(bid);
        }

        public AppUser Accept(AppUser user, int bidId)
        {
            var bid = Bids.FirstOrDefault(x => x.Id == bidId);
            if (bid == null) return null;
            AcceptedBidId = bid.Id;
            return bid.Maker;
        }
    }
}