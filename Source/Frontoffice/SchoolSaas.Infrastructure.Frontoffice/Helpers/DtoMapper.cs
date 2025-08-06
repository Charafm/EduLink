using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OpenApi = SchoolSaas.Infrastructure.Frontoffice.OpenAPIs.BackofficeService;
using Domain = SchoolSaas.Domain.Common.DataObjects;
using SchoolSaas.Application.Common.Models;



namespace SchoolSaas.Infrastructure.Frontoffice.Helpers
{
    public static class DtoMapper 

    {
        public static OpenAPIs.BackofficeService.EnrollmentDocumentUploadDTO ToOpenApi(this Domain.Common.DataObjects.Enrollment.EnrollmentDocumentUploadDTO dto)
        {
            return new OpenAPIs.BackofficeService.EnrollmentDocumentUploadDTO
            {
                EnrollmentId = dto.EnrollmentId,
                FileName = dto.FileName,
                ContentType = dto.ContentType,
                FileContent = dto.FileContent,
                DocumentType = (OpenAPIs.BackofficeService.DocumentTypeEnum)dto.DocumentType
            };
        }
        public static OpenAPIs.BackofficeService.EnrollmentDTO ToOpenApi(this Domain.Common.DataObjects.Enrollment.EnrollmentDTO dto)
        {
            return new OpenAPIs.BackofficeService.EnrollmentDTO
            {
                StudentId = dto.StudentId,
                BranchId = dto.BranchId,
                Status = (OpenApi.EnrollmentStatusEnum)dto.Status,
                SubmittedAt = dto.SubmittedAt,
                AdminComment = dto.AdminComment,
            };
        }
        public static ICollection<Domain.Common.DataObjects.Attendance.AttendanceDTO> ToDomain(this ICollection<OpenAPIs.BackofficeService.AttendanceDTO> list)
        {
            return list.Select(dto => new Domain.Common.DataObjects.Attendance.AttendanceDTO
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                Date = dto.Date.UtcDateTime,
                NotesFr = dto.NotesFr,
                NotesAr = dto.NotesAr,
                NotesEn = dto.NotesEn,
                Status = (Domain.Common.Enums.AttendanceEnum)dto.Status
            }).ToList();
        }
        public static Domain.Common.DataObjects.Academic.GpaDTO ToDomain(this OpenAPIs.BackofficeService.GpaDTO dto)
        {
            return new Domain.Common.DataObjects.Academic.GpaDTO
            {
                StudentId = dto.StudentId,
                GPA = dto.Gpa
              
            };

        }
        public static Domain.Common.DataObjects.Academic.AcademicYearDTO ToDomain(this OpenAPIs.BackofficeService.AcademicYearDTO dto)
        {
            return  new Domain.Common.DataObjects.Academic.AcademicYearDTO
            {
                StartYear = dto.StartYear,
                EndYear = dto.EndYear,
                Description = dto.Description,
            };
        }
        public static Domain.Common.DataObjects.Enrollment.EnrollmentDetailDTO ToDomain(this OpenAPIs.BackofficeService.EnrollmentDetailDTO dto)
        {
            return new Domain.Common.DataObjects.Enrollment.EnrollmentDetailDTO
            {
             
                StudentId = dto.StudentId,
                BranchId = dto.BranchId,
                 
                Status = (Domain.Common.Enums.EnrollmentStatusEnum)dto.Status,
                SubmittedAt = dto.SubmittedAt.UtcDateTime,
                StudentName = dto.StudentName,
                BranchName = dto.BranchName,
                Documents = dto.Documents?.Select(d => new Domain.Common.DataObjects.Common.DocumentDTO
                {
                    FilePath = d.FilePath,
                    DocumentType = (Domain.Common.Enums.DocumentTypeEnum)d.DocumentType,
                    UploadedAt = DateTime.Parse(d.UploadedAt.ToString())
                }).ToList()
            };
        }

        public static Domain.Common.DataObjects.Enrollment.EnrollmentTranscriptDTO ToDomain(this OpenAPIs.BackofficeService.EnrollmentTranscriptDTO dto)
        {
            return new Domain.Common.DataObjects.Enrollment.EnrollmentTranscriptDTO
            {
                EnrollmentId = dto.EnrollmentId,
                StudentId = dto.StudentId,
                StudentName = dto.StudentName,
                BranchName = dto.BranchName,
                Status = (Domain.Common.Enums.EnrollmentStatusEnum)dto.Status,
                SubmittedAt = dto.SubmittedAt.UtcDateTime,
                Documents = dto.Documents?.Select(d => new Domain.Common.DataObjects.Common.DocumentDTO
                {
                    FilePath = d.FilePath,
                    DocumentType = (Domain.Common.Enums.DocumentTypeEnum)d.DocumentType,
                    UploadedAt = DateTime.Parse(d.UploadedAt.ToString()),
                }).ToList(),
                StatusHistory = dto.StatusHistory?.Select(h => new Domain.Common.DataObjects.Enrollment.EnrollmentStatusHistoryDTO
                {
                    EnrollmentId= h.EnrollmentId,
                    OldStatus = (Domain.Common.Enums.EnrollmentStatusEnum)h.OldStatus,
                    NewStatus = (Domain.Common.Enums.EnrollmentStatusEnum)h.NewStatus,
                    ChangedAt = h.ChangedAt.UtcDateTime,
                    ChangedBy = h.ChangedBy,
                    ChangeReason = (Domain.Common.Enums.EnrollmentChangeReasonEnum)h.ChangeReason
                }).ToList()
            };
        }
        public static Domain.Common.DataObjects.Transfer.TransferEligibilityResultDTO ToDomain(this OpenAPIs.BackofficeService.TransferEligibilityResultDTO dto)
        {
            return new Domain.Common.DataObjects.Transfer.TransferEligibilityResultDTO
            {
                IsEligible = dto.IsEligible,
                PendingFees = dto.PendingFees,
                DisciplinaryIssues = dto.DisciplinaryIssues
            };
        }

        public static OpenAPIs.BackofficeService.TransferRequestDTO ToOpenApi(this Domain.Common.DataObjects.Transfer.TransferRequestDTO dto)
        {
            return new OpenAPIs.BackofficeService.TransferRequestDTO
            {
                StudentId = dto.StudentId,
                FromBranchId = dto.FromBranchId,
                ToBranchId = dto.ToBranchId,
                FromSchoolId = dto.FromSchoolId,
                ToSchoolId = dto.ToSchoolId,
                Reason = dto.Reason,
                Documents = dto.Documents?.Select(d => new OpenAPIs.BackofficeService.DocumentDTO
                {
                    DocumentType = (OpenAPIs.BackofficeService.DocumentTypeEnum)d.DocumentType,
                    FilePath = d.FilePath
                }).ToList(),
                SubmittedAt = DateTimeOffset.UtcNow,
                Status = OpenAPIs.BackofficeService.TransferRequestStatus.Pending
            };
        }
        public static OpenAPIs.BackofficeService.DateRangeDTO ToOpenApi(this Domain.Common.DataObjects.Common.DateRangeDTO dto) =>
    new() { StartDate = dto.StartDate, EndDate = dto.EndDate };

        public static Domain.Common.DataObjects.Attendance.AttendanceSummaryDTO ToDomain(this OpenAPIs.BackofficeService.AttendanceSummaryDTO dto) =>
            new() { TotalDays = dto.TotalDays, PresentDays = dto.PresentDays, AbsentDays = dto.AbsentDays, LateDays = dto.LateDays, AttendanceRate = dto.AttendanceRate };

        public static OpenAPIs.BackofficeService.AttendanceExcuseDTO ToOpenApi(this Domain.Common.DataObjects.Attendance.AttendanceExcuseDTO dto) =>
            new() { AttendanceId = dto.AttendanceId, Explanation = dto.Explanation, DocumentUrl = dto.DocumentUrl, Language = dto.Language, SubmittedBy  = dto.SubmittedBy  };

        public static Domain.Common.DataObjects.Course.CourseDetailDTO ToDomain(this OpenAPIs.BackofficeService.CourseDetailDTO dto) =>
            new() { TitleFr = dto.TitleFr, TitleAr = dto.TitleAr, TitleEn = dto.TitleEn, TeacherAssignments = (List<Domain.Common.DataObjects.Teacher.TeacherAssignmentDetailDTO>)dto.TeacherAssignments, Description = dto.Description , Materials = (List<Domain.Common.DataObjects.Course.CourseMaterialDTO>)dto.Materials, Code = dto.Code, GradeMappings = (List<Domain.Common.DataObjects.Grade.CourseGradeMappingDTO>)dto .GradeMappings };

        public static Domain.Common.DataObjects.Course.CourseDTO ToDomain(this OpenAPIs.BackofficeService.CourseDTO dto) =>
            new() { TitleFr = dto.TitleFr, TitleAr = dto.TitleAr, TitleEn = dto.TitleEn, Code = dto.Code, Description = dto.Description };
        public static OpenAPIs.BackofficeService.AttendanceHistoryFilterDTO ToOpenApi(this Domain.Common.DataObjects.Attendance.AttendanceHistoryFilterDTO dto)
    => new() { StudentId = dto.StudentId, DateFrom = dto.DateFrom, DateTo = dto.DateTo, PageNumber = dto.PageNumber, PageSize = dto.PageSize };

        public static OpenAPIs.BackofficeService.GradeHistoryFilterDTO ToOpenApi(this Domain.Common.DataObjects.Grade.GradeHistoryFilterDTO dto)
            => new() { GradeId = dto.GradeId, PageNumber = dto.PageNumber, PageSize = dto.PageSize };

        public static OpenAPIs.BackofficeService.GradeFilterDTO ToOpenApi(this Domain.Common.DataObjects.Grade.GradeFilterDTO dto)
            => new() { StudentId = dto.StudentId, CourseId = dto.CourseId, SemesterId = dto.SemesterId, MaxScore = dto.MaxScore, MinScore = dto.MinScore , PageNumber = dto.PageNumber, PageSize = dto.PageSize };
        // ATTENDANCE HISTORY
        public static PagedResult<Domain.Common.DataObjects.Attendance.AttendanceHistoryDTO> ToDomain(this OpenApi.PagedResultOfAttendanceHistoryDTO dto)
        {
            return new PagedResult<Domain.Common.DataObjects.Attendance.AttendanceHistoryDTO>
            {
                Results = dto.Results.Select(r => new Domain.Common.DataObjects.Attendance.AttendanceHistoryDTO
                {
                  AttendanceId = r.AttendanceId,
                  Date = DateTime.Parse(r.Date.ToString()),
                  ChangeReason = (Domain.Common.Enums.AttendanceChangeReasonEnum)r.ChangeReason,
                  ChangedAt = DateTime.Parse(r.ChangedAt.ToString()),
                  ChangedBy = r.ChangedBy,
                  Status = (Domain.Common.Enums.AttendanceEnum)r.Status,
                  StudentId = r.StudentId,
                  
                }).ToList(),
                CurrentPage = dto.CurrentPage,
                PageSize = dto.PageSize,
                PageCount = dto.PageCount,
                RowCount = dto.RowCount
            };
        }

        // GRADE HISTORY
        public static PagedResult<Domain.Common.DataObjects.Grade.GradeHistoryDTO> ToDomain(this OpenApi.PagedResultOfGradeHistoryDTO dto)
        {
            return new PagedResult<Domain.Common.DataObjects.Grade.GradeHistoryDTO>
            {
                Results = dto.Results.Select(r => new Domain.Common.DataObjects.Grade.GradeHistoryDTO
                {
                   GradeId = r.GradeId,
                  NewScore = r.NewScore,
                  OldScore = r.OldScore,
                  ModifiedAt = DateTime.Parse(r.ModifiedAt.ToString()),
                  ModifiedBy = r.ModifiedBy,
                }).ToList(),
                CurrentPage = dto.CurrentPage,
                PageSize = dto.PageSize,
                PageCount = dto.PageCount,
                RowCount = dto.RowCount
            };
        }

        // GRADE RESOURCES
        public static PagedResult<Domain.Common.DataObjects.Grade.GradeResourceDTO> ToDomain(this OpenApi.PagedResultOfGradeResourceDTO dto)
        {
            return new PagedResult<Domain.Common.DataObjects.Grade.GradeResourceDTO>
            {
                Results = dto.Results.Select(r => new Domain.Common.DataObjects.Grade.GradeResourceDTO
                {
                    ResourceTitle = r.ResourceTitle,
                    Id = r.Id,
                    GradeLevelId = r.GradeLevelId,
                    BookId= r.BookId,
                    GradeLevelName = r.GradeLevelName,
                    SchoolSupplyId = r.SchoolSupplyId,
                    SupplyQuantity = r.SupplyQuantity,
                    
                }).ToList(),
                CurrentPage = dto.CurrentPage,
                PageSize = dto.PageSize,
                PageCount = dto.PageCount,
                RowCount = dto.RowCount
            };
        }

        // TRANSFER HISTORY
        public static PagedResult<Domain.Common.DataObjects.Transfer.TransferRequestHistoryDTO> ToDomain(this OpenApi.PagedResultOfTransferRequestHistoryDTO dto)
        {
            return new PagedResult<Domain.Common.DataObjects.Transfer.TransferRequestHistoryDTO>
            {
                Results = dto.Results.Select(h => new Domain.Common.DataObjects.Transfer.TransferRequestHistoryDTO
                {
                    TransferRequestId = h.TransferRequestId,
                    OldStatus = (Domain.Common.Enums.TransferRequestStatus)h.OldStatus,
                    NewStatus = (Domain.Common.Enums.TransferRequestStatus)h.NewStatus,
                    ChangedAt = h.ChangedAt.UtcDateTime,
                    ChangedBy = h.ChangedBy,
                    ChangeReason = (Domain.Common.Enums.TransferRequestChangeReasonEnum)h.ChangeReason,
                    Reason = h.Reason
                }).ToList(),
                CurrentPage = dto.CurrentPage,
                PageSize = dto.PageSize,
                PageCount = dto.PageCount,
                RowCount = dto.RowCount
            };
        }

        public static OpenAPIs.BackofficeService.ResourceFilterDTO ToOpenApi(this Domain.Common.DataObjects.SchoolSupply.ResourceFilterDTO dto)
            => new() { GradeLevelId = dto.GradeLevelId, ResourceType = (OpenApi.ResourceType?)dto.ResourceType,  PageNumber = dto.PageNumber, PageSize = dto.PageSize };

        public static OpenAPIs.BackofficeService.TransferHistoryFilterDTO ToOpenApi(this Domain.Common.DataObjects.Transfer.TransferHistoryFilterDTO dto)
            => new() { TransferRequestId = dto.TransferRequestId, PageNumber = dto.PageNumber, PageSize = dto.PageSize };

        public static ICollection<Domain.Common.DataObjects.Schedule.ScheduleDTO> ToDomain(this ICollection<OpenAPIs.BackofficeService.ScheduleDTO> list)
            => list.Select(s => new Domain.Common.DataObjects.Schedule.ScheduleDTO
            {
                Id = s.Id,
                CourseId = s.CourseId,
                DayOfWeek = (DayOfWeek)s.DayOfWeek,
                TeacherId = s.TeacherId,
                Duration = s.Duration,
                ClassroomId = s.ClassroomId,
                GradeSectionId = s.GradeSectionId,
                StartTime = TimeOnly.Parse(s.StartTime.ToString()),
                EndTime = TimeOnly.Parse(s.EndTime.ToString()),
            }).ToList();
        public static PagedResult<Domain.Common.DataObjects.Grade.GradeDTO> ToDomain(this OpenApi.PagedResultOfGradeDTO dto)
    => new()
    {
        Results = dto.Results?.Select(r => r.ToDomain()).ToList(),
        CurrentPage = dto.CurrentPage,
        PageSize = dto.PageSize,
        RowCount = dto.RowCount,
        PageCount = dto.PageCount
    };

        public static Domain.Common.DataObjects.Grade.GradeDTO ToDomain(this OpenApi.GradeDTO dto)
            => new()
            {
                CourseId = dto.CourseId,
                GradeType = (Domain.Common.Enums.GradeTypeEnum)dto.GradeType,
                Id = dto.Id,
                Score = dto.Score,
                SemesterId = dto.SemesterId,
                TeacherCommentAr = dto.TeacherCommentAr,
                TeacherCommentEn= dto.TeacherCommentEn,
                TeacherCommentFr = dto.TeacherCommentFr,
                
                StudentId = dto.StudentId
            };
        public static Domain.Common.DataObjects.Grade.GradeStatisticsDTO ToDomain(this OpenApi.GradeStatisticsDTO dto)
    => new()
    {
        AverageScore = dto.AverageScore,
        GradeDistribution = (List<Domain.Common.DataObjects.Grade.GradeDistributionDTO>)dto.GradeDistribution,
        MaxScore = dto.MaxScore,
        MinScore = dto.MinScore,
    };

        // Add reverse mapping if needed
        public static Domain.Common.DataObjects.Transfer.TransferRequestDetailDTO ToDomain(this OpenAPIs.BackofficeService.TransferRequestDetailDTO dto)
        {
            return new Domain.Common.DataObjects.Transfer.TransferRequestDetailDTO
            {
                StudentId = dto.StudentId,
                FromBranchId = dto.FromBranchId,
                ToBranchId = dto.ToBranchId,
                FromSchoolId = dto.FromSchoolId,
                ToSchoolId = dto.ToSchoolId,
                Reason = dto.Reason,
                Status = (Domain.Common.Enums.TransferRequestStatus)dto.Status,
                SubmittedAt = DateTime.Parse(dto.SubmittedAt.ToString()),
                Documents = dto.Documents.Select(d => new SchoolSaas.Domain.Common.DataObjects.Common.DocumentDTO
                {
                    FilePath = d.FilePath,
                    DocumentType = (Domain.Common.Enums.DocumentTypeEnum)d.DocumentType,
                    UploadedAt = DateTime.Parse(d.UploadedAt.ToString()),
                }).ToList(),
                History = dto.History.Select(h => new Domain.Common.DataObjects.Transfer.TransferRequestHistoryDTO
                {
                    TransferRequestId = h.TransferRequestId,
                    OldStatus = (Domain.Common.Enums.TransferRequestStatus)h.OldStatus,
                    NewStatus = (Domain.Common.Enums.TransferRequestStatus)h.NewStatus,
                    ChangedAt = DateTime.Parse(h.ChangedAt.ToString()),
                    ChangedBy = h.ChangedBy,
                    ChangeReason = (Domain.Common.Enums.TransferRequestChangeReasonEnum)h.ChangeReason,
                    Reason = h.Reason
                }).ToList()
            };
        }
       
    }

}
