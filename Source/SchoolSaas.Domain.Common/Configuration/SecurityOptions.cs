namespace SchoolSaas.Domain.Common.Configuration
{
    public class SecurityOptions
    {
        public string AuthorityUrl { get; set; }
        public string Audience { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}