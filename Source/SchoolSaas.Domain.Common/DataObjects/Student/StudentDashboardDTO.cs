namespace SchoolSaas.Domain.Common.DataObjects.Student
{
    public class StudentDashboardDTO
    {
        public Guid StudentId { get; set; }
        public string FullNameFR { get; set; }
        public string FullNameAR { get; set; }
        public double GPA { get; set; }
        public int TotalAbsences { get; set; }
        public int TotalLate {  get; set; }
        public List<DashboardCourseDTO> Courses { get; set; }
    }
    public class DashboardCourseDTO
    {
        public Guid CourseId { get; set; }
        public string CourseNameFr { get; set; }
        public string CourseNameAr { get; set; }
        public string TeacherNameFr { get; set; }
        public string TeacherNameAr { get; set; }
        public string Schedule { get; set; }
    }
}
