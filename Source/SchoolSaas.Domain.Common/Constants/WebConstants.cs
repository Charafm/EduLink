namespace SchoolSaas.Domain.Common.Constants
{
    public static class WebConstants
    {
        public const string HttpContextTenantKey = "TENANT_KEY";

        public const string HttpHeaderTenantKey = "X-Tenant-Id";

        public const string HttpContextLanguageKey = "LANGUAGE_KEY";

        public const string HttpHeaderLanguageKey = "Accept-language";

        public const string HttpHeaderAuthorizationKey = "Authorization";

        public const string HttpHeaderAuthorizationType = "Bearer";

        public const string HttpQueryAccessTokenKey = "access_token";

        public const string ApiVersionKey = "X-Api-Version";

        public const int ApiVersionMaj = 1;

        public const int ApiVersionMin = 0;

        public const string ApiVersion1_0 = "1.0";
    }
}
