using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Academic;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Course;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using SchoolSaas.Domain.Common.DataObjects.Schedule;
using SchoolSaas.Domain.Common.DataObjects.SchoolSupply;
using SchoolSaas.Domain.Common.DataObjects.Transfer;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IBackofficeConnectedService
    {

        Task<ResponseDto<bool>> UploadEnrollmentDocument(EnrollmentDocumentUploadDTO dto, CancellationToken cancellationToken);
        Task<ResponseDto<bool>> SubmitTransferRequest(TransferRequestDTO dto, CancellationToken cancellationToken);
        Task<ResponseDto<TransferEligibilityResultDTO>> CheckTransferEligibility(Guid studentId, CancellationToken cancellationToken);
        Task<ResponseDto<TransferRequestDetailDTO>> GetTransferDetails(Guid requestId, CancellationToken cancellationToken);
        Task<ResponseDto<bool>> CancelTransferRequest(Guid requestId, CancellationToken cancellationToken);
        Task<ResponseDto<AcademicYearDTO>> GetCurrentYear(CancellationToken cancellationToken);
        Task<ResponseDto<ICollection<AttendanceDTO>>> GetAttendanceRecords(Guid studentId, CancellationToken cancellationToken);
        Task<ResponseDto<EnrollmentDetailDTO>> GetEnrollment(Guid id, CancellationToken cancellationToken);
        Task<ResponseDto<EnrollmentTranscriptDTO>> GetEnrollmentTranscript(Guid id, CancellationToken cancellationToken);
        Task<ResponseDto<bool>> SubmitEnrollment(EnrollmentDTO dto, CancellationToken cancellationToken);
        Task<ResponseDto<AttendanceSummaryDTO>> GetAttendanceSummary(Guid studentId, DateRangeDTO dateRange, CancellationToken cancellationToken);
        Task<ResponseDto<bool>> SubmitAttendanceExcuse(Guid studentId, AttendanceExcuseDTO excuseDto, CancellationToken cancellationToken);
        Task<ResponseDto<CourseDetailDTO>> GetCourseDetails(Guid courseId, CancellationToken cancellationToken);
        Task<ResponseDto<ICollection<CourseDTO>>> GetCoursesByGradeLevel(Guid gradeLevelId, CancellationToken cancellationToken);
        Task<ResponseDto<PagedResult<GradeHistoryDTO>>> GetGradeHistory(GradeHistoryFilterDTO filter, CancellationToken cancellationToken);
        Task<ResponseDto<PagedResult<GradeDTO>>> GetGrade(GradeFilterDTO filter, CancellationToken cancellationToken);
        Task<ResponseDto<GradeStatisticsDTO>> GetGradeStatistics(GradeFilterDTO filter, CancellationToken cancellationToken);
        Task<ResponseDto<GpaDTO>> GetStudentGPA(Guid studentId, CancellationToken cancellationToken);
        Task<ResponseDto<PagedResult<GradeResourceDTO>>> GetGraderResources(ResourceFilterDTO filter, CancellationToken cancellationToken);
        Task<ResponseDto<ICollection<ScheduleDTO>>> GetScheduleByGradeSection(Guid gradeSectionId, CancellationToken cancellationToken);
        Task<ResponseDto<PagedResult<TransferRequestHistoryDTO>>> GetTransferHistory(TransferHistoryFilterDTO filter, CancellationToken cancellationToken);
        Task<ResponseDto<PagedResult<AttendanceHistoryDTO>>> GetPaginatedAttendanceHistory(AttendanceHistoryFilterDTO filter, CancellationToken cancellationToken);

    }
}
