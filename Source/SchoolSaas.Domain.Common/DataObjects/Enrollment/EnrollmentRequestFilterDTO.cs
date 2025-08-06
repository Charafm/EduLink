using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.DataObjects.Enrollment
{
    public class EnrollmentRequestFilterDTO : PagedRequestBase
    {
        public EnrollmentRequestStatusEnum? Status { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? ParentId { get; set; }
        public string? SearchTerm { get; set; }
    }
}
