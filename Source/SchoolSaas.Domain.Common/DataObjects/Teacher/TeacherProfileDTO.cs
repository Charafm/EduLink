using SchoolSaas.Domain.Common.DataObjects.Course;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Teacher
{
    public  class TeacherProfileDTO
    {
        public Guid TeacherId { get; set; }
        public string FullNameFr { get; set; }
        public string FullNameAr { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public string SpecializationFr { get; set; }
        public string SpecializationAr { get; set; }
        public TeacherStatusEnum Status { get; set; }
        public List<CourseScheduleDTO> Courses { get; set; }
    }
}
