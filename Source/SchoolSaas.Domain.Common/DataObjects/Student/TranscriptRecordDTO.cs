namespace SchoolSaas.Domain.Common.DataObjects.Student
{
    public class TranscriptRecordDTO
    {
        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public double Grade { get; set; }  
        public string Term { get; set; }
        public string AcademicYear { get; set; }
    }
}
