namespace SchoolSaas.Domain.Common.Entities
{
    public class MessageBody : BaseEntity<Guid>
    {
        public string? Subject { get; set; }
        public string Content { get; set; }
        public List<Message>? Messages { get; set; }
    }
}