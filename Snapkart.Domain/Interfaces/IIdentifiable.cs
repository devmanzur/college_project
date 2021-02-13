namespace Snapkart.Domain.Entities
{
    public interface IIdentifiable<T>
    {
        public T Id { get; set; }

    }
}