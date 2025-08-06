namespace SchoolSaas.Domain.Common.DataObjects.Grade
{
    public class CourseGradeMappingDTO
    {
        public Guid CourseId { get; set; }
        public Guid GradeLevelId { get; set; }
        public bool? IsElective { get; set; }
    }
}
