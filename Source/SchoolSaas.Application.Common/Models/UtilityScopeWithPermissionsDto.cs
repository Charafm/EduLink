namespace SchoolSaas.Application.Common.Models
{
    public class PermissionStatusDto
    {
       public PermissionDto permission {  get; set; }
        public bool IsActive { get; set; }
    }

    public class UtilityScopeWithPermissionsDto
    {
        public Guid UtilityScopeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<PermissionStatusDto> Permissions { get; set; }
    }
}
