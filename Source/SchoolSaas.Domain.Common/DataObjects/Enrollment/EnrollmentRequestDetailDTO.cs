using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Parent;
using SchoolSaas.Domain.Common.DataObjects.Student;
using SchoolSaas.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.DataObjects.Enrollment
{
    public class EnrollmentRequestDetailDTO
    {
        public Guid Id { get; set; }
        public string RequestCode { get; set; }
        public Guid SchoolId { get; set; }
        public string SchoolName { get; set; }

        public StudentDTO Student { get; set; }
        public ParentDTO Parent { get; set; }

        public List<DocumentDTO> Documents { get; set; } = new();

        public EnrollmentRequestStatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
    }

}
