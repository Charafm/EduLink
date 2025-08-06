namespace SchoolSaas.Application.Common.Models
{
    public class GetRoleByCriteriaDto
    {
        public string? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserFn { get; set;}
        public string? UserLn { get; set;}
    }
    
}
