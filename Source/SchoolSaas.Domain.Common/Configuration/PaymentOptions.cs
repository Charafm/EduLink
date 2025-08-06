namespace SchoolSaas.Domain.Common.Configuration
{
    public class PaymentOptions
    {
        public string Endpoint { get; set; } = "http://secure.lyra.com/vads-payment/";
        public string APIEndpoint { get; set; } = "http://secure.lyra.com/vads-payment/";
        public string APIUsername { get; set; } = string.Empty;
        public string APIPassword { get; set; } = string.Empty;
        public string APIHashKey { get; set; } = string.Empty;
        public string StoreKey { get; set; } = string.Empty;
        public string StoreId { get; set; } = string.Empty;
        public string Mode { get; set; } = "TEST";

        public string MandatSignatureURL { get; set; } = string.Empty;
    }
}
