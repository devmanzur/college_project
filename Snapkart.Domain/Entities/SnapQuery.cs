using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;

namespace Snapkart.Domain.Entities
{
    public class SnapQuery : BaseEntity
    {
        protected SnapQuery()
        {
        }

        public SnapQuery(string createdBy, string description, string imageUrl, int categoryId, List<int> tags,
            int areaId, int cityId)
        {
            Description = description;
            ImageUrl = imageUrl;
            CategoryId = categoryId;
            TagIds = tags;
            AreaId = areaId;
            CityId = cityId;
            CreatedAt = DateTimeOffset.UtcNow;
            Bids = new List<Bid>();
            CreatedBy = createdBy;
        }

        public string CreatedBy { get; private set; }
        public int AreaId { get; private set; }
        public int CityId { get; private set; }
        public int AcceptedBidId { get; set; }
        public string Description { get; private set; }
        public string ImageUrl { get; private set; }
        public int CategoryId { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public List<int> TagIds { get; private set; } = new List<int>();
        public List<string> Likes { get; private set; } = new List<string>();
        public List<Bid> Bids { get; private set; } = new List<Bid>();

        public void MakeBid(Bid bid)
        {
            Bids.Add(bid);
        }

        public Result<AppUser> Accept(string userId, int bidId)
        {
            if (userId == CreatedBy)
            {
                var bid = Bids.FirstOrDefault(x => x.Id == bidId);
                if (bid == null) return Result.Failure<AppUser>("bid not found");
                AcceptedBidId = bid.Id;
                return Result.Success(bid.Maker);
            }

            return Result.Failure<AppUser>("you are not allowed to accept bids for this query");
        }

        public void LikedBy(string userId)
        {
            if (Likes == null)
            {
                Likes = new List<string>();
            }
            Likes.Add(userId);
        }
    }
}