using SchoolSaas.Domain.Common.DataObjects.Grade;
using SchoolSaas.Domain.Common.DataObjects.Teacher;

namespace SchoolSaas.Domain.Common.DataObjects.Course
{
    public class CourseDetailDTO
    {
        public string TitleFr { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public List<TeacherAssignmentDetailDTO> TeacherAssignments { get; set; }
        public List<CourseGradeMappingDTO> GradeMappings { get; set; }
        public List<CourseMaterialDTO>? Materials { get; set; }
    }
}
