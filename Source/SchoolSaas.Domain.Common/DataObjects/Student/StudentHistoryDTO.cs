namespace SchoolSaas.Domain.Common.DataObjects.Student
{
    public class StudentHistoryDTO
    {
        public Guid StudentId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
        public string FieldChanges { get; set; }
    }
}
