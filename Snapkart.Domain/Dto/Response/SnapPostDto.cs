using System;

namespace Snapkart.Domain.Dto.Response
{
    public class SnapPostDto
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public int AreaId { get; set; }
        public int CityId { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int Likes { get; set; }
        public int Bids { get; set; }
        public string UserImageUrl { get; set; }
        public string Location { get; set; }
    }
}