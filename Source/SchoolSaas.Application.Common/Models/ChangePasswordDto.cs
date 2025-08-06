namespace SchoolSaas.Application.Common.Models
{
    public class ChangePasswordDto
    {
        public string userId { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
