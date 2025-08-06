using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Grade
{
    public class GradeUpdateDTO
    {
        public double Score { get; set; }
        public GradeTypeEnum GradeType { get; set; }
        public string? TeacherCommentFr { get; set; }
        public string? TeacherCommentAr { get; set; }
        public string? TeacherCommentEn { get; set; }
    }
}
