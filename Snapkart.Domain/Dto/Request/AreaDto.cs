using Snapkart.Domain.Entities;

namespace Snapkart.Domain.Dto.Request
{
    public class AreaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public AreaDto(Area area)
        {
            Id = area.Id;
            Name = area.Name;
        }
    }
}