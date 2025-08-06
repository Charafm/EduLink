using Microsoft.EntityFrameworkCore;
using SchoolSaas.Infrastructure.Identity.Context;
using Microsoft.Extensions.DependencyInjection;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Tests.Utilities
{
    public class TestContextFactory : IContextFactory
    {
        private readonly IServiceProvider _provider;
        public TestContextFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public DbContext CreateContext(string clientId)
        {
            // For testing, if clientId contains "Backoffice", return IdentityContext; otherwise, FrontOfficeIdentityContext.
            if (clientId.Contains("Backoffice"))
                return _provider.GetRequiredService<IdentityContext>();
            else
                return _provider.GetRequiredService<FrontOfficeIdentityContext>();
        }

        public Type ResolveContextType(string clientId)
        {
            if (ClientContextMapping.ClientContextMap.TryGetValue(clientId, out var contextName))
            {
                return contextName == "FrontOfficeIdentityContext" ? typeof(FrontOfficeIdentityContext) : typeof(IdentityContext);
            }
            throw new InvalidOperationException($"No context mapping found for client ID {clientId}");
        }
    }
}
