using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Enrollment
{
    public class EnrollmentTranscriptDTO
    {
        /*
         EnrollmentId = enrollment.Id,
                StudentId = enrollment.StudentId,
                StudentName = $"{enrollment.Student.FirstNameFr} {enrollment.Student.LastNameFr}",
                BranchName = enrollment.Branch.BranchNameFr,
                Status = enrollment.Status,
                SubmittedAt = enrollment.SubmittedAt,
                // Map history to DTOs – reusing your helper method.
                StatusHistory = history.Select(h => new EnrollmentStatusHistoryDTO
                {
                    EnrollmentId = h.EnrollmentId,
                    OldStatus = h.OldStatus,
                    NewStatus = h.NewStatus,
                    ChangedBy = h.ChangedBy,
                    ChangedAt = h.ChangedAt,
                    ChangeReason = h.ChangeReason
                }).ToList(),
                Documents = documents
         */
        public Guid EnrollmentId { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public string BranchName { get; set; }
        public EnrollmentStatusEnum Status { get; set; }
        public DateTime SubmittedAt { get; set; }
        public List<EnrollmentStatusHistoryDTO> StatusHistory { get; set; }
        public List<DocumentDTO> Documents { get; set; }
    }
}
