using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Application.Common.Models
{
    public class LoginRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
