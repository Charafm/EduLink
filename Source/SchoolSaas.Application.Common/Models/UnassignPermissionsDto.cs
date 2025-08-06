namespace SchoolSaas.Application.Common.Models
{
    public class UnassignPermissionsDto
    {
        public string UserId { get; set; }
        public List<Guid> PermissionIds { get; set; }
    }
}
