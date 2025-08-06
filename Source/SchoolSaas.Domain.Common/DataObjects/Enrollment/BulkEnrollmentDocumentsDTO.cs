namespace SchoolSaas.Domain.Common.DataObjects.Enrollment
{
    public class BulkEnrollmentDocumentsDTO
    {
        public Guid EnrollmentId { get; set; }
        public List<EnrollmentDocumentUploadDTO> Documents { get; set; } = new();
    }
}
