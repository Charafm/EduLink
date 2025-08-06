using SchoolSaas.Domain.Common.DataObjects.Schedule;

namespace SchoolSaas.Domain.Common.DataObjects.Course
{
    public class CourseScheduleDTO
    {
        public Guid CourseId { get; set; }
        public string CourseNameFr { get; set; }
        public string CourseNameAr { get; set; }
        public string? Description { get; set; }
        public Guid GradeId { get; set; }
        public string GradeNameFr { get; set; }
        public string GradeNameAr { get; set; }
        public List<ScheduleItemDTO> Schedule { get; set; } = new List<ScheduleItemDTO>();
    }
}
