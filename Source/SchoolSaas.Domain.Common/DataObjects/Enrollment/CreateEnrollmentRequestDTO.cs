using SchoolSaas.Domain.Common.DataObjects.Parent;
using SchoolSaas.Domain.Common.DataObjects.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.DataObjects.Enrollment
{
    public class CreateEnrollmentRequestDTO
    {
        public Guid StudentId { get; set; }
        public Guid BranchId  { get; set; }
        public bool IsDraft { get; set; }
        public List<EnrollmentDocumentUploadDTO> Documents { get; set; } = new();
    }

}
