namespace SchoolSaas.Domain.Common.Entities
{
    public interface IMultiLingualEntity<TTranslation>
        where TTranslation : class, IEntityTranslation
    {
        List<TTranslation> Translations { get; set; }
    }
}