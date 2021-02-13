using System;
using Snapkart.Domain.Entities;

namespace Snapkart.Domain.Dto.Response
{
    public class BidResponseDto
    {
        public DateTimeOffset CreatedAt { get; private set; }
        public string ImageUrl { get; private set; }
        public string Description { get; private set; }
        public string CreatedBy { get; private set; }
        public string UserImageUrl { get; private set; }
        public decimal Price { get; private set; }
        public int Id { get; set; }

        public BidResponseDto(Bid bid)
        {
            CreatedBy = bid.Maker.Name;
            UserImageUrl = bid.Maker.ImageUrl;
            Id = bid.Id;
            CreatedAt = bid.CreatedAt;
            ImageUrl = bid.ImageUrl;
            Description = bid.Description;
            Price = bid.Price;
        }
    }
}