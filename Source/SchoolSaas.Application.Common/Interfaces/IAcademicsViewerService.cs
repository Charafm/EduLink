using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Academic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Common.Interfaces
{
    public  interface IAcademicsViewerService
    {
        Task<AcademicYearDTO> GetCurrentAcademicYearAsync();
      //  Task<SemesterDTO> GetCurrentSemesterAsync(); // Optional, you can implement logic for this
        Task<PagedResult<SemesterDTO>> GetPaginatedSemestersForYearAsync(Guid academicYearId, FilterSemesterDTO filter, CancellationToken cancellationToken);
    }
}
