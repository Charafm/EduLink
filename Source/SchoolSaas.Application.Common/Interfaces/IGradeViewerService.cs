using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Academic;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IGradeViewerService
    {
        Task<PagedResult<GradeDTO>> GetPaginatedGradesAsync(GradeFilterDTO filter, CancellationToken cancellationToken);
        Task<GradeStatisticsDTO> GetGradeStatisticsAsync(GradeFilterDTO filter, CancellationToken cancellationToken);
        Task<PagedResult<GradeHistoryDTO>> GetGradeHistoryAsync(GradeHistoryFilterDTO filter, CancellationToken cancellationToken);
        Task<GpaDTO> CalculateGPAForStudentAsync(Guid studentId, CancellationToken cancellationToken);
        Task<bool> SubmitGradeAppealAsync(GradeAppealDTO dto, CancellationToken cancellationToken);

    }
}
