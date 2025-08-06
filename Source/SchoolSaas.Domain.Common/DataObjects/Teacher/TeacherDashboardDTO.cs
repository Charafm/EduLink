using SchoolSaas.Domain.Common.DataObjects.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.DataObjects.Teacher
{
    public class TeacherDashboardDTO
    {
        public Guid TeacherId { get; set; }
        public string FullNameFr { get; set; }
        public string FullNameAr { get; set; }
        public int CourseCount { get; set; }
        public List<CourseDTO> AssignedCourses { get; set; }
    }
}
