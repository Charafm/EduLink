namespace SchoolSaas.Application.Common.Models
{
    public class PaymentResponseDto 
    {
        public string CardHolder { get; set; }
        public string Reference { get; set; }
        public double Amount { get; set; }
    }
}
