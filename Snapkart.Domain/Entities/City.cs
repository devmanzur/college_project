using System.Collections.Generic;

namespace Snapkart.Domain.Entities
{
    public class City: BaseEntity
    {
        public City(string name)
        {
            Name = name;
        }

        public void Add(Area area)
        {
            Areas.Add(area);
        }
        
        public string Name { get; private set; }
        public List<Area> Areas { get; private set; } = new List<Area>();
    }
}