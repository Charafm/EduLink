using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;

namespace SchoolSaas.Infrastructure.Identity.Identity
{
    public class CustomApplicationManager : OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication>
    {
        public CustomApplicationManager(
            IOpenIddictApplicationCache<OpenIddictEntityFrameworkCoreApplication> cache,
            ILogger<OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication>> logger,
            IOptionsMonitor<OpenIddictCoreOptions> options,
            IOpenIddictApplicationStoreResolver resolver)
            : base(cache, logger, options, resolver)
        {
        }

        public async Task<string> ObfuscateClientSecretAsync(string clientSecret, CancellationToken cancellationToken = default)
        {
            return await base.ObfuscateClientSecretAsync(clientSecret, cancellationToken);
        }
    }
}
