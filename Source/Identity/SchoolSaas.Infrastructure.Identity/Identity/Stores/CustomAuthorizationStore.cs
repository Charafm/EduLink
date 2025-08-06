using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OpenIddict.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolSaas.Infrastructure.Identity.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;

namespace SchoolSaas.Infrastructure.Identity.Identity.Stores
{
    public class CustomAuthorizationStore : OpenIddictEntityFrameworkCoreAuthorizationStore<
     OpenIddictEntityFrameworkCoreAuthorization,
     OpenIddictEntityFrameworkCoreApplication,
     OpenIddictEntityFrameworkCoreToken,
     DbContext,
     string>
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomAuthorizationStore(
            IMemoryCache cache,
            IOptionsMonitor<OpenIddictEntityFrameworkCoreOptions> options,
            IServiceProvider serviceProvider)
            : base(cache, serviceProvider.GetRequiredService<IdentityContext>(), options)
        {
            _serviceProvider = serviceProvider;
        }

        private DbContext GetDbContext(string clientId)
        {
            var contextType = ClientContextMapping.ClientContextMap[clientId];
            return contextType == "FrontOfficeIdentityContext"
                ? _serviceProvider.GetRequiredService<FrontOfficeIdentityContext>()
                : _serviceProvider.GetRequiredService<IdentityContext>();
        }
        public override async ValueTask<OpenIddictEntityFrameworkCoreAuthorization?> FindByIdAsync(string identifier, CancellationToken cancellationToken)
        {
            var clientId = _serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.GetOpenIddictServerRequest().ClientId;
            var context = GetDbContext(clientId);
            return await context.Set<OpenIddictEntityFrameworkCoreAuthorization>()
                .FirstOrDefaultAsync(auth => auth.Id == identifier, cancellationToken);
        }
        // Override necessary methods to use the correct DbContext
    }

    



    }

