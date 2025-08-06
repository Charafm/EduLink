using Microsoft.EntityFrameworkCore;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice.Resources;
using SchoolSaas.Domain.Backoffice.Traceability;
using SchoolSaas.Domain.Common.DataObjects.Book;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using SchoolSaas.Domain.Common.DataObjects.SchoolSupply;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class SchoolSupplyService : ISchoolSupplyService
    {
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly IServiceHelper _serviceHelper;

        public SchoolSupplyService(
            IBackofficeContext dbContext,
            IBackofficeReadOnlyContext dbReadOnlyContext,
            IServiceHelper serviceHelper)
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _serviceHelper = serviceHelper;
        }

        public async Task<bool> CreateSchoolSupplyAsync(SchoolSupplyDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                if (await SupplyExists(dto.NameFr, cancellationToken))
                    throw new InvalidOperationException("School supply already exists");

                var supply = new SchoolSupply
                {
                    NameFr = dto.NameFr,
                    NameAr = dto.NameAr,
                    NameEn = dto.NameEn,
                    DescriptionFr = dto.DescriptionFr,
                    DescriptionAr = dto.DescriptionAr,
                    DescriptionEn = dto.DescriptionEn
                };

                await _dbContext.SchoolSupplies.AddAsync(supply, cancellationToken);
                //await LogSupplyHistory(supply, ResourceActionType.Created, cancellationToken);
                return true;
            });
        }
        public async Task<bool> DeleteSupplyAsync(Guid supplyId, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var supply = await _dbContext.SchoolSupplies
                    .FirstOrDefaultAsync(ss => ss.Id == supplyId, cancellationToken);

                if (supply == null)
                    throw new KeyNotFoundException("Supply not found.");

                _dbContext.SchoolSupplies.Remove(supply);

                //await LogSupplyHistory(supply, ResourceActionType.Deleted, cancellationToken);
                return true;
            });
        }

        public async Task<bool> UpdateSchoolSupplyAsync(Guid supplyId, SchoolSupplyDTO dto,
            CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var supply = await GetSupplyEntity(supplyId, cancellationToken);
                var originalValues = CaptureOriginalValues(supply);

                supply.NameFr = dto.NameFr;
                supply.NameAr = dto.NameAr;
                supply.NameEn = dto.NameEn;
                supply.DescriptionFr = dto.DescriptionFr;
                supply.DescriptionAr = dto.DescriptionAr;
                supply.DescriptionEn = dto.DescriptionEn;

                //await LogSupplyHistory(supply, ResourceActionType.Updated,cancellationToken, originalValues);
                return true;
            });
        }

        public async Task<PagedResult<SchoolSupplyDTO>> GetPaginatedSuppliesAsync(
            SchoolSupplyFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = BuildSupplyQuery(filter);

            var totalCount = await query.CountAsync(cancellationToken);
            var results = await query
                .OrderBy(s => s.NameFr)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(s => new SchoolSupplyDTO
                {
             
                    NameFr = s.NameFr,
                    NameAr = s.NameAr,
                    NameEn = s.NameEn,
                    DescriptionFr = s.DescriptionFr,
                    DescriptionAr = s.DescriptionAr,
                    DescriptionEn = s.DescriptionEn
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<SchoolSupplyDTO>
            {
                Results = results,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };
        }

        public async Task<bool> BulkCreateSuppliesAsync(BulkSchoolSupplyDTO dto,
            CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var supplies = dto.Supplies.Select(s => new SchoolSupply
                {
                    NameFr = s.NameFr,
                    NameAr = s.NameAr,
                    NameEn = s.NameEn,
                    DescriptionFr = s.DescriptionFr,
                    DescriptionAr = s.DescriptionAr,
                    DescriptionEn = s.DescriptionEn
                });

                await _dbContext.SchoolSupplies.AddRangeAsync(supplies, cancellationToken);
                await LogBulkSupplyHistory(supplies);
                return true;
            });
        }
        public async Task<bool> CreateBookAsync(BookDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var book = new Book
                {
                    TitleFr = dto.TitleFr,
                    TitleAr = dto.TitleAr,
                    TitleEn = dto.TitleEn,
                    AuthorNameFr = dto.AuthorNameFr,
                    AuthorNameAr = dto.AuthorNameAr,
                    AuthorNameEn = dto.AuthorNameEn,
                    ISBN = dto.ISBN,
                    Subject = dto.Subject
                };

                await _dbContext.Books.AddAsync(book, cancellationToken);
                await LogResourceHistory(book, ResourceActionType.Created, cancellationToken);
                return true;
            });
        }

        public async Task<bool> UpdateBookAsync(Guid bookId, BookDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var book = await _dbContext.Books
                    .FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken)
                    ?? throw new KeyNotFoundException("Book not found");

                var originalValues = new BookHistoryDTO
                {
                    TitleFr = book.TitleFr,
                    TitleAr = book.TitleAr,
                    ISBN = book.ISBN
                };

                book.TitleFr = dto.TitleFr;
                book.TitleAr = dto.TitleAr;
                book.TitleEn = dto.TitleEn;
                book.AuthorNameFr = dto.AuthorNameFr;
                book.AuthorNameAr = dto.AuthorNameAr;
                book.AuthorNameEn = dto.AuthorNameEn;
                book.ISBN = dto.ISBN;
                book.Subject = dto.Subject;

                await LogResourceHistory(book, ResourceActionType.Updated, cancellationToken, originalValues);
                return true;
            });
        }

        public async Task<bool> AssignResourceToGradeAsync(GradeResourceDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                if (await ResourceAssignmentExists(dto))
                    return false;

                var assignment = new GradeResource
                {
                    GradeLevelId = dto.GradeLevelId,
                    BookId = dto.BookId,
                    SupplyId = dto.SchoolSupplyId,
                    SupplyQuantity = dto.SupplyQuantity
                };

                await _dbContext.GradeResources.AddAsync(assignment, cancellationToken);
                await LogAssignmentHistory(assignment, cancellationToken);
                return true;
            });
        }

        public async Task<PagedResult<GradeResourceDTO>> GetPaginatedResourcesAsync(
            ResourceFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = BuildResourceQuery(filter);

            var totalCount = await query.CountAsync(cancellationToken);
            var results = await query
                .OrderBy(r => r.GradeLevel.TitleFr)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(r => new GradeResourceDTO
                {
                    Id = r.Id,
                    GradeLevelId = r.GradeLevelId,
                    BookId = r.BookId,
                    SchoolSupplyId = r.SupplyId,
                    SupplyQuantity = r.SupplyQuantity,
                    GradeLevelName = r.GradeLevel.TitleFr,
                    ResourceTitle = r.Book != null ? r.Book.TitleFr : r.Supply.NameFr
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<GradeResourceDTO>
            {
                Results = results,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };
        }

        public async Task<bool> BulkAssignResourcesAsync(BulkResourceAssignmentDTO dto,
            CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var assignments = dto.Assignments.Select(a => new GradeResource
                {
                    GradeLevelId = a.GradeLevelId,
                    BookId = a.BookId,
                    SupplyId = a.SchoolSupplyId,
                    SupplyQuantity = a.SupplyQuantity
                });

                await _dbContext.GradeResources.AddRangeAsync(assignments, cancellationToken);
                await LogBulkAssignmentHistory(assignments);
                return true;
            });
        }
        #region Private Helpers
        private async Task<SchoolSupply> GetSupplyEntity(Guid supplyId, CancellationToken ct)
        {
            return await _dbContext.SchoolSupplies
                .FirstOrDefaultAsync(s => s.Id == supplyId, ct)
                ?? throw new KeyNotFoundException("School supply not found");
        }

        private IQueryable<SchoolSupply> BuildSupplyQuery(SchoolSupplyFilterDTO filter)
        {
            var query = _dbReadOnlyContext.SchoolSupplies.AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(s =>
                    s.NameFr.Contains(filter.SearchTerm) ||
                    s.DescriptionFr.Contains(filter.SearchTerm));
            }

            return query;
        }

        private async Task<bool> SupplyExists(string nameFr, CancellationToken ct)
        {
            return await _dbContext.SchoolSupplies
                .AnyAsync(s => s.NameFr == nameFr, ct);
        }

        private async Task LogSupplyHistory(SchoolSupply supply, ResourceActionType actionType,CancellationToken cancellationToken,
            SchoolSupplyHistoryDTO oldValues = null)
        {
            var history = new SchoolSupplyHistory
            {
                SupplyId = supply.Id,
                ActionType = actionType,
                OldValues = oldValues != null ? System.Text.Json.JsonSerializer.Serialize(oldValues) : null,
                ChangedBy = "System", // Replace with actual user context
                ChangedAt = DateTime.UtcNow
            };

            await _dbContext.SchoolSupplyHistories.AddAsync(history);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private SchoolSupplyHistoryDTO CaptureOriginalValues(SchoolSupply supply)
        {
            return new SchoolSupplyHistoryDTO
            {
                NameFr = supply.NameFr,
                DescriptionFr = supply.DescriptionFr
            };
        }

        private async Task LogBulkSupplyHistory(IEnumerable<SchoolSupply> supplies)
        {
            var histories = supplies.Select(s => new SchoolSupplyHistory
            {
                SupplyId = s.Id,
                ActionType = ResourceActionType.Created,
                ChangedBy = "System",
                ChangedAt = DateTime.UtcNow
            });

            await _dbContext.SchoolSupplyHistories.AddRangeAsync(histories);
        }
        private IQueryable<GradeResource> BuildResourceQuery(ResourceFilterDTO filter)
        {
            var query = _dbReadOnlyContext.GradeResources
                .Include(gr => gr.GradeLevel)
                .Include(gr => gr.Book)
                .Include(gr => gr.Supply)
                .AsQueryable();

            if (filter.GradeLevelId.HasValue)
                query = query.Where(gr => gr.GradeLevelId == filter.GradeLevelId.Value);

            if (filter.ResourceType.HasValue)
            {
                query = filter.ResourceType == ResourceType.Book
                    ? query.Where(gr => gr.BookId != null)
                    : query.Where(gr => gr.SupplyId != null);
            }

            return query;
        }

        private async Task<bool> ResourceAssignmentExists(GradeResourceDTO dto)
        {
            return await _dbContext.GradeResources
                .AnyAsync(gr => gr.GradeLevelId == dto.GradeLevelId &&
                    (gr.BookId == dto.BookId || gr.SupplyId == dto.SchoolSupplyId));
        }

        private async Task LogResourceHistory(Book book, ResourceActionType actionType,
            CancellationToken cancellationToken, BookHistoryDTO oldValues = null)
        {
            var history = new ResourceHistory
            {
                ResourceId = book.Id,
                ResourceType = ResourceType.Book,
                ActionType = actionType,
                OldValues = oldValues != null ? System.Text.Json.JsonSerializer.Serialize(oldValues) : null,
                ChangedBy = "System", // Replace with actual user context
                ChangedAt = DateTime.UtcNow
            };

            await _dbContext.ResourceHistories.AddAsync(history);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task LogAssignmentHistory(GradeResource assignment, CancellationToken cancellation)
        {
            var history = new SupplyAssignmentHistory
            {
                AssignmentId = assignment.Id,
                Action = SupplyAssignmentActionEnum.Assign,
                UserId = assignment.CreatedBy,
                ChangeDate = DateTime.UtcNow,
                GradeLevel = assignment.GradeLevelId,
                SchoolSupply = assignment.SupplyId,
                Comment = "assignment",
                GradeResource = assignment.Id
            };

            await _dbContext.SupplyAssignmentHistories.AddAsync(history);
        }

        private async Task LogBulkAssignmentHistory(IEnumerable<GradeResource> assignments)
        {
            var histories = assignments.Select(a => new SupplyAssignmentHistory
            {
                AssignmentId = a.Id,
                ChangeDate = DateTime.UtcNow,
                UserId = a.CreatedBy,
                Action = SupplyAssignmentActionEnum.Assign,
                GradeLevel = a.GradeLevelId,
                SchoolSupply = a.SupplyId,
                Comment = "Bulk assignment",
                GradeResource = a.Id
            });

            await _dbContext.SupplyAssignmentHistories.AddRangeAsync(histories);
        }
        #endregion
    }
}
