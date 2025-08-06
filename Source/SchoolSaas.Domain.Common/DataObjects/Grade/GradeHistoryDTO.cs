namespace SchoolSaas.Domain.Common.DataObjects.Grade
{
    public class GradeHistoryDTO
    {
        public Guid GradeId { get; set; }
        public double OldScore { get; set; }
        public double NewScore { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
