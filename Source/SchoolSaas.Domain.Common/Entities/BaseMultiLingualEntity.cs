namespace SchoolSaas.Domain.Common.Entities
{
    public abstract class BaseMultiLingualEntity<TTranslation, TId> : BaseEntity<TId>,
        IMultiLingualEntity<TTranslation>
        where TTranslation : class, IEntityTranslation
    {
        public List<TTranslation> Translations { get; set; } = new List<TTranslation>();
    }

    public abstract class
        BaseMultiLingualEntity<TTranslation> : BaseMultiLingualEntity<TTranslation, Guid>
        where TTranslation : class, IEntityTranslation
    {
    }
}