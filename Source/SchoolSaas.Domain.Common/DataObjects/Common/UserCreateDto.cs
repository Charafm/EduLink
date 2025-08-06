namespace SchoolSaas.Domain.Common.DataObjects.Common
{
    public class UserCreateDto
    {
        public string? Id { get; set; }
        public string FirstName     { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Token { get; set; } = null;


    }
}
