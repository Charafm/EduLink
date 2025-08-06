using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Academic;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Backoffice.Services.FrontofficeServices
{
    public  class GradeViewerService : IGradeViewerService
    {
        private readonly IGradebookService _service;

        public GradeViewerService(IGradebookService service)
        {
            _service = service;
        }
        public async Task<PagedResult<GradeDTO>> GetPaginatedGradesAsync(GradeFilterDTO filter, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedGradesAsync(filter, cancellationToken);
        }
        public async Task<GradeStatisticsDTO> GetGradeStatisticsAsync(GradeFilterDTO filter, CancellationToken cancellationToken)
        {
            return await _service.GetGradeStatisticsAsync(filter, cancellationToken);
        }
        public async Task<PagedResult<GradeHistoryDTO>> GetGradeHistoryAsync(GradeHistoryFilterDTO filter, CancellationToken cancellationToken)
        {
            return await _service.GetGradeHistoryAsync(filter, cancellationToken);
        }
        public async Task<GpaDTO> CalculateGPAForStudentAsync(Guid studentId, CancellationToken cancellationToken)
        {
            return await _service.CalculateGPAForStudentAsync(studentId, cancellationToken);
        }
        public async Task<bool> SubmitGradeAppealAsync(GradeAppealDTO dto, CancellationToken cancellationToken)
        {
            return await _service.SubmitGradeAppealAsync(dto, cancellationToken);
        }
    }
}
