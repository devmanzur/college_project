using System;
using Snapkart.Domain.Entities;

namespace Snapkart.Domain.Dto.Request
{
    public class BidDto
    {
        public DateTimeOffset CreatedAt { get; private set; }
        public string ImageUrl { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Id { get; set; }

        public BidDto(Bid bid)
        {
            Id = bid.Id;
            CreatedAt = bid.CreatedAt;
            ImageUrl = bid.ImageUrl;
            Description = bid.Description;
            Price = bid.Price;
        }
    }
}