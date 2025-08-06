namespace SchoolSaas.Domain.Common.Entities
{
    public class BackofficeDocument : AbstractDocument, ITenantAware
    {
        public string? TenantId { get; set; }
        public Guid BackofficeId { get; set; }
        public Backoffice? Backoffice { get; set; }
    }
}
