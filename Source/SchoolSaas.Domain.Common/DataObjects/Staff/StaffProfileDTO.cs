using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Staff
{
    public class StaffProfileDTO
    {
        public Guid StaffId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public StaffRole Role { get; set; }

        // Detailed audit logs or role history can be returned as a list.
        public List<StaffAuditDTO> AuditLogs { get; set; }
    }
}
