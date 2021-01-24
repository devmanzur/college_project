using Snapkart.Domain.Entities;

namespace Snapkart.Domain.Dto.Request
{
    public class TagDto
    {
        public TagDto(Tag tag)
        {
            Id = tag.Id;
            Name = tag.Name;
        }

        public int Id { get;private set; }
        public string Name { get;private set; }
    }
}