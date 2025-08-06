namespace SchoolSaas.Domain.Common.DataObjects.Student
{
    public class StudentTranscriptDTO
    {
        public Guid StudentId { get; set; }
        public string FullName { get; set; }
        public List<TranscriptRecordDTO> TranscriptRecords { get; set; }
    }
}
