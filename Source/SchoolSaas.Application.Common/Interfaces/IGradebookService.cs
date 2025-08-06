using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Academic;
using SchoolSaas.Domain.Common.DataObjects.Grade;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IGradebookService
    {
        Task<bool> RecordGradeAsync(GradeDTO dto, CancellationToken cancellationToken);
        Task<bool> UpdateGradeAsync(Guid gradeId, GradeUpdateDTO dto, CancellationToken cancellationToken);
        Task<PagedResult<GradeDTO>> GetPaginatedGradesAsync(GradeFilterDTO filter, CancellationToken cancellationToken);
        Task<bool> BulkRecordGradesAsync(BulkGradeDTO dto, CancellationToken cancellationToken);
        Task<bool> BulkUpdateGradesAsync(BulkGradeUpdateDTO dto, CancellationToken cancellationToken);
        Task<GradeStatisticsDTO> GetGradeStatisticsAsync(GradeFilterDTO filter, CancellationToken cancellationToken);
        Task<bool> SubmitGradeAppealAsync(GradeAppealDTO dto, CancellationToken cancellationToken);
        Task<PagedResult<GradeAppealDTO>> GetGradeAppealsAsync(GradeAppealFilterDTO filter, CancellationToken cancellationToken);
        Task<PagedResult<GradeHistoryDTO>> GetGradeHistoryAsync(GradeHistoryFilterDTO filter, CancellationToken cancellationToken);
        Task<GpaDTO> CalculateGPAForStudentAsync(Guid studentId, CancellationToken cancellationToken);
        Task<PagedResult<GradeDTO>> GetGradesByTeacherAsync(Guid teacherId, GradeFilterDTO filter, CancellationToken cancellationToken);
        Task<bool> CalculateFinalGradesAsync(Guid courseId, Guid semesterId, CancellationToken cancellationToken);
    }
}
