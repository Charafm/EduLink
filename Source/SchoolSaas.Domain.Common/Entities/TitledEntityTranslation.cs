namespace SchoolSaas.Domain.Common.Entities
{
    public abstract class TitledEntityTranslation<TEntity, TId> : BaseEntityTranslation<TEntity, TId>,
        IEntityTranslation<TEntity>
    {
        public virtual string? Title { get; set; }
        public virtual string? Description { get; set; }
    }
}