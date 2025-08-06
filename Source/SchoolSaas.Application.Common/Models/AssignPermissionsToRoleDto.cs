namespace SchoolSaas.Application.Common.Models
{
    public class AssignPermissionsToRoleDto
    {
        public string RoleId { get; set; }
        public List<Guid> Permissions { get; set; }
    }
}
