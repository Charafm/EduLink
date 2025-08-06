using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Academic
{
    public class Vacation : BaseEntity<Guid>
    {
        public Guid AcademicYearId { get; set; }

        public VacationNameEnum Name { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public AcademicYear AcademicYear { get; set; }
    }

}
