namespace Snapkart.Domain.Entities
{
    public class AppNotification : BaseEntity
    {
        public AppUser User { get; set; }
        public string UserId { get; set; }
        public string Subject { get; private set; }
        public int SubjectId { get; private set; }
        public string Message { get; private set; }

        protected AppNotification()
        {
        }

        public AppNotification(string subject, int subjectId, string message)
        {
            Subject = subject;
            Message = message;
            SubjectId = subjectId;
        }
    }
}