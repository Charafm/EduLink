using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Domain.Common.DataObjects.Enrollment
{
    public class EnrollmentDetailDTO : EnrollmentDTO
    {
        public string StudentName { get; set; }
        public string BranchName { get; set; }
        public List<DocumentDTO> Documents { get; set; } = new();
    }
}
