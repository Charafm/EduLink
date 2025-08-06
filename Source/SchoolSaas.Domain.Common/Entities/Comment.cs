namespace SchoolSaas.Domain.Common.Entities
{
    public class Comment : BaseEntity<Guid>
    {
      
        public Guid? TargetEntityId { get; set; } 
        public List<CommentBody> Body { get; set; }
    }
}
