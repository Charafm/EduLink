namespace SchoolSaas.Domain.Common.DataObjects.Common
{
    public class UserCriteria
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? IsDeactivated { get; set; }
        public string? RoleName { get; set; }
    }
}