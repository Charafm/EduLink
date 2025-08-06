using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Staff
{
    public class StaffRoleDTO
    {
        public Guid StaffId { get; set; }
        public StaffRole Role { get; set; }
        public string RequestorId { get; set; }
    }
}
