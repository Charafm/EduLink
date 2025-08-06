namespace SchoolSaas.Domain.Common.DataObjects.Course
{
    public class CourseMaterialDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime LastModified { get; set; }
    }
}
