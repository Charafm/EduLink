using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Academic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Backoffice.Services.FrontofficeServices
{
    public  class AcademicsViewerService : IAcademicsViewerService
    {
        private readonly IAcademicsService _service;

        public AcademicsViewerService(IAcademicsService service)
        {
            _service = service;
        }
        public async Task<AcademicYearDTO> GetCurrentAcademicYearAsync()
        {
            return await _service.GetCurrentAcademicYearAsync();
        }
        //public async Task<SemesterDTO> GetCurrentSemesterAsync()
        //{
        //    return await _service.GetPaginatedSemestersForYearAsync(await GetCurrentAcademicYearAsync().Id)
        //}
            public async Task<PagedResult<SemesterDTO>> GetPaginatedSemestersForYearAsync(Guid academicYearId, FilterSemesterDTO filter, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedSemestersForYearAsync(academicYearId, filter, cancellationToken);
        }
    }
}
