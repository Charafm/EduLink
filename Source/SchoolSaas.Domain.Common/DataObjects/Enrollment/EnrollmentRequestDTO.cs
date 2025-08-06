using SchoolSaas.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.DataObjects.Enrollment
{
    public class EnrollmentRequestDTO
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string RequestCode { get; set; }
        public Guid SchoolId { get; set; }
        public Guid BranchId { get; set; }
        public string SchoolName { get; set; }
        public string RequestedByUserId { get; set; }
        public string RequestedByName { get; set; }
        public EnrollmentRequestStatusEnum Status { get; set; }  
        public DateTime CreatedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
    }
}
