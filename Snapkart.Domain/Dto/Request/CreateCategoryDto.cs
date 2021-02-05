using System.Collections.Generic;

namespace Snapkart.Domain.Dto.Request
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public List<int> TagIds { get; set; } = new List<int>();
    }
}