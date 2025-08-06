namespace SchoolSaas.Application.Common.Models
{
    public class PaymentRequestDto
    {
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public string VerificationCode { get; set; }
        public DateTime CardExpiryDate { get; set; }
    }
}
