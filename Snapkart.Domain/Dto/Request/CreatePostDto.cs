using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Snapkart.Domain.Dto.Request
{
    public class CreatePostDto
    {
        public string Details { get; set; }
        public IFormFile Image{ get; set; }
        public int CategoryId { get; set; }
        public List<int> TagIds { get; set; }
    }
}