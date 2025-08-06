namespace SchoolSaas.Domain.Common.DataObjects.Course
{
    public class CourseFilterDTO
    {
        public string SearchTerm { get; set; }
        public Guid? GradeLevelId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
