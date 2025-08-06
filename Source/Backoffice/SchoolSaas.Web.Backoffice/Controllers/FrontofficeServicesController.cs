using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Backoffice.Attendance.Queries.GetAttendanceHistory;
using SchoolSaas.Application.Backoffice.Attendance.Queries.GetPaginatedAttendance;
using SchoolSaas.Application.Backoffice.Frontoffice.AcademicsVIewer;
using SchoolSaas.Application.Backoffice.Frontoffice.AttendanceViewer;
using SchoolSaas.Application.Backoffice.Frontoffice.CourseViewer;
using SchoolSaas.Application.Backoffice.Frontoffice.EnrollmentPortalService;
using SchoolSaas.Application.Backoffice.Frontoffice.GradebookViewer;
using SchoolSaas.Application.Backoffice.Frontoffice.ResourceViewer;
using SchoolSaas.Application.Backoffice.Frontoffice.ScheduleViewer;
using SchoolSaas.Application.Backoffice.Frontoffice.TransferPortalService;
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
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class FrontofficeServicesController : ApiController
    {
        [HttpGet("GetCurrentYear")]
        public async Task<ActionResult<AcademicYearDTO>> GetCurrentYear()
        {
            return await Mediator.Send(new GetAcademicCurrentYearQuery { });
        }
        [HttpGet("GetAttendanceRecords/{StudentId}")]
        public async Task<ActionResult<List<AttendanceDTO>>> GetAttendanceRecords(Guid StudentId)
        {
            return await Mediator.Send(new GetAttendanceRecordsQuery { Id = StudentId });
        }
        [HttpGet("GetAttendanceSummary")]
        public async Task<ActionResult<AttendanceSummaryDTO>> GetAttendanceSummary(Guid StudentId, DateRangeDTO Data)
        {
            return await Mediator.Send(new GetAttendanceSummaryQuery { Id = StudentId, Data = Data });
        }
        [HttpPost("SubmitAttendanceExcuse")]
        public async Task<ActionResult<bool>> SubmitAttendanceExcuse(Guid StudentId, AttendanceExcuseDTO Data)
        {
            return await Mediator.Send(new SubmitAttendanceExcuseCommand { Id = StudentId, Data = Data });
        }

        [HttpGet("GetCourseDetails/{CourseId}")]
        public async Task<ActionResult<CourseDetailDTO>> GetCourseDetails(Guid CourseId)
        {
            return await Mediator.Send(new GetCourseDetailsQuery { Id = CourseId});
        }

        [HttpGet("GetCoursesByGradeLevel/{GradeLevelId}")]
        public async Task<ActionResult<List<CourseDTO>>> GetCoursesByGradeLevel(Guid GradeLevelId)
        {
            return await Mediator.Send(new GetCoursesByGradeLevelQuery { Id = GradeLevelId });
        }

        [HttpGet("GetEnrollment/{Id}")]
        public async Task<ActionResult<EnrollmentDetailDTO>> GetEnrollment(Guid Id)
        {
            return await Mediator.Send(new GetEnrollmentQuery { Id = Id });
        }
        [HttpGet("GetEnrollmentTranscript/{Id}")]
        public async Task<ActionResult<EnrollmentTranscriptDTO>> GetEnrollmentTranscript(Guid Id)
        {
            return await Mediator.Send(new GetEnrollmentTranscriptQuery { Id = Id });
        }
        [HttpPost("SubmitEnrollment")]
        public async Task<ActionResult<bool>> SubmitEnrollment(EnrollmentDTO Data)
        {
            return await Mediator.Send(new SubmitEnrollmentCommand { Data = Data });
        }
        [HttpPost("UploadEnrollmentDocument")]
        public async Task<ActionResult<bool>> UploadEnrollmentDocument(EnrollmentDocumentUploadDTO Data)
        {
            return await Mediator.Send(new UploadEnrollmentDocumentCommand { Data = Data });
        }
        [HttpGet("GetGradeHistory")]
        public async Task<ActionResult<PagedResult<GradeHistoryDTO>>> GetGradeHistory(GradeHistoryFilterDTO Data)
        {
            return await Mediator.Send(new GetGradeHistoryQuery { Data = Data });
        }
        [HttpGet("GetGrade")]
        public async Task<ActionResult<PagedResult<GradeDTO>>> GetGrade(GradeFilterDTO Data)
        {
            return await Mediator.Send(new GetGradeQuery { Data = Data });
        }
        [HttpGet("GetGradeStatistics")]
        public async Task<ActionResult<GradeStatisticsDTO>> GetGradeStatistics(GradeFilterDTO Data)
        {
            return await Mediator.Send(new GetGradeStatisticsQuery { Data = Data });
        }
        [HttpGet("GetStudentGPA/{StudentId}")]
        public async Task<ActionResult<GpaDTO>> GetStudentGPA(Guid StudentId)
        {
            return await Mediator.Send(new GetStudentGPAQuery { Id = StudentId });
        }
        [HttpGet("GetGraderResources")]
        public async Task<ActionResult<PagedResult<GradeResourceDTO>>> GetGraderResources(ResourceFilterDTO Data)
        {
            return await Mediator.Send(new GetGraderResourcesQuery { Data = Data });
        }
        [HttpGet("GetScheduleByGradeSection/{GradeSectionId}")]
        public async Task<ActionResult<List<ScheduleDTO>>> GetScheduleByGradeSection(Guid GradeSectionId)
        {
            return await Mediator.Send(new GetScheduleByGradeSectionQuery { Id = GradeSectionId });
        }
        [HttpPost("CancelTransferRequest/{RequestId}")]
        public async Task<ActionResult<bool>> CancelTransferRequest(Guid RequestId)
        {
            return await Mediator.Send(new CancelTransferRequestCommand { Id = RequestId });
        }
        [HttpGet("CheckTransferEligibility/{StudentId}")]
        public async Task<ActionResult<TransferEligibilityResultDTO>> CheckTransferEligibility(Guid StudentId)
        {
            return await Mediator.Send(new CheckTransferEligibilityQuery { Id = StudentId });
        }
        [HttpGet("GetTransferDetails/{RequestId}")]
        public async Task<ActionResult<TransferRequestDetailDTO>> GetTransferDetails(Guid RequestId)
        {
            return await Mediator.Send(new GetTransferDetailsQuery { Id = RequestId });
        }
        [HttpGet("GetTransferHistory")]
        public async Task<ActionResult<PagedResult<TransferRequestHistoryDTO>>> GetTransferHistory(TransferHistoryFilterDTO Data)
        {
            return await Mediator.Send(new GetTransferHistoryQuery { DATA = Data });
        }
        [HttpPost("SubmitTransferRequest")]
        public async Task<ActionResult<bool>> SubmitTransferRequest(TransferRequestDTO Request)
        {
            return await Mediator.Send(new SubmitTransferRequestCommand { DATA = Request });
        }
        [HttpGet("GetPaginatedAttendanceHistory")]
        public async Task<ActionResult<PagedResult<AttendanceHistoryDTO>>> GetPaginatedAttendanceHistory(AttendanceHistoryFilterDTO Data)
        {
            return await Mediator.Send(new GetAttendanceHistoryQuery { DTO = Data });
        }

    }
}
