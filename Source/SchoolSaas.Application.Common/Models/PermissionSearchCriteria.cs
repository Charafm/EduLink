namespace SchoolSaas.Application.Common.Models
{
    public class PermissionSearchCriteria
    {
        public string? Name { get; set; }
        public Guid? UtilityScopeId { get; set; }
        public string? UserId { get; set; }
        public string? RoleId { get; set; }
    }
}
