using Snapkart.Domain.Entities;

namespace Snapkart.Domain.Dto.Response
{
    public class CategoryDto
    {
        public string Name { get; private set; }
        public int Id { get; private set; }

        public CategoryDto(Category category)
        {
            Name = category.Name;
            Id = category.Id;
        }
    }
}