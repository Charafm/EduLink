using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Traceability
{
    public class GradeHistory : BaseEntity<Guid>
    {
        public Guid GradeId { get; set; }

        public double OldScore { get; set; }
        public double NewScore { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string OldComments { get; set; } // JSON serialized
        public string NewComments { get; set; } // JSON serialized
        public Grade Grade { get; set; }
    }

}
