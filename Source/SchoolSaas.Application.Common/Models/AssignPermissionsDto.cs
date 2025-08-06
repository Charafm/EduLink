namespace SchoolSaas.Application.Common.Models
{
    public class AssignPermissionsDto
    {
        public string UserId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }
}
