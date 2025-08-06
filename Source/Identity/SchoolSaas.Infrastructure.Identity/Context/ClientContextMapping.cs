namespace SchoolSaas.Infrastructure.Identity.Context
{
    public static class ClientContextMapping
    {
        public static readonly Dictionary<string, string> ClientContextMap = new Dictionary<string, string>
    {
        // Mapping for CitizenIdentityContext
        { "m2m.SchoolSaas.Frontoffice-prod", "FrontOfficeIdentityContext" },
        { "interactive.SchoolSaas.Web.FrontofficeClient-local", "FrontOfficeIdentityContext" },
        { "interactive.SchoolSaas.Web.FrontofficeClient", "FrontOfficeIdentityContextFrontOffice" },
        { "interactive.SchoolSaas.Web.FrontofficeClient-prod", "FrontOfficeIdentityContext" },
        { "interactive.SchoolSaas.Web.FrontofficeClient-staging", "FrontOfficeIdentityContext" },
        { "interactive.SchoolSaas.Web.FrontofficeClient-preprod", "FrontOfficeIdentityContext" },
        { "m2m.SchoolSaas.Frontoffice-preprod", "FrontOfficeIdentityContext" },
        { "m2m.SchoolSaas.Frontoffice-staging", "FrontOfficeIdentityContext" },
        { "m2m.SchoolSaas.Frontoffice", "FrontOfficeIdentityContext" },

        // Mapping for IdentityContext
        { "m2m.SchoolSaas.Backoffice", "IdentityContext" },
        { "m2m.SchoolSaas.Backoffice-preprod", "IdentityContext" },
        { "m2m.SchoolSaas.Backoffice-prod", "IdentityContext" },
        { "m2m.SchoolSaas.Backoffice-staging", "IdentityContext" },
        { "interactive.SchoolSaas.Web.AccountClient-preprod", "IdentityContext" },
        { "interactive.SchoolSaas.Web.AccountClient-local", "IdentityContext" },
        { "interactive.SchoolSaas.Web.AccountClient-staging", "IdentityContext" },
        { "interactive.SchoolSaas.Web.AccountClient-prod", "IdentityContext" },
        { "interactive.SchoolSaas.Web.AccountClient", "IdentityContext" },
    };
    }
}
