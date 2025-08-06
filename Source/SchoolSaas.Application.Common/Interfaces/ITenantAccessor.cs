namespace SchoolSaas.Application.Common.Interfaces
{
    public interface ITenantAccessor
    {
        string? GetTenantId();
    }
}