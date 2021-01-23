namespace Snapkart.Domain.Entities
{
    public class AppNotification : BaseEntity
    {
        public AppUser Recipient { get; set; }
        public long RecipientId { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}