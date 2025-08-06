namespace SchoolSaas.Domain.Common.Entities
{
    public interface IHasDocuments<T> where T : AbstractDocument
    {
        List<T> Documents { get; set; }
    }
}
