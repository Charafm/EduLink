namespace SchoolSaas.Domain.Common.Entities
{
    public interface ITenantAware
    {
        string? TenantId { get; set; }
    }
}