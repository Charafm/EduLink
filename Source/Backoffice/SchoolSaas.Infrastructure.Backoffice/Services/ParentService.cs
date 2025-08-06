using Microsoft.EntityFrameworkCore;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Common.Enums;
using System.Text.Json;
using SchoolSaas.Domain.Common.DataObjects.Student;
using SchoolSaas.Domain.Common.DataObjects.Parent;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Notification;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class ParentService : IParentService
    {
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly IServiceHelper _serviceHelper;

        public ParentService(
            IBackofficeContext dbContext,
            IBackofficeReadOnlyContext dbReadOnlyContext,
            IServiceHelper serviceHelper)
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _serviceHelper = serviceHelper;
        }

        public async Task<bool> CreateParentAsync(ParentDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var parent = new Parent
                {
                    Id = Guid.NewGuid(),
                    UserId = dto.UserId,
                    //BranchId = dto.BranchId,
                    FirstNameFr = dto.FirstNameFr,
                    FirstNameAr = dto.FirstNameAr,
                    LastNameFr = dto.LastNameFr,
                    LastNameAr = dto.LastNameAr,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    CIN = dto.CIN,
                    Occupation = dto.Occupation,
                    AddressFr = dto.AddressFr,
                    AddressAr = dto.AddressAr,
                    IsIdentityVerified = false
                };

                await _dbContext.Parents.AddAsync(parent, cancellationToken);
                //await LogParentAudit(parent, ParentActionType.Created, "System", cancellationToken);
                return true;
            });
        }

        public async Task<bool> UpdateParentAsync(Guid parentId, ParentDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var parent = await _dbContext.Parents
                    .FirstOrDefaultAsync(p => p.Id == parentId, cancellationToken)
                    ?? throw new KeyNotFoundException("Parent not found");

                var originalValues = CaptureOriginalValues(parent);

                parent.FirstNameFr = dto.FirstNameFr;
                parent.FirstNameAr = dto.FirstNameAr;
                parent.LastNameFr = dto.LastNameFr;
                parent.LastNameAr = dto.LastNameAr;
                parent.Email = dto.Email;
                parent.Phone = dto.Phone;
                parent.CIN = dto.CIN;
                parent.Occupation = dto.Occupation;
                parent.AddressFr = dto.AddressFr;
                parent.AddressAr = dto.AddressAr;
                parent.LastModified = DateTime.UtcNow;

                //await LogParentAudit(parent, ParentActionType.Updated, "System", cancellationToken, originalValues);
                return true;
            });
        }

        public async Task<ParentDTO> GetParentByIdAsync(Guid parentId, CancellationToken cancellationToken)
        {
            var parent = await _dbReadOnlyContext.Parents
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == parentId, cancellationToken);

            return parent != null ? MapToDto(parent) : throw new KeyNotFoundException("Parent not found");
        }

        public async Task<PagedResult<ParentDTO>> GetPaginatedParentsAsync(ParentFilterDTO filter, CancellationToken cancellationToken)
        {
            //var ParentStudent = _dbReadOnlyContext.StudentParents.Include(p => p.StudentId == filter.StudentId).Select(ps => ps.Parent).ToList();
            var query = _dbReadOnlyContext.Parents
                .AsNoTracking();

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(p =>
                    p.FirstNameFr.Contains(filter.SearchTerm) ||
                    p.FirstNameAr.Contains(filter.SearchTerm) ||
                    p.LastNameFr.Contains(filter.SearchTerm) ||
                    p.CIN.Contains(filter.SearchTerm));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var results = await query
                .OrderBy(p => p.LastNameFr)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(p => MapToDto(p))
                .ToListAsync(cancellationToken);

            return new PagedResult<ParentDTO>
            {
                Results = results,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };
        }

        public async Task<bool> DeleteParentAsync(Guid parentId, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var parent = await _dbContext.Parents
                    .FirstOrDefaultAsync(p => p.Id == parentId, cancellationToken)
                    ?? throw new KeyNotFoundException("Parent not found");

                parent.IsDeleted = true;
                parent.LastModified = DateTime.UtcNow;

                //await LogParentAudit(parent, ParentActionType.Deleted, "System", cancellationToken);
                return true;
            });
        }
        public async Task<bool> UpdateContactPreferencesAsync(Guid parentId, ContactPreferencesDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var parent = await _dbContext.Parents
                    .FirstOrDefaultAsync(p => p.Id == parentId, cancellationToken)
                    ?? throw new KeyNotFoundException("Parent not found");

                //SEND NOTIFICATION 
                //parent.NotificationPreferencesJson = JsonSerializer.Serialize(dto);

                parent.LastModified = DateTime.UtcNow;

                _dbContext.Parents.Update(parent);
                //await LogParentAudit(parent, ParentActionType.Updated, "System", cancellationToken);
                return true;
            });
        }

       
        public async Task<bool> BulkCreateParentsAsync(BulkParentDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var parents = dto.Records.Select(r => new Parent
                {
                    Id = Guid.NewGuid(),
                    UserId = r.UserId,
                    //BranchId = r.BranchId,
                    FirstNameFr = r.FirstNameFr,
                    FirstNameAr = r.FirstNameAr,
                    LastNameFr = r.LastNameFr,
                    LastNameAr = r.LastNameAr,
                    Email = r.Email,
                    Phone = r.Phone,
                    CIN = r.CIN,
                    Occupation = r.Occupation,
                    AddressFr = r.AddressFr,
                    AddressAr = r.AddressAr,
                    IsIdentityVerified = false
                }).ToList();

                await _dbContext.Parents.AddRangeAsync(parents, cancellationToken);
                foreach (var parent in parents)
                {
                    //await LogParentAudit(parent, ParentActionType.Created, dto.InitiatedBy, cancellationToken);
                }

                return true;
            });
        }

        public async Task<List<ParentAuditDTO>> GetParentAuditLogsAsync(Guid parentId, CancellationToken cancellationToken)
        {
            return await _dbReadOnlyContext.ParentAudits
                .Where(a => a.ParentId == parentId)
                .OrderByDescending(a => a.ActionDate)
                .Select(a => new ParentAuditDTO
                {
                    ActionDate = a.ActionDate,
                    ActionType = a.ActionType,
                    Details = a.Details,
                    PerformedBy = a.PerformedBy
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> VerifyParentIdentityAsync(Guid parentId, ParentVerificationDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var parent = await _dbContext.Parents
                    .FirstOrDefaultAsync(p => p.Id == parentId, cancellationToken)
                    ?? throw new KeyNotFoundException("Parent not found");

                // Replace with your actual identity verification logic.
                parent.IsIdentityVerified = dto.CIN == parent.CIN;
                parent.VerificationDate = DateTime.UtcNow;

                ////await LogParentAudit(parent, ParentActionType.IdentityVerified, dto.VerifiedBy, cancellationToken);
                return parent.IsIdentityVerified;
            });
        }
        public async Task<ParentDTO> GetParentByUserId(string userId)
        {
            var query = _dbReadOnlyContext.Parents
               .AsNoTracking().FirstOrDefaultAsync(s => s.UserId == userId).Result;
            if (query == null)
            {
                throw new KeyNotFoundException("Parent not found");
            }
            return MapToDto(query);
        }
        // NEW: Retrieves associated students of a parent using the StudentParent join table.
        public async Task<PagedResult<ParsedStudentDto>> GetAssociatedStudentsAsync(Guid parentId, int page, int? size, CancellationToken cancellationToken)
        {
            var pageSize = size ?? PagedResult<StudentDTO>.DefaultPageSize;

            var query = _dbReadOnlyContext.StudentParents
                .AsNoTracking()
                .Where(sp => sp.ParentId == parentId)
                .Include(sp => sp.Student)
                .Select(sp => sp.Student);

            var totalCount = await query.CountAsync(cancellationToken);

            var students = await query
                .OrderBy(s => s.LastNameFr)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var studentIds = students.Select(s => s.Id).ToList();

            var enrolledStudentIds = await _dbReadOnlyContext.Enrollments
                .Where(e => studentIds.Contains(e.StudentId))
                .Select(e => e.StudentId)
                .Distinct()
                .ToListAsync(cancellationToken);

            var studentDtos = students.Select(s => new ParsedStudentDto
            {
                Id = s.Id,
                FirstNameFr = s.FirstNameFr,
                FirstNameAr = s.FirstNameAr,
                LastNameFr = s.LastNameFr,
                LastNameAr = s.LastNameAr,
                DateOfBirth = s.DateOfBirth,
    
                Status = s.Status,
                isEnrolled = enrolledStudentIds.Contains(s.Id)
            }).ToList();

            return new PagedResult<ParsedStudentDto>
            {
                Results = studentDtos,
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }


        #region Private Helpers

        private async Task LogParentAudit(Parent parent, ParentActionType actionType, string performedBy, CancellationToken cancellationToken, string originalValues = null)
        {
            var audit = new ParentAudit
            {
                Id = Guid.NewGuid(),
                ParentId = parent.Id,
                ActionType = actionType,
                ActionDate = DateTime.UtcNow,
                Details = originalValues ?? JsonSerializer.Serialize(parent),
                PerformedBy = performedBy
            };

            await _dbContext.ParentAudits.AddAsync(audit, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private static string CaptureOriginalValues(Parent parent)
        {
            return JsonSerializer.Serialize(new
            {
                parent.FirstNameFr,
                parent.FirstNameAr,
                parent.LastNameFr,
                parent.LastNameAr,
                parent.Email,
                parent.Phone,
                parent.CIN,
                parent.Occupation,
                parent.AddressFr,
                parent.AddressAr
            });
        }

        private static ParentDTO MapToDto(Parent parent) => new ParentDTO
        {
            Id = parent.Id,
            UserId = parent.UserId,
           // BranchId = parent.BranchId,
            FirstNameFr = parent.FirstNameFr,
            FirstNameAr = parent.FirstNameAr,
            LastNameFr = parent.LastNameFr,
            LastNameAr = parent.LastNameAr,
            Email = parent.Email,
            Phone = parent.Phone,
            CIN = parent.CIN,
            Occupation = parent.Occupation,
            AddressFr = parent.AddressFr,
            AddressAr = parent.AddressAr,
            IsIdentityVerified = parent.IsIdentityVerified,
            VerificationDate = parent.VerificationDate
        };

        #endregion
    }
}
