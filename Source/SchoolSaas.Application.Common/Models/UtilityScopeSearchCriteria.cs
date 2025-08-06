namespace SchoolSaas.Application.Common.Models
{
    public class UtilityScopeSearchCriteria
    {
        public string? Title { get; set; }
        public string? RoleId { get; set; }
        public string? UserId { get; set; }
        public Guid? PermissionId { get; set; }
    }
}
