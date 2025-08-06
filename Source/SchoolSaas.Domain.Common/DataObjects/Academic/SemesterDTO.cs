using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Academic
{
    public class SemesterDTO
    {
        public Guid Id { get; set; }   
        public AcademicYearDTO AcademicYear { get; set; }
        public SemesterNameEnum Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
