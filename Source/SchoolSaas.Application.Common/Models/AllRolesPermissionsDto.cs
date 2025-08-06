namespace SchoolSaas.Application.Common.Models
{
    public class AllRolesPermissionsDto
    {
        public List<RoleWithPermissions> RolesWithPermissions { get; set; }
    }
    public class RoleWithPermissions
    {
        public RoleDto Role { get; set; }
        public List<PermissionDto>Permissions { get; set; }

    }
}
