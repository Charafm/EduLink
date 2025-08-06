using Microsoft.AspNetCore.Http;
using SchoolSaas.Domain.Common.Constants;

namespace SchoolSaas.Infrastructure.Common.MultiTenancy
{
    public static class MultiTenantHttpContextExtensions
    {
        public static T? GetTenantId<T>(this HttpContext context) where T : class
        {
            if (context.Items.TryGetValue(WebConstants.HttpContextTenantKey, out object? tenantId))
            {
                return tenantId as T;
            }
            return default;
        }
    }
}