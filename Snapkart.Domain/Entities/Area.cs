namespace Snapkart.Domain.Entities
{
    public class Area : BaseEntity
    {
        public Area(string name)
        {
            Name = name;
        }

        public int CityId { get; set; }
        public City City { get; set; }
        public string Name { get; private set; }
    }
}