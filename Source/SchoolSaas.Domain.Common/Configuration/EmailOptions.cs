namespace SchoolSaas.Domain.Common.Configuration
{
    public class EmailOptions
    {
        public string MailServer { get; set; } = string.Empty;
        public int MailPort { get; set; }
        public bool UseSSL { get; set; } = true;
        public string SenderName { get; set; } = string.Empty;
        public string Sender { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}