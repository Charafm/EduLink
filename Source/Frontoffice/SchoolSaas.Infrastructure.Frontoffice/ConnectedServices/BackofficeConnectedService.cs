using Microsoft.AspNetCore.Http;
using SchoolSaas.Application.Common.Exceptions;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Common.Constants;
using SchoolSaas.Domain.Common.Extensions;
using SchoolSaas.Infrastructure.Common.Logger;
using SchoolSaas.Infrastructure.Frontoffice.Helpers;
using SchoolSaas.Infrastructure.Frontoffice.OpenAPIs.BackofficeService;
using System.Threading;

namespace SchoolSaas.Infrastructure.Frontoffice.ConnectedServices
{
    public class BackofficeConnectedService : IBackofficeConnectedService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        //private readonly IDbLogger _logger;
        

        public BackofficeConnectedService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = _httpClientFactory.CreateClient(ApiConstants.BackofficeBaseClient);
            //_logger = loggerProvider.CreateDbLogger(nameof(BackofficeConnectedService));
      
        }

        public async Task<ResponseDto<bool>> UploadEnrollmentDocument(Domain.Common.DataObjects.Enrollment.EnrollmentDocumentUploadDTO dto, CancellationToken cancellationToken) =>
            await ExecuteApiCall(async client => await client.UploadEnrollmentDocumentAsync(DtoMapper.ToOpenApi(dto), cancellationToken), "Document Uploaded successfully");

        public async Task<ResponseDto<bool>> SubmitTransferRequest(Domain.Common.DataObjects.Transfer.TransferRequestDTO dto, CancellationToken cancellationToken) =>
            await ExecuteApiCall(async client => await client.SubmitTransferRequestAsync(DtoMapper.ToOpenApi(dto)   , cancellationToken), "Transfer submitted successfully");

        public async Task<ResponseDto<Domain.Common.DataObjects.Transfer.TransferEligibilityResultDTO>> CheckTransferEligibility(Guid studentId, CancellationToken cancellationToken)
        {
            var result = ExecuteApiCall(async client => await client.CheckTransferEligibilityAsync(studentId, cancellationToken)).Result.Data.ToDomain();

            return new ResponseDto<Domain.Common.DataObjects.Transfer.TransferEligibilityResultDTO>
            {
                Data = result,
                Message = "Done"
            };
        }
        public async Task<ResponseDto<PagedResult<Domain.Common.DataObjects.Attendance.AttendanceHistoryDTO>>> GetPaginatedAttendanceHistory(Domain.Common.DataObjects.Attendance.AttendanceHistoryFilterDTO filter, CancellationToken cancellationToken)
        {
            var result = await ExecuteApiCall(async client => await client.GetPaginatedAttendanceHistoryAsync(DtoMapper.ToOpenApi(filter), cancellationToken));
            return new ResponseDto<PagedResult<Domain.Common.DataObjects.Attendance.AttendanceHistoryDTO>>
            {
                Data = result.Data.ToDomain(),
                Message = "Done"
            };
        }

        public async Task<ResponseDto<PagedResult<Domain.Common.DataObjects.Grade.GradeHistoryDTO>>> GetGradeHistory(Domain.Common.DataObjects.Grade.GradeHistoryFilterDTO filter, CancellationToken cancellationToken)
        {
            var result = await ExecuteApiCall(async client => await client.GetGradeHistoryAsync(DtoMapper.ToOpenApi(filter), cancellationToken));
            return new ResponseDto<PagedResult<Domain.Common.DataObjects.Grade.GradeHistoryDTO>>
            {
                Data = result.Data.ToDomain(),
                Message = "Done"
            };
        }

        public async Task<ResponseDto<PagedResult<Domain.Common.DataObjects.Grade.GradeDTO>>> GetGrade(Domain.Common.DataObjects.Grade.GradeFilterDTO filter, CancellationToken cancellationToken)
        {
            var result = await ExecuteApiCall(async client => await client.GetGradeAsync(DtoMapper.ToOpenApi(filter), cancellationToken));
            return new ResponseDto<PagedResult<Domain.Common.DataObjects.Grade.GradeDTO>>
            {
                Data = result.Data.ToDomain(),
                Message = "Done"
            };
        }

        public async Task<ResponseDto<Domain.Common.DataObjects.Grade.GradeStatisticsDTO>> GetGradeStatistics(Domain.Common.DataObjects.Grade.GradeFilterDTO filter, CancellationToken cancellationToken)
        {
            var result = await ExecuteApiCall(async client => await client.GetGradeStatisticsAsync(DtoMapper.ToOpenApi(filter), cancellationToken));
            return new ResponseDto<Domain.Common.DataObjects.Grade.GradeStatisticsDTO>
            {
                Data = result.Data.ToDomain(),
                Message = "Done"
            };
        }

        public async Task<ResponseDto<Domain.Common.DataObjects.Academic.GpaDTO>> GetStudentGPA(Guid studentId, CancellationToken cancellationToken)
        {
            var result = await ExecuteApiCall(async client => await client.GetStudentGPAAsync(studentId, cancellationToken));
            return new ResponseDto<Domain.Common.DataObjects.Academic.GpaDTO>
            {
                Data = result.Data.ToDomain(),
                Message = "Done"
            };
        }

        public async Task<ResponseDto<PagedResult<Domain.Common.DataObjects.Grade.GradeResourceDTO>>> GetGraderResources(Domain.Common.DataObjects.SchoolSupply.ResourceFilterDTO filter, CancellationToken cancellationToken)
        {
            var result = await ExecuteApiCall(async client => await client.GetGraderResourcesAsync(DtoMapper.ToOpenApi(filter), cancellationToken));
            return new ResponseDto<PagedResult<Domain.Common.DataObjects.Grade.GradeResourceDTO>>
            {
                Data =DtoMapper.ToDomain(result.Data),
                Message = "Done"
            };
        }

        public async Task<ResponseDto<ICollection<Domain.Common.DataObjects.Schedule.ScheduleDTO>>> GetScheduleByGradeSection(Guid gradeSectionId, CancellationToken cancellationToken)
        {
            var result = await ExecuteApiCall(async client => await client.GetScheduleByGradeSectionAsync(gradeSectionId, cancellationToken));
            return new ResponseDto<ICollection<Domain.Common.DataObjects.Schedule.ScheduleDTO>>
            {
                Data = DtoMapper.ToDomain(result.Data),
                Message = "Done"
            };
        }

        public async Task<ResponseDto<PagedResult<Domain.Common.DataObjects.Transfer.TransferRequestHistoryDTO>>> GetTransferHistory(Domain.Common.DataObjects.Transfer.TransferHistoryFilterDTO filter, CancellationToken cancellationToken)
        {
            var result = await ExecuteApiCall(async client => await client.GetTransferHistoryAsync(DtoMapper.ToOpenApi(filter), cancellationToken));
            return new ResponseDto<PagedResult<Domain.Common.DataObjects.Transfer.TransferRequestHistoryDTO>>
            {
                Data = result.Data.ToDomain(),
                Message = "Done"
            };
        }



        public async Task<ResponseDto<Domain.Common.DataObjects.Transfer.TransferRequestDetailDTO>> GetTransferDetails(Guid requestId, CancellationToken cancellationToken) 
            {
            var result = await ExecuteApiCall(async client => await client.GetTransferDetailsAsync(requestId, cancellationToken));
            return new ResponseDto<Domain.Common.DataObjects.Transfer.TransferRequestDetailDTO>
            {
                Data = result.Data.ToDomain(),
                Message = "Done"
            };
        }
        public async Task<ResponseDto<bool>> CancelTransferRequest(Guid requestId, CancellationToken cancellationToken) =>
            await ExecuteApiCall(async client => await client.CancelTransferRequestAsync(requestId, cancellationToken), "Transfer cancelled successfully");

        public async Task<ResponseDto<Domain.Common.DataObjects.Academic.AcademicYearDTO>> GetCurrentYear(CancellationToken cancellationToken)
        {
            var result = await ExecuteApiCall(async client => await client.GetCurrentYearAsync(cancellationToken));
            return new ResponseDto<Domain.Common.DataObjects.Academic.AcademicYearDTO>
            {
                Data = result.Data.ToDomain(),
                Message = "Done"
            };
        }
        public async Task<ResponseDto<ICollection<Domain.Common.DataObjects.Attendance.AttendanceDTO>>> GetAttendanceRecords(Guid studentId, CancellationToken cancellationToken)
          {
            var result = await ExecuteApiCall(async client => await client.GetAttendanceRecordsAsync(studentId, cancellationToken));
            return new ResponseDto<ICollection<Domain.Common.DataObjects.Attendance.AttendanceDTO>>
            {
                Data = result.Data.ToDomain(),
                Message = "Done"
            };
           }
public async Task<ResponseDto<Domain.Common.DataObjects.Enrollment.EnrollmentDetailDTO>> GetEnrollment(Guid id, CancellationToken cancellationToken)
        {
            var result = await ExecuteApiCall(async client => await client.GetEnrollmentAsync(id, cancellationToken));
            return new ResponseDto<Domain.Common.DataObjects.Enrollment.EnrollmentDetailDTO>
            {
                Data = result.Data.ToDomain(),
                Message = "Done"
            };
        }
            

        public async Task<ResponseDto<Domain.Common.DataObjects.Enrollment.EnrollmentTranscriptDTO>> GetEnrollmentTranscript(Guid id, CancellationToken cancellationToken)
            
         {
            var result = await ExecuteApiCall(async client => await client.GetEnrollmentTranscriptAsync(id, cancellationToken));
            return new ResponseDto<Domain.Common.DataObjects.Enrollment.EnrollmentTranscriptDTO>
            {
                Data = result.Data.ToDomain(),
                Message = "Done"
            };
}
        public async Task<ResponseDto<bool>> SubmitEnrollment(Domain.Common.DataObjects.Enrollment.EnrollmentDTO dto, CancellationToken cancellationToken) =>
            await ExecuteApiCall(async client => await client.SubmitEnrollmentAsync(DtoMapper.ToOpenApi(dto), cancellationToken), "Enrollment submitted successfully");
        public async Task<ResponseDto<ICollection<Domain.Common.DataObjects.Course.CourseDTO>>> GetCoursesByGradeLevel(Guid gradeLevelId, CancellationToken cancellationToken)
        {
            var result = await ExecuteApiCall(async client =>
                await client.GetCoursesByGradeLevelAsync(gradeLevelId, cancellationToken));
            return new ResponseDto<ICollection<Domain.Common.DataObjects.Course.CourseDTO>>
            {
                Data = result.Data.Select(DtoMapper.ToDomain).ToList(),
                Message = "Done"
            };
        }
        public async Task<ResponseDto<Domain.Common.DataObjects.Course.CourseDetailDTO>> GetCourseDetails(Guid courseId, CancellationToken cancellationToken)
        {
            var result = await ExecuteApiCall(async client =>
                await client.GetCourseDetailsAsync(courseId, cancellationToken));
            return new ResponseDto<Domain.Common.DataObjects.Course.CourseDetailDTO>
            {
                Data = result.Data.ToDomain(),
                Message = "Done"
            };
        }
        public async Task<ResponseDto<bool>> SubmitAttendanceExcuse(Guid studentId, Domain.Common.DataObjects.Attendance.AttendanceExcuseDTO dto, CancellationToken cancellationToken)
        {
            return await ExecuteApiCall(async client =>
                await client.SubmitAttendanceExcuseAsync(studentId, DtoMapper.ToOpenApi(dto), cancellationToken), "Excuse submitted");
        }
        public async Task<ResponseDto<Domain.Common.DataObjects.Attendance.AttendanceSummaryDTO>> GetAttendanceSummary(Guid studentId, Domain.Common.DataObjects.Common.DateRangeDTO data, CancellationToken cancellationToken)
        {
            var result = await ExecuteApiCall(async client =>
                await client.GetAttendanceSummaryAsync(studentId, DtoMapper.ToOpenApi(data), cancellationToken));
            return new ResponseDto<Domain.Common.DataObjects.Attendance.AttendanceSummaryDTO>
            {
                Data = result.Data.ToDomain(),
                Message = "Done"
            };
        }

        #region Helpers

        private async Task<ResponseDto<T>> ExecuteApiCall<T>(Func<FrontofficeServicesClient, Task<T>> apiCall, string successMessage = "")
        {
            try
            {
                var client = new FrontofficeServicesClient(_httpClient.BaseAddress!.AbsoluteUri, _httpClient, _httpContextAccessor);
                var result = await apiCall(client);

                return new ResponseDto<T>
                {
                    Data = result,
                    Message = string.IsNullOrWhiteSpace(successMessage) ? "Request successful" : successMessage
                };
            }
            catch (ApiException e) when (e.StatusCode == StatusCodes.Status400BadRequest)
            {
                throw new BusinessValidationException(getErreurMessage(e.Message));
            }
            catch (ApiException e) when (e.StatusCode == StatusCodes.Status404NotFound)
            {
                throw new NotFoundException(getErreurMessage(e.Message));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string getErreurMessage(string erruer)
        {
            return erruer.Split("detail")[1].RemoveChars(['\\', '}', '"']);
        }

        #endregion
    }
}