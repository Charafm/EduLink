using Microsoft.AspNetCore.Http;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Infrastructure.Common.MultiTenancy.Services
{
    public class TenantAccessor : ITenantAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetTenantId()
        {
            var tenantId = _httpContextAccessor.HttpContext?.GetTenantId<string>();
            if (tenantId != null)
            {
                return tenantId;
            }

            return Guid.NewGuid().ToString();
        }
    }
}