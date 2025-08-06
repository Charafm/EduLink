using SchoolSaas.Domain.Common.DataObjects.Course;
using SchoolSaas.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.DataObjects.GradeLevel
{
    public class GradeLevelDTO
    {
        public string TitleFr { get; set; }
        public string? TitleAr { get; set; }
        public string? TitleEn { get; set; }
        public string? Description { get; set; }
        public EducationalStageEnum? EducationalStage { get; set; }
        public List<CourseDTO>? CourseGradeMappings { get; set; }
    }

}
