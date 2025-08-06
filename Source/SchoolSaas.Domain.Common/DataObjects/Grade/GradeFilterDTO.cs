namespace SchoolSaas.Domain.Common.DataObjects.Grade
{
    public class GradeFilterDTO
    {
        public Guid? StudentId { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? SemesterId { get; set; }
        public double? MinScore { get; set; }
        public double? MaxScore { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
