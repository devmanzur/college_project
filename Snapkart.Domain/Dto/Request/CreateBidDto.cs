using Microsoft.AspNetCore.Http;

namespace Snapkart.Domain.Dto.Request
{
    public class CreateBidDto
    {
        public IFormFile Image { get; set; }
        public decimal Price { get; set; }
        public string Details { get; set; }
    }
}