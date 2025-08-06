using Microsoft.EntityFrameworkCore;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice.School;
using SchoolSaas.Domain.Backoffice.Traceability;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Transfer;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class TransferService : ITransferService
    {
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly IServiceHelper _serviceHelper;

        public TransferService(
            IBackofficeContext dbContext,
            IBackofficeReadOnlyContext dbReadOnlyContext,
            IServiceHelper serviceHelper)
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _serviceHelper = serviceHelper;
        }

        public async Task<bool> SubmitTransferRequestAsync(TransferRequestDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var request = new TransferRequest
                {
                    StudentId = dto.StudentId,
                    FromBranchId = dto.FromBranchId,
                    ToBranchId = dto.ToBranchId,
                    FromSchoolId = dto.FromSchoolId,
                    ToSchoolId = dto.ToSchoolId,
                    SubmittedAt = DateTime.UtcNow,
                    Reason = dto.Reason,
                    Status = TransferRequestStatus.Pending,
                    Documents = dto.Documents.Select(d => new TransferDocument
                    {
                        DocumentType = d.DocumentType,
                        FilePath = d.FilePath
                    }).ToList()
                };

                await ValidateTransferEligibility(dto.StudentId, cancellationToken);

                await _dbContext.TransferRequests.AddAsync(request, cancellationToken);
                //await LogTransferHistory(request, cancellationToken, TransferRequestStatus.Pending, "Initial submission",
                //    dto.StudentId.ToString(), TransferRequestChangeReasonEnum.NewRequest);

                return true;
            });
        }
        public async Task<bool> UpdateTransferRequestAsync(Guid transferRequestId, TransferRequestUpdateDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var request = await _dbContext.TransferRequests
                    .Include(t => t.Documents)
                    .FirstOrDefaultAsync(t => t.Id == transferRequestId, cancellationToken)
                    ?? throw new KeyNotFoundException("Transfer request not found");

                // Only allow updates if the status is still pending
                if (request.Status != TransferRequestStatus.Pending)
                    throw new InvalidOperationException("Cannot update a transfer request that is already processed.");

                // Update editable fields
                request.ToBranchId = dto.ToBranchId ?? request.ToBranchId;
                request.ToSchoolId = dto.ToSchoolId ?? request.ToSchoolId;
                request.Reason = dto.Reason ?? request.Reason;
                request.LastModified = DateTime.UtcNow;

                // Optionally update documents
                if (dto.Documents != null && dto.Documents.Any())
                {
                    // Clear old ones (depends on business logic — this can be adjusted)
                    _dbContext.TransferDocuments.RemoveRange(request.Documents);

                    request.Documents = dto.Documents.Select(d => new TransferDocument
                    {
                        DocumentType = d.DocumentType,
                        FilePath = d.FilePath,
                        UploadedAt = DateTime.UtcNow
                    }).ToList();
                }

                //await LogTransferHistory(request, cancellationToken, request.Status, "Request updated", dto.UpdatedBy, TransferRequestChangeReasonEnum.Edited);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<bool> UpdateTransferStatusAsync(Guid transferRequestId, TransferStatusUpdateDTO dto,
            CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var request = await GetTransferRequestEntity(transferRequestId, cancellationToken);
                var oldStatus = request.Status;

                request.Status = dto.NewStatus;
                request.AdminComment = dto.AdminComment;
                request.LastModified = DateTime.UtcNow;
                if (request.Status == TransferRequestStatus.Approved || request.Status == TransferRequestStatus.Completed)
                {
                    await _dbContext.Enrollments.AddAsync(new Enrollment
                    {
                        Id = request.Id,
                        RequestCode = Enrollment.GenerateRequestCode(),
                        StudentId = request.StudentId,
                        BranchId = request.ToBranchId,
                        Status = EnrollmentStatusEnum.Approved,
                        SubmittedAt = DateTime.UtcNow,
                        Student = request.Student,
                        Branch = await _dbReadOnlyContext.Branches.Where(b => b.Id == request.ToBranchId).FirstOrDefaultAsync(cancellationToken)
                    });
                    _dbContext.Enrollments.Remove(await _dbReadOnlyContext.Enrollments.Where(e => e.BranchId == request.FromBranchId && e.StudentId == request.StudentId).FirstAsync());
                }
                //await LogTransferHistory(request,cancellationToken, oldStatus, dto.Reason, dto.ChangedBy, dto.ChangeReason);

                return true;
            });
        }

        public async Task<PagedResult<TransferRequestDTO>> GetPaginatedTransferRequestsAsync(
            TransferRequestFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = BuildTransferQuery(filter);

            var totalCount = await query.CountAsync(cancellationToken);

            var results = await query
                .OrderByDescending(t => t.SubmittedAt)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(t => MapToDto(t))
                .ToListAsync(cancellationToken);

            return new PagedResult<TransferRequestDTO>
            {
                Results = results,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling((double)totalCount / filter.PageSize)
            };
        }

        public async Task<bool> BulkUpdateTransferStatusAsync(BulkTransferUpdateDTO dto,
            CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var requests = await _dbContext.TransferRequests
                    .Where(t => dto.TransferIds.Contains(t.Id))
                    .ToListAsync(cancellationToken);

                foreach (var request in requests)
                {
                    var oldStatus = request.Status;
                    request.Status = dto.NewStatus;
                    request.AdminComment = dto.AdminComment;
                    //await LogTransferHistory(request,cancellationToken, oldStatus, dto.Reason, dto.ChangedBy, dto.ChangeReason);
                }

                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<TransferEligibilityResultDTO> CheckTransferEligibilityAsync(Guid studentId,
            CancellationToken cancellationToken)
        {
            var hasPendingFees = await _dbReadOnlyContext.StudentFinances
                .AnyAsync(f => f.StudentId == studentId && f.Status == PaymentStatusEnum.Overdue, cancellationToken);

            var hasDisciplinaryIssues = await _dbReadOnlyContext.DisciplinaryRecords
                .AnyAsync(d => d.StudentId == studentId && d.Resolved == false, cancellationToken);

            return new TransferEligibilityResultDTO
            {
                IsEligible = !hasPendingFees && !hasDisciplinaryIssues,
                PendingFees = hasPendingFees,
                DisciplinaryIssues = hasDisciplinaryIssues
            };
        }

        public async Task<PagedResult<TransferRequestHistoryDTO>> GetTransferHistoryAsync(
            TransferHistoryFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = _dbReadOnlyContext.TransferRequestHistories
                .Where(h => h.TransferRequestId == filter.TransferRequestId)
                .OrderByDescending(h => h.ChangedAt);

            var totalCount = await query.CountAsync(cancellationToken);

            var results = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(h => new TransferRequestHistoryDTO
                {
                    TransferRequestId = h.TransferRequestId,
                    OldStatus = h.OldStatus,
                    NewStatus = h.NewStatus,
                    ChangedBy = h.ChangedBy,
                    ChangedAt = h.ChangedAt,
                    ChangeReason = h.ChangeReason,
                    Reason = h.Reason
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<TransferRequestHistoryDTO>
            {
                Results = results,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling((double)totalCount / filter.PageSize)
            };
        }
        public async Task<bool> CancelTransferRequestAsync(Guid requestId, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var request = await GetTransferRequestEntity(requestId, cancellationToken);

                // Only allow cancellation if status is still pending
                if (request.Status != TransferRequestStatus.Pending)
                    throw new InvalidOperationException("Only pending transfer requests can be cancelled.");

                var oldStatus = request.Status;
                request.Status = TransferRequestStatus.Canceled;  // Make sure your enum includes Canceled
                request.LastModified = DateTime.UtcNow;

                //////await LogTransferHistory(request, cancellationToken,
                //    oldStatus,
                //    "Request cancelled",
                //    "system",  // or use the current user context, if available
                //    TransferRequestChangeReasonEnum.Canceled);

                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<bool> ReassignTransferRequestAsync(Guid requestId, Guid newTargetBranchId, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var request = await GetTransferRequestEntity(requestId, cancellationToken);

                // Allow reassignment only if the request is still pending
                if (request.Status != TransferRequestStatus.Pending)
                    throw new InvalidOperationException("Only pending transfer requests can be reassigned.");

                var oldTargetBranchId = request.ToBranchId;
                request.ToBranchId = newTargetBranchId;
                request.LastModified = DateTime.UtcNow;

                ////await LogTransferHistory(request, cancellationToken,
                //    request.Status,
                //    $"Reassigned: from branch {oldTargetBranchId} to {newTargetBranchId}",
                //    "system",
                //    TransferRequestChangeReasonEnum.Reassignment);

                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<TransferRequestDetailDTO> GetTransferRequestDetailsAsync(Guid requestId, CancellationToken cancellationToken)
        {
            // Retrieve the transfer request including its documents
            var request = await _dbReadOnlyContext.TransferRequests
                .Include(t => t.Documents)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == requestId, cancellationToken)
                ?? throw new KeyNotFoundException("Transfer request not found.");

            // Retrieve history for this request
            var history = await _dbReadOnlyContext.TransferRequestHistories
                .Where(h => h.TransferRequestId == requestId)
                .OrderByDescending(h => h.ChangedAt)
                .Select(h => new TransferRequestHistoryDTO
                {
                    TransferRequestId = h.TransferRequestId,
                    OldStatus = h.OldStatus,
                    NewStatus = h.NewStatus,
                    Reason = h.Reason,
                    ChangedBy = h.ChangedBy,
                    ChangedAt = h.ChangedAt,
                    ChangeReason = h.ChangeReason
                })
                .ToListAsync(cancellationToken);

            // Map the request to a detailed DTO
            var detailDto = new TransferRequestDetailDTO
            {
                StudentId = request.StudentId,
                FromBranchId = request.FromBranchId,
                ToBranchId = request.ToBranchId,
                FromSchoolId = request.FromSchoolId,
                ToSchoolId = request.ToSchoolId,
                Reason = request.Reason,
                Status = request.Status,
                SubmittedAt = request.SubmittedAt,
                Documents = request.Documents.Select(d => new DocumentDTO
                {
                    DocumentType = d.DocumentType,
                    FilePath = d.FilePath,
                    UploadedAt = d.UploadedAt
                }).ToList(),
                History = history
            };

            return detailDto;
        }


        #region Private Helpers

        private async Task<TransferRequest> GetTransferRequestEntity(Guid transferRequestId,
            CancellationToken ct)
        {
            return await _dbContext.TransferRequests
                .Include(t => t.Documents)
                .FirstOrDefaultAsync(t => t.Id == transferRequestId, ct)
                ?? throw new KeyNotFoundException("Transfer request not found");
        }

        private async Task ValidateTransferEligibility(Guid studentId, CancellationToken ct)
        {
            var eligibility = await CheckTransferEligibilityAsync(studentId, ct);
            if (!eligibility.IsEligible)
            {
                throw new InvalidOperationException(
                    "Student is not eligible for transfer due to pending issues");
            }
        }

        private async Task LogTransferHistory(TransferRequest request, CancellationToken cancellationToken, TransferRequestStatus oldStatus,
            string reason, string changedBy, TransferRequestChangeReasonEnum changeReason)
        {
            var history = new TransferRequestHistory
            {
                TransferRequestId = request.Id,
                OldStatus = oldStatus,
                NewStatus = request.Status,
                Reason = reason,
                ChangedBy = changedBy,
                ChangedAt = DateTime.UtcNow,
                ChangeReason = changeReason
            };

            await _dbContext.TransferRequestHistories.AddAsync(history);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private IQueryable<TransferRequest> BuildTransferQuery(TransferRequestFilterDTO filter)
        {
            var query = _dbReadOnlyContext.TransferRequests
                .Include(t => t.Student)
                .Include(t => t.FromBranchId)
                .Include(t => t.ToBranchId)
                .AsQueryable();

            if (filter.StudentId.HasValue)
                query = query.Where(t => t.StudentId == filter.StudentId.Value);

            if (filter.FromSchoolId.HasValue)
                query = query.Where(t => t.FromSchoolId == filter.FromSchoolId.Value);

            if (filter.Status.HasValue)
                query = query.Where(t => t.Status == filter.Status.Value);

            return query;
        }

        private static TransferRequestDTO MapToDto(TransferRequest entity) => new()
        {
    
            StudentId = entity.StudentId,
            FromBranchId = entity.FromBranchId,
            ToBranchId = entity.ToBranchId,
            FromSchoolId = entity.FromSchoolId,
            ToSchoolId = entity.ToSchoolId,
            Reason = entity.Reason,
            Status = entity.Status,
            Documents = entity.Documents.Select(d => new DocumentDTO
            {
                UploadedAt = d.UploadedAt,
                DocumentType = d.DocumentType,
                FilePath = d.FilePath
            }).ToList(),
            SubmittedAt = entity.SubmittedAt
        };

        #endregion
    }
}