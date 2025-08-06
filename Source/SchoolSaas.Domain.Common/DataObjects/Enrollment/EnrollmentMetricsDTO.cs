namespace SchoolSaas.Domain.Common.DataObjects.Enrollment
{
    public class EnrollmentMetricsDTO
    {
        public int TotalEnrollments { get; set; }
        public int Pending { get; set; }
        public int Approved { get; set; }
        public int Rejected { get; set; }
        public int Completed { get; set; }
    }
}
