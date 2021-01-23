namespace Snapkart.Domain.Entities
{
    public class Tag: BaseEntity
    {
        public Tag(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}