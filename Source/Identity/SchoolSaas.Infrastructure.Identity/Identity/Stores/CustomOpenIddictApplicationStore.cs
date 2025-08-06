using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OpenIddict.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;
using SchoolSaas.Infrastructure.Identity.Context;

namespace SchoolSaas.Infrastructure.Identity.Identity.Stores
{
    public class CustomApplicationStore : OpenIddictEntityFrameworkCoreApplicationStore<
        OpenIddictEntityFrameworkCoreApplication,
        OpenIddictEntityFrameworkCoreAuthorization,
        OpenIddictEntityFrameworkCoreToken,
        DbContext,
        string>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly FrontOfficeIdentityContext _frontOfficeContext;
        private readonly IdentityContext _identityContext;

        public CustomApplicationStore(
            FrontOfficeIdentityContext frontOfficeContext,
            IdentityContext identityContext,
            IMemoryCache cache,
            IOptionsMonitor<OpenIddictEntityFrameworkCoreOptions> options,
            IServiceProvider serviceProvider)
            : base(cache, identityContext, options) // Use one context as the base
        {
            _serviceProvider = serviceProvider;
            _frontOfficeContext = frontOfficeContext;
            _identityContext = identityContext;
        }

        public override async ValueTask CreateAsync(OpenIddictEntityFrameworkCoreApplication application, CancellationToken cancellationToken)
        {
            //_frontOfficeContext.Add(application);
            _identityContext.Add(application);
            await _frontOfficeContext.SaveChangesAsync(cancellationToken);
            await _identityContext.SaveChangesAsync(cancellationToken);
        }

        public override async ValueTask UpdateAsync(OpenIddictEntityFrameworkCoreApplication application, CancellationToken cancellationToken)
        {
            //_frontOfficeContext.Update(application);
            _identityContext.Update(application);
            await _frontOfficeContext.SaveChangesAsync(cancellationToken);
            await _identityContext.SaveChangesAsync(cancellationToken);
        }

        public override async ValueTask DeleteAsync(OpenIddictEntityFrameworkCoreApplication application, CancellationToken cancellationToken)
        {
            //_frontOfficeContext.Remove(application);
            _identityContext.Remove(application);
            await _frontOfficeContext.SaveChangesAsync(cancellationToken);
            await _identityContext.SaveChangesAsync(cancellationToken);
        }

        public override async ValueTask<OpenIddictEntityFrameworkCoreApplication?> FindByIdAsync(string identifier, CancellationToken cancellationToken)
        {
            var application =  await _identityContext.Set<OpenIddictEntityFrameworkCoreApplication>().FindAsync(new object[] { identifier }, cancellationToken);

            return application;
        }
    }
}
