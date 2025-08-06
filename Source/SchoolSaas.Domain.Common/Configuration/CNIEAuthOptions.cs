namespace SchoolSaas.Domain.Common.Configuration
{
    public class CNIEAuthOptions
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSeceret { get; set; } = string.Empty;
        public string ServerUrl { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string RedirectUri { get; set; } = string.Empty;
        public string FOUrl { get; set; } = string.Empty;
        public string ScopeId { get; set; } = string.Empty;
        public string UiLocales { get; set; } = string.Empty;
        public string ProductHeaderValue { get; set; } = string.Empty;

    }
}