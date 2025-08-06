namespace SchoolSaas.Domain.Common.Entities
{
    public abstract class TitledEntity : BaseEntity<Guid>
    {
        public virtual string? Title { get; set; }
        public virtual string? Description { get; set; }
    }

    public abstract class TitledEntity<T> : BaseEntity<T>
    {
        public virtual string? Title { get; set; }
        public virtual string? Description { get; set; }
    }

    public abstract class TitledEntityTranslation<T> : BaseEntity<T>
    {
        public virtual string? Title { get; set; }
        public virtual string? Description { get; set; }
        public virtual string? TitleAr { get; set; }
        public virtual string? DescriptionAr { get; set; }
    }
}