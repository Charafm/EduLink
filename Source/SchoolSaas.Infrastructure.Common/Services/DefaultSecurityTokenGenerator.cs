using SchoolSaas.Application.Common.Interfaces;
using OtpNet;
using System.Text;

namespace SchoolSaas.Infrastructure.Common.Services
{
    public class DefaultSecurityTokenGenerator : ISecurityTokenGenerator
    {
        public string GetCode(string secret)
        {
            var code = GetTotpInstance(secret).ComputeTotp();
            return code;
        }

        public bool ValidateCode(string secret, string code)
        {
            long timeWindowUsed;
            var valid = GetTotpInstance(secret).VerifyTotp(code, out timeWindowUsed, new VerificationWindow(2, 2));
            return valid;
        }

        private static Totp GetTotpInstance(string secret)
        {
            return new Totp(Encoding.ASCII.GetBytes(secret), mode: OtpHashMode.Sha512, totpSize: 4);
        }
    }
}
