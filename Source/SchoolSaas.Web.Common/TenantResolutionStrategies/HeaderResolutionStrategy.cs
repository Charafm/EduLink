using SchoolSaas.Domain.Common.Constants;
using Autofac.Multitenant;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SchoolSaas.Web.Common.TenantResolutionStrategies
{
    public class HeaderResolutionStrategy : ITenantIdentificationStrategy
    {
        private readonly ILogger<HeaderResolutionStrategy> _logger;

        public IHttpContextAccessor Accessor { get; private set; }

        public HeaderResolutionStrategy(IHttpContextAccessor accessor, ILogger<HeaderResolutionStrategy> logger)
        {
            Accessor = accessor;
            _logger = logger;
        }

        public bool TryIdentifyTenant(out object tenantId)
        {
            var context = Accessor.HttpContext;
            if (context == null)
            {
                // No current HttpContext. This happens during app startup
                // and isn't really an error, but is something to be aware of.
                tenantId = null;
                return false;
            }

            // Caching the value both speeds up tenant identification for
            // later and ensures we only see one log message indicating
            // relative success or failure for tenant ID.
            if (context.Items.TryGetValue(WebConstants.HttpContextTenantKey, out tenantId))
                // We've already identified the tenant at some point
                // so just return the cached value (even if the cached value
                // indicates we couldn't identify the tenant for this context).
                return tenantId != null;


            if (context.Request.Headers.TryGetValue(WebConstants.HttpHeaderTenantKey, out var tenantValues))
            {
                tenantId = tenantValues[0];
                context.Items[WebConstants.HttpContextTenantKey] = tenantId;
                _logger.LogInformation("Identified tenant: {tenant}", tenantId);
                return true;
            }

            _logger.LogWarning("Unable to identify tenant from query string. Falling back to default.");
            tenantId = null;
            context.Items[WebConstants.HttpContextTenantKey] = null;
            return false;
        }
    }
}