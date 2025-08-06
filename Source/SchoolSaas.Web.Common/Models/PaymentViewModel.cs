namespace SchoolSaas.Web.Common.Models
{
    public class PaymentViewModel
    {
        public Guid Id { get; set; }
        public string Endpoint { get; set; }
        public string Signature { get; set; } = string.Empty;
        public List<KeyValuePair<string, string>> Fields { get; set; } = new List<KeyValuePair<string, string>>();
    }
}
