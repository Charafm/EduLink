using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.Constants;

namespace SchoolSaas.Infrastructure.Common.Services
{
    public class NullTenantAccessor : ITenantAccessor
    {
        public string? GetTenantId() => AuthorizationConstants.DefaultBackofficeId;
    }
}