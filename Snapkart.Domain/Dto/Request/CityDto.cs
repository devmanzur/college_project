using System.Collections.Generic;
using System.Linq;
using Snapkart.Domain.Entities;

namespace Snapkart.Domain.Dto.Request
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AreaDto> Areas { get; set; }

        public CityDto(City city)
        {
            Id = city.Id;
            Name = city.Name;
            Areas = city.Areas.Select(x => new AreaDto(x)).ToList();
        }
    }
}