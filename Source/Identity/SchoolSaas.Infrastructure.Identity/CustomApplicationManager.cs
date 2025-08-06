using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using System.Collections.Immutable;

namespace SchoolSaas.Infrastructure.Identity
{
    public class CustomApplicationManager : OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication>
    {
        public CustomApplicationManager(IOpenIddictApplicationCache<OpenIddictEntityFrameworkCoreApplication> cache, ILogger<OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication>> logger, IOptionsMonitor<OpenIddictCoreOptions> options, IOpenIddictApplicationStoreResolver resolver) : base(cache, logger, options, resolver)
        {
        }

        public override ValueTask<bool> ValidateRedirectUriAsync(OpenIddictEntityFrameworkCoreApplication application, string address, CancellationToken cancellationToken = default)
        {
            return base.ValidateRedirectUriAsync(application, address, cancellationToken);
        }

        public override ValueTask<ImmutableArray<string>> GetRedirectUrisAsync(OpenIddictEntityFrameworkCoreApplication application, CancellationToken cancellationToken = default)
        {
            return base.GetRedirectUrisAsync(application, cancellationToken);
        }
    }
}
