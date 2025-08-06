using Microsoft.EntityFrameworkCore;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Common.DataObjects.Academic;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class AcademicsService : IAcademicsService
    {
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly IServiceHelper _serviceHelper;

        public AcademicsService(IBackofficeContext dbContext, IBackofficeReadOnlyContext dbReadOnlyContext, IServiceHelper serviceHelper)
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _serviceHelper = serviceHelper;
        }

        public async Task<bool> CreateAcademicYearAsync(AcademicYearDTO data, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var year = new AcademicYear
                {
                    Id = Guid.NewGuid(),
                    StartYear = data.StartYear,
                    EndYear = data.EndYear,
                    Description = data.Description
                };

                await _dbContext.AcademicYears.AddAsync(year, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<bool> UpdateAcademicYearAsync(Guid academicYearId, AcademicYearDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var year = await _dbContext.AcademicYears.FirstOrDefaultAsync(a => a.Id == academicYearId, cancellationToken);
                if (year == null)
                    throw new KeyNotFoundException("Academic year not found.");

                year.StartYear = dto.StartYear;
                year.EndYear = dto.EndYear;
                year.Description = dto.Description;
                year.LastModified = DateTime.UtcNow;

                _dbContext.AcademicYears.Update(year);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<AcademicYearDTO> GetCurrentAcademicYearAsync()
        {
            try
            {
                var result = await _dbReadOnlyContext.AcademicYears
                    .IgnoreQueryFilters()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.StartYear <= DateTime.Today.Year && a.EndYear >= DateTime.Today.Year);

                if (result == null)
                    throw new InvalidOperationException("Current Academic Year not found.");

                return new AcademicYearDTO
                {
                    StartYear = result.StartYear,
                    EndYear = result.EndYear,
                    Description = result.Description
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetCurrentAcademicYearAsync error: {ex}");
                return null;
            }
        }

        public async Task<bool> CreateSemesterAsync(SemesterDTO semesterDto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                // Get the current academic year to set the AcademicYearId for the semester
                var currentYear = await _dbReadOnlyContext.AcademicYears
                    .IgnoreQueryFilters()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.StartYear <= DateTime.Today.Year && a.EndYear >= DateTime.Today.Year, cancellationToken);
                if (currentYear == null)
                    throw new InvalidOperationException("Current Academic Year not found.");

                var semester = new Semester
                {
                    Id = Guid.NewGuid(),
                    Name = semesterDto.Name,
                    StartDate = semesterDto.StartDate,
                    EndDate = semesterDto.EndDate,
                    AcademicYearId = currentYear.Id
                };

                await _dbContext.Semesters.AddAsync(semester, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<SemesterDTO> GetSemesterByIdAsync(Guid semesterId, CancellationToken cancellationToken)
        {
            try
            {
                var semester = await _dbReadOnlyContext.Semesters
                    .AsNoTracking()
                    .Include(s => s.AcademicYear)
                    .FirstOrDefaultAsync(s => s.Id == semesterId, cancellationToken);

                if (semester == null)
                    throw new KeyNotFoundException("Semester not found.");

                return new SemesterDTO
                {
                    Id = semester.Id,
                    Name = semester.Name,
                    StartDate = semester.StartDate,
                    EndDate = semester.EndDate,
                    AcademicYear = new AcademicYearDTO
                    {
                        StartYear = semester.AcademicYear.StartYear,
                        EndYear = semester.AcademicYear.EndYear,
                        Description = semester.AcademicYear.Description
                    }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetSemesterByIdAsync error: {ex}");
                return null;
            }
        }

        // Returns a paginated list of semesters for a given academic year.
        public async Task<PagedResult<SemesterDTO>> GetPaginatedSemestersForYearAsync(Guid academicYearId, FilterSemesterDTO filter, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbReadOnlyContext.Semesters
                    .IgnoreQueryFilters()
                    .AsNoTracking()
                    .Where(s => s.AcademicYearId == academicYearId);

                // Apply sorting based on filter.SortBy (default is by Name)
                if (!string.IsNullOrEmpty(filter.SortBy))
                {
                    query = filter.IsAscending
                        ? query.OrderBy(s => s.Name)
                        : query.OrderByDescending(s => s.Name);
                }
                else
                {
                    query = query.OrderBy(s => s.Name);
                }

                var totalCount = await query.CountAsync(cancellationToken);
                var semesters = await query
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .Include(s => s.AcademicYear)
                    .ToListAsync(cancellationToken);

                var semesterDtos = semesters.Select(s => new SemesterDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    AcademicYear = new AcademicYearDTO
                    {
                        StartYear = s.AcademicYear.StartYear,
                        EndYear = s.AcademicYear.EndYear,
                        Description = s.AcademicYear.Description
                    }
                }).ToList();

                return new PagedResult<SemesterDTO>
                {
                    Results = semesterDtos,
                    RowCount = totalCount,
                    CurrentPage = filter.PageNumber,
                    PageSize = filter.PageSize,
                    PageCount = (int)Math.Ceiling((double)totalCount / filter.PageSize)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetPaginatedSemestersForYearAsync error: {ex}");
                return new PagedResult<SemesterDTO>
                {
                    Results = new List<SemesterDTO>(),
                    RowCount = 0,
                    CurrentPage = filter.PageNumber,
                    PageSize = filter.PageSize,
                    PageCount = 0
                };
            }
        }
    }
}
