using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolSaas.Infrastructure.Identity.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;

namespace SchoolSaas.Infrastructure.Identity.Identity.Stores
{
    public class CustomTokenStore : OpenIddictEntityFrameworkCoreTokenStore<
        OpenIddictEntityFrameworkCoreToken,
        OpenIddictEntityFrameworkCoreApplication,
        OpenIddictEntityFrameworkCoreAuthorization,
        DbContext,
        string>
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomTokenStore(
            DbContext context,
            IMemoryCache cache,
            IOptionsMonitor<OpenIddictEntityFrameworkCoreOptions> options,
            IServiceProvider serviceProvider)
            : base(cache, context, options)
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

        public override async ValueTask CreateAsync(OpenIddictEntityFrameworkCoreToken token, CancellationToken cancellationToken)
        {
            var request = _serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.GetOpenIddictServerRequest();
            var clientId = request.ClientId;
            var context = GetDbContext(clientId);
            var application = await context.Set<OpenIddictEntityFrameworkCoreApplication>()
                                           .FirstOrDefaultAsync(app => app.ClientId == clientId, cancellationToken);

            if (application == null)
            {
                throw new InvalidOperationException($"The application with client_id '{clientId}' cannot be found.");
            }

            token.Application = application;

            // Check for existing valid authorization with the same criteria
            var existingAuthorization = await context.Set<OpenIddictEntityFrameworkCoreAuthorization>()
                .FirstOrDefaultAsync(auth => auth.Application == token.Application &&
                                             auth.Subject == token.Subject &&
                                             auth.Type == OpenIddictConstants.AuthorizationTypes.AdHoc &&
                                             auth.Scopes == request.Scope &&
                                             auth.Status == OpenIddictConstants.Statuses.Valid, // Ensure the authorization is valid
                                             cancellationToken);

            if (existingAuthorization != null)
            {
                token.Authorization = existingAuthorization;
            }
            else
            {
                var newAuthorization = new OpenIddictEntityFrameworkCoreAuthorization
                {
                    Id = Guid.NewGuid().ToString(),
                    Application = token.Application,
                    Status = OpenIddictConstants.Statuses.Valid,
                    Subject = token.Subject,
                    CreationDate = token.CreationDate,
                    Type = OpenIddictConstants.AuthorizationTypes.AdHoc,
                    Scopes = request.Scope
                };
                token.Authorization = newAuthorization;
                context.Add(newAuthorization);
            }

            context.Add(token);
            await context.SaveChangesAsync(cancellationToken);
        }


        public override async ValueTask UpdateAsync(OpenIddictEntityFrameworkCoreToken token, CancellationToken cancellationToken)
        {
            var clientId = token.Application.ClientId;
            var context = GetDbContext(clientId);
            context.Update(token);
            await context.SaveChangesAsync(cancellationToken);
        }

        public override async ValueTask DeleteAsync(OpenIddictEntityFrameworkCoreToken token, CancellationToken cancellationToken)
        {
            var clientId = token.Application.ClientId;
            var context = GetDbContext(clientId);
            context.Remove(token);
            await context.SaveChangesAsync(cancellationToken);
        }

        public override async ValueTask<OpenIddictEntityFrameworkCoreToken?> FindByIdAsync(string identifier, CancellationToken cancellationToken)
        {
            var frontOfficeContext = _serviceProvider.GetRequiredService<FrontOfficeIdentityContext>();
            var identityContext = _serviceProvider.GetRequiredService<IdentityContext>();

            var token = await frontOfficeContext.Set<OpenIddictEntityFrameworkCoreToken>().FindAsync(new object[] { identifier }, cancellationToken)
                        ?? await identityContext.Set<OpenIddictEntityFrameworkCoreToken>().FindAsync(new object[] { identifier }, cancellationToken);

            return token;
        }
      

    }
}
