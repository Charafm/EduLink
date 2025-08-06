namespace SchoolSaas.Domain.Common.Entities
{
    public interface IEntityTranslation
    {
        string LanguageCode { get; set; }
    }

    public interface IEntityTranslation<TEntity, TKey> : IEntityTranslation
    {
        TEntity? Parent { get; set; }
    }

    public interface IEntityTranslation<TEntity> : IEntityTranslation<TEntity, Guid>
    {
    }
}