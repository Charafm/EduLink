using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Academic;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IAcademicsService
    {
        Task<bool> CreateAcademicYearAsync(AcademicYearDTO year, CancellationToken cancellationToken);
        Task<bool> UpdateAcademicYearAsync(Guid academicYearId, AcademicYearDTO dto, CancellationToken cancellationToken);
        Task<AcademicYearDTO> GetCurrentAcademicYearAsync();

        Task<bool> CreateSemesterAsync(SemesterDTO semesterDto, CancellationToken cancellationToken);
        Task<SemesterDTO> GetSemesterByIdAsync(Guid semesterId, CancellationToken cancellationToken);
        Task<PagedResult<SemesterDTO>> GetPaginatedSemestersForYearAsync(Guid academicYearId, FilterSemesterDTO filter, CancellationToken cancellationToken);
    }
}
