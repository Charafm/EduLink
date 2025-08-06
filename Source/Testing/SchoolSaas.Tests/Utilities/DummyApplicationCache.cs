using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;

namespace SchoolSaas.Tests.Utilities
{
    public class DummyApplicationCache : IOpenIddictApplicationCache<OpenIddictEntityFrameworkCoreApplication>
    {
        public ValueTask AddAsync(OpenIddictEntityFrameworkCoreApplication application, CancellationToken cancellationToken)
        {
            return ValueTask.CompletedTask;
        }

        public IAsyncEnumerable<OpenIddictEntityFrameworkCoreApplication> FindByPostLogoutRedirectUriAsync(string postLogoutRedirectUri, CancellationToken cancellationToken)
        {
            return AsyncEnumerable.Empty<OpenIddictEntityFrameworkCoreApplication>();
        }

        public IAsyncEnumerable<OpenIddictEntityFrameworkCoreApplication> FindByRedirectUriAsync(string redirectUri, CancellationToken cancellationToken)
        {
            return AsyncEnumerable.Empty<OpenIddictEntityFrameworkCoreApplication>();
        }

        public ValueTask<OpenIddictEntityFrameworkCoreApplication?> FindByClientIdAsync(string clientId, CancellationToken cancellationToken)
        {
            return new ValueTask<OpenIddictEntityFrameworkCoreApplication?>((OpenIddictEntityFrameworkCoreApplication?)null);
        }

        public ValueTask<OpenIddictEntityFrameworkCoreApplication?> FindByIdAsync(string identifier, CancellationToken cancellationToken)
        {
            return new ValueTask<OpenIddictEntityFrameworkCoreApplication?>((OpenIddictEntityFrameworkCoreApplication?)null);
        }

        public ValueTask RemoveAsync(OpenIddictEntityFrameworkCoreApplication application, CancellationToken cancellationToken)
        {
            return ValueTask.CompletedTask;
        }

        public void Clear()
        {
            // No-op
        }

        public IEnumerable<OpenIddictEntityFrameworkCoreApplication> List()
        {
            return Enumerable.Empty<OpenIddictEntityFrameworkCoreApplication>();
        }
    }
}
