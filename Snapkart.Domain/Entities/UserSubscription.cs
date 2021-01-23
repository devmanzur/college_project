namespace Snapkart.Domain.Entities
{
    public class UserSubscription : BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}