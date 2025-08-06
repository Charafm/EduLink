using SchoolSaas.Domain.Common.Constants;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SchoolSaas.Infrastructure.Identity
{
    public static class Config
    {
        public const string ClientSecret = "DDC1ACA0-A05C-4607-AE32-1773A8B8BCF4";

        public static Dictionary<string, string> APIClients =>
            new Dictionary<string, string>()
            {
                { "m2m.SchoolSaas.Backoffice",  "http://localhost:5232"},
                //{ "m2m.SchoolSaas.Backoffice-preprod",  "http://eConsulatMA-algo.westeurope.cloudapp.azure.com:4481"},
                //{ "m2m.SchoolSaas.Backoffice-prod",  "http://eConsulatMA-algo.westeurope.cloudapp.azure.com:4481"},
                //{ "m2m.SchoolSaas.Backoffice-staging",  "http://eConsulatMA-algo.westeurope.cloudapp.azure.com:4481"},

                { "m2m.SchoolSaas.Frontoffice",  "http://localhost:5232"},
                //{ "m2m.SchoolSaas.Frontoffice-preprod",  "http://eConsulatMA-algo.westeurope.cloudapp.azure.com:4483"},
                //{ "m2m.SchoolSaas.Frontoffice-prod",  "http://eConsulatMA-algo.westeurope.cloudapp.azure.com:4483"},
                //{ "m2m.SchoolSaas.Frontoffice-staging",  "http://eConsulatMA-algo.westeurope.cloudapp.azure.com:4483"}
            };
        public static Dictionary<string, string> WebClients =>
            new Dictionary<string, string>()
            {
                { "interactive.SchoolSaas.Web.FrontofficeClient-local",  "http://localhost:3000"},
                //{ "interactive.SchoolSaas.Web.FrontofficeClient", "http://eConsulatMA-algo.westeurope.cloudapp.azure.com:4493"},
                //{ "interactive.SchoolSaas.Web.FrontofficeClient-preprod", "http://eConsulatMA-algo.westeurope.cloudapp.azure.com:4493"},
                //{ "interactive.SchoolSaas.Web.FrontofficeClient-prod", "http://eConsulatMA-algo.westeurope.cloudapp.azure.com:4493"},
                //{ "interactive.SchoolSaas.Web.FrontofficeClient-staging", "http://eConsulatMA-algo.westeurope.cloudapp.azure.com:4493"},

                { "interactive.schoolsaas.web.accountclient-local",  "http://localhost:3000"},
                //{ "interactive.SchoolSaas.Web.AccountClient",  "http://eConsulatMA-algo.westeurope.cloudapp.azure.com:4494"},
                //{ "interactive.SchoolSaas.Web.AccountClient-preprod",  "http://eConsulatMA-algo.westeurope.cloudapp.azure.com:4494"},
                //{ "interactive.SchoolSaas.Web.AccountClient-prod",  "http://eConsulatMA-algo.westeurope.cloudapp.azure.com:4494"},
                //{ "interactive.SchoolSaas.Web.AccountClient-staging",  "http://eConsulatMA-algo.westeurope.cloudapp.azure.com:4494"}
            };

        public static List<string> AllClients => APIClients.Values.Union(WebClients.Values).ToList();

        public static IEnumerable<OpenIddictApplicationDescriptor> GetClientDescriptors()
        {
            var clients = new List<OpenIddictApplicationDescriptor>();

            clients.AddRange(APIClients.Select(e => new OpenIddictApplicationDescriptor
            {
                ClientId = e.Key,
                DisplayName = e.Key,
                Permissions =
                {
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.ClientCredentials,
                    Permissions.GrantTypes.Password,
                    Permissions.GrantTypes.RefreshToken,
                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Roles,
                    Permissions.Prefixes.Scope + AuthorizationConstants.Scopes.EduLink,
                    Permissions.Prefixes.Scope + Scopes.OfflineAccess // Add offline_access permission
                },
                ClientSecret = "DDC1ACA0-A05C-4607-AE32-1773A8B8BCF4",
            }).ToList());

            clients.AddRange(WebClients.Select(e => new OpenIddictApplicationDescriptor
            {
                ClientId = e.Key,
                DisplayName = e.Key,
                ConsentType = ConsentTypes.Implicit,
                Permissions =
                {
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Logout,
                    Permissions.Endpoints.Token,
                    Permissions.Endpoints.Revocation,
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.GrantTypes.RefreshToken,
                    Permissions.ResponseTypes.Code,
                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Roles,
                    Permissions.Prefixes.Scope + AuthorizationConstants.Scopes.EduLink,
                    Permissions.Prefixes.Scope +Scopes.OfflineAccess 
                  
                },
                Requirements =
                {
                    Requirements.Features.ProofKeyForCodeExchange
                },
                RedirectUris = { new Uri($"{e.Value}/authentication/login-callback") },
                PostLogoutRedirectUris = { new Uri($"{e.Value}/authentication/logout-callback") },
            }).ToList());

            return clients;
        }
    }
}
