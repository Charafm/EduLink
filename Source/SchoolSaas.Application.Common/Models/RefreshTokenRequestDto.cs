using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Application.Common.Models
{
    public class RefreshTokenRequestDto
    {
        public string RefreshToken { get; set; }
        public UserType UserType { get; set; }
    }
}
