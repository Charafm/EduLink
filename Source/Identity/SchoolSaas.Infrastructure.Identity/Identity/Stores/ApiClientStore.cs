using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SchoolSaas.Infrastructure.Identity.Identity.Stores
{
    public class ApiClientStore
    {
        private const int TokenLifetime = 60 * 60 * 24 * 7;

        public static IEnumerable<OpenIddictApplicationDescriptor> GetAllClients() => new[]
        {
            new OpenIddictApplicationDescriptor
            {
                ClientSecret = "388D45FA-B36B-4988-BA59-B187D329C207",
                DisplayName = "EduLink API Client",
                Permissions =
                {
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.ClientCredentials
                }
            }
        };
    }
}
