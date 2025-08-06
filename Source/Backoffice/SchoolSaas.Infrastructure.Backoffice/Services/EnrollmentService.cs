using Microsoft.EntityFrameworkCore;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Backoffice.Traceability;
using SchoolSaas.Domain.Common.Enums;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice.School;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly IServiceHelper _serviceHelper;
        private readonly IDocumentService _blobservice;
        public EnrollmentService(
            IBackofficeContext dbContext,
            IBackofficeReadOnlyContext dbReadOnlyContext,
            IServiceHelper serviceHelper,
            IDocumentService blobservice)
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _serviceHelper = serviceHelper;
            _blobservice = blobservice;
        }

        public async Task<bool> SubmitEnrollmentAsync(EnrollmentDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var enrollment = new Enrollment
                {
                    Id = Guid.NewGuid(),
                    StudentId = dto.StudentId,
                    BranchId = dto.BranchId,
                    Status = EnrollmentStatusEnum.Pending,
                    SubmittedAt = DateTime.UtcNow
                };

                await _dbContext.Enrollments.AddAsync(enrollment, cancellationToken);
                ////await LogStatusChange(enrollment, cancellationToken, EnrollmentStatusEnum.None, EnrollmentStatusEnum.Pending);
                return true;
            });
        }
        public async Task<EnrollmentTranscriptDTO> GetEnrollmentTranscriptAsync(Guid enrollmentId, CancellationToken cancellationToken)
        {
            // Retrieve the enrollment details along with related Student and Branch info.
            var enrollment = await _dbReadOnlyContext.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Branch)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == enrollmentId, cancellationToken)
                ?? throw new KeyNotFoundException("Enrollment not found.");

            // Retrieve the complete status history for the enrollment.
            var history = await _dbReadOnlyContext.EnrollmentStatusHistories
                .Where(h => h.EnrollmentId == enrollmentId)
                .OrderBy(h => h.ChangedAt)
                .ToListAsync(cancellationToken);

            // Retrieve associated documents using your existing helper.
            var documents = await GetEnrollmentDocuments(enrollmentId, cancellationToken);

            // Compose and return the transcript DTO.
            return new EnrollmentTranscriptDTO
            {
                EnrollmentId = enrollment.Id,
                StudentId = enrollment.StudentId,
                StudentName = $"{enrollment.Student.FirstNameFr} {enrollment.Student.LastNameFr}",
                BranchName = enrollment.Branch.BranchNameFr,
                Status = enrollment.Status,
                SubmittedAt = enrollment.SubmittedAt,
                // Map history to DTOs – reusing your helper method.
                StatusHistory = history.Select(h => new EnrollmentStatusHistoryDTO
                {
                    EnrollmentId = h.EnrollmentId,
                    OldStatus = h.OldStatus,
                    NewStatus = h.NewStatus,
                    ChangedBy = h.ChangedBy,
                    ChangedAt = h.ChangedAt,
                    ChangeReason = h.ChangeReason
                }).ToList(),
                Documents = documents
            };
        }

        public async Task<bool> UpdateEnrollmentStatusAsync(Guid enrollmentId, EnrollmentStatusUpdateDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var enrollment = await GetEnrollmentEntity(enrollmentId, cancellationToken);
                var oldStatus = enrollment.Status;

                enrollment.Status = dto.NewStatus;
                enrollment.AdminComment = dto.AdminComment;
                enrollment.LastModified = DateTime.UtcNow;

                //await LogStatusChange(enrollment, cancellationToken, oldStatus, dto.NewStatus, dto.ChangeReason);
                return true;
            });
        }

        public async Task<EnrollmentDetailDTO> GetEnrollmentAsync(Guid enrollmentId, CancellationToken cancellationToken)
        {
            var enrollment = await _dbReadOnlyContext.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Branch)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == enrollmentId, cancellationToken)
                ?? throw new KeyNotFoundException("Enrollment not found");

            return new EnrollmentDetailDTO
            {
                StudentName = $"{enrollment.Student.FirstNameFr} {enrollment.Student.LastNameFr}",
                BranchName = enrollment.Branch.BranchNameFr,
                Status = enrollment.Status,
                SubmittedAt = enrollment.SubmittedAt,
                Documents = await GetEnrollmentDocuments(enrollmentId, cancellationToken)
            };
        }

        public async Task<PagedResult<EnrollmentDTO>> GetPaginatedEnrollmentsAsync(EnrollmentFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = BuildEnrollmentQuery(filter);

            var totalCount = await query.CountAsync(cancellationToken);
            var results = await PaginateResults(query, filter)
                .Select(e => MapToDto(e))
                .ToListAsync(cancellationToken);

            return CreatePagedResult(results, totalCount, filter);
        }

        public async Task<bool> BulkUpdateEnrollmentStatusAsync(BulkEnrollmentStatusUpdateDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var enrollments = await _dbContext.Enrollments
                    .Where(e => dto.EnrollmentIds.Contains(e.Id))
                    .ToListAsync(cancellationToken);

                foreach (var enrollment in enrollments)
                {
                    var oldStatus = enrollment.Status;
                    enrollment.Status = dto.NewStatus;
                    //await LogStatusChange(enrollment, cancellationToken, oldStatus, dto.NewStatus, dto.ChangeReason);
                }

                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<bool> MoveEnrollmentToNextStepAsync(Guid enrollmentId, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var enrollment = await GetEnrollmentEntity(enrollmentId, cancellationToken);
                var newStatus = enrollment.Status switch
                {
                    EnrollmentStatusEnum.Pending => EnrollmentStatusEnum.UnderReview,
                    EnrollmentStatusEnum.UnderReview => EnrollmentStatusEnum.Approved,
                    EnrollmentStatusEnum.Approved => EnrollmentStatusEnum.Completed,
                    _ => throw new InvalidOperationException("No next status available")
                };

                await UpdateEnrollmentStatusAsync(enrollmentId, new EnrollmentStatusUpdateDTO
                {
                    NewStatus = newStatus,
                    ChangeReason = EnrollmentChangeReasonEnum.WorkflowTransition
                }, cancellationToken);

                return true;
            });
        }

        public async Task<EnrollmentMetricsDTO> GetEnrollmentDashboardMetricsAsync(DateRangeDTO dateRange, CancellationToken cancellationToken)
        {
            var baseQuery = _dbReadOnlyContext.Enrollments
                .Where(e => e.SubmittedAt >= dateRange.StartDate && e.SubmittedAt <= dateRange.EndDate);

            return new EnrollmentMetricsDTO
            {
                TotalEnrollments = await baseQuery.CountAsync(cancellationToken),
                Pending = await baseQuery.CountAsync(e => e.Status == EnrollmentStatusEnum.Pending, cancellationToken),
                Approved = await baseQuery.CountAsync(e => e.Status == EnrollmentStatusEnum.Approved, cancellationToken),
                Rejected = await baseQuery.CountAsync(e => e.Status == EnrollmentStatusEnum.Rejected, cancellationToken),
                Completed = await baseQuery.CountAsync(e => e.Status == EnrollmentStatusEnum.Completed, cancellationToken)
            };
        }

        public async Task<PagedResult<EnrollmentStatusHistoryDTO>> GetEnrollmentHistoryAsync(EnrollmentHistoryFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = _dbReadOnlyContext.EnrollmentStatusHistories
                .Where(h => h.EnrollmentId == filter.EnrollmentId)
                .OrderByDescending(h => h.ChangedAt);

            var totalCount = await query.CountAsync(cancellationToken);
            var results = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(h => MapHistoryToDto(h))
                .ToListAsync(cancellationToken);

            return new PagedResult<EnrollmentStatusHistoryDTO>
            {
                Results = results,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };
        }

        public async Task<bool> UploadEnrollmentDocumentAsync(EnrollmentDocumentUploadDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                await ValidateEnrollmentExists(dto.EnrollmentId, cancellationToken);

                using var stream = new MemoryStream(dto.FileContent); // assuming byte[] file
                var blobName = await _blobservice.UploadAsync(stream, dto.FileName, dto.ContentType, cancellationToken);

                var document = new EnrollmentDocument
                {
                    Id = Guid.NewGuid(),
                    EnrollmentId = dto.EnrollmentId,
                    FilePath = blobName,
                    DocumentType = dto.DocumentType,
                    UploadedAt = DateTime.UtcNow,
                    VerificationStatus = VerificationStatusEnum.Pending
                };

                await _dbContext.EnrollmentDocuments.AddAsync(document, cancellationToken);
                return true;
            });
        }


        //public async Task<bool> BulkUploadEnrollmentDocumentsAsync(BulkEnrollmentDocumentsDTO dto, CancellationToken cancellationToken)
        //{
        //    return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
        //    {
        //        await ValidateEnrollmentExists(dto.EnrollmentId, cancellationToken);
        //        await ValidateEnrollmentExists(dto.EnrollmentId, cancellationToken);

        //        using var stream = new MemoryStream(dto.DocumentsFileContent); // assuming byte[] file
        //        var blobName = await _blobservice.UploadAsync(stream, dto.FileName, dto.ContentType, cancellationToken);

        //        var documents = dto.Documents.Select(doc => new EnrollmentDocument
        //        {
        //            Id = Guid.NewGuid(),
        //            EnrollmentId = dto.EnrollmentId,
        //            FilePath = doc.FilePath,
        //            DocumentType = doc.DocumentType,
        //            UploadedAt = DateTime.UtcNow,
        //            VerificationStatus = VerificationStatusEnum.Pending
        //        });

        //        await _dbContext.EnrollmentDocuments.AddRangeAsync(documents, cancellationToken);
        //        return true;
        //    });
        //}

        // NEW METHOD: Verify an enrollment document (e.g., mark as Verified or Rejected with comments)
        public async Task<bool> VerifyEnrollmentDocumentAsync(Guid documentId, EnrollmentDocumentVerificationDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var document = await _dbContext.EnrollmentDocuments
                    .FirstOrDefaultAsync(d => d.Id == documentId, cancellationToken)
                    ?? throw new KeyNotFoundException("Enrollment document not found.");

                document.VerificationStatus = dto.VerificationStatus;
                document.VerificationComment = dto.VerificationComment;
                document.LastModified = DateTime.UtcNow;

                _dbContext.EnrollmentDocuments.Update(document);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }
        public async Task<Guid> CreateEnrollmentRequestAsync(CreateEnrollmentRequestDTO dto, CancellationToken ct)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var req = new EnrollmentRequest
                {
                    Id = Guid.NewGuid(),
                    RequestCode = GenerateRequestCode(),
                    StudentId = dto.StudentId,
                    BranchId = dto.BranchId,
                    Status = EnrollmentRequestStatusEnum.Draft,
                    IsDraft = dto.IsDraft,
                    SubmittedAt = DateTime.UtcNow,
                
                };
                await _dbContext.EnrollmentRequests.AddAsync(req, ct);

                // store documents
                foreach (var doc in dto.Documents)
                {
                    var url = await _blobservice.UploadAsync(
                        new MemoryStream(doc.FileContent),
                        doc.FileName,
                        doc.ContentType,
                        ct);

                    await _dbContext.EnrollmentDocuments.AddAsync(new EnrollmentDocument
                    {
                        Id = Guid.NewGuid(),
                        EnrollmentId = req.Id,
                        DocumentType = doc.DocumentType,
                        FilePath = url,
                        VerificationStatus = VerificationStatusEnum.Pending,
                        UploadedAt = DateTime.UtcNow
                    }, ct);
                }

                return req.Id;
            });
        }

        public async Task<PagedResult<EnrollmentRequestDTO>> GetEnrollmentRequestsAsync(EnrollmentRequestFilterDTO filter, CancellationToken ct)
        {
            var q = _dbReadOnlyContext.EnrollmentRequests
                .Include(r => r.Student)
                .Include(r => r.Branch)
                .AsQueryable();

            // Filter by enrollment request status
            if (filter.Status.HasValue)
                q = q.Where(r => r.Status == filter.Status.Value);

            // Filter by branch
            if (filter.BranchId.HasValue)
                q = q.Where(r => r.BranchId == filter.BranchId.Value);

            // === NEW: Filter by ParentId via ParentStudent join ===
            if (filter.ParentId.HasValue)
            {
                var studentIds = await _dbReadOnlyContext.StudentParents
                    .Where(ps => ps.ParentId == filter.ParentId.Value)
                    .Select(ps => ps.StudentId)
                    .ToListAsync(ct);

                q = q.Where(r => studentIds.Contains(r.StudentId));
            }

            var total = await q.CountAsync(ct);
            var items = await q
                .OrderByDescending(r => r.SubmittedAt)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(r => new EnrollmentRequestDTO
                {
                    Id = r.Id,
                    RequestCode = r.RequestCode,
                    SchoolId = r.BranchId.Value,
                    SchoolName = r.Branch!.BranchNameFr,
                    RequestedByUserId = r.Student.UserId,
                    RequestedByName = $"{r.Student.FirstNameFr} {r.Student.LastNameFr}",
                    Status = r.Status,
                    CreatedAt = r.Created,
                    SubmittedAt = r.IsDraft ? (DateTime?)null : r.SubmittedAt
                })
                .ToListAsync(ct);

            return new PagedResult<EnrollmentRequestDTO>
            {
                Results = items,
                RowCount = total,
                CurrentPage = filter.Page,
                PageSize = filter.PageSize,
                PageCount = (int)Math.Ceiling(total / (double)filter.PageSize)
            };
        }


        public async Task<EnrollmentRequestDetailDTO> GetEnrollmentRequestByIdAsync(Guid requestId, CancellationToken ct)
        {
            var r = await _dbReadOnlyContext.EnrollmentRequests
                .Include(r => r.Student)
                .Include(r => r.Branch)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == requestId, ct)
                ?? throw new KeyNotFoundException("Request not found");

            var docs = await _dbReadOnlyContext.EnrollmentDocuments
                .Where(d => d.EnrollmentId == requestId)
                .Select(d => new DocumentDTO
                {
                    DocumentType = d.DocumentType,
                    FilePath = d.FilePath,
                    UploadedAt = d.UploadedAt,
                    VerificationStatus = d.VerificationStatus
                })
                .ToListAsync(ct);

            return new EnrollmentRequestDetailDTO
            {
                Id = r.Id,
                RequestCode = r.RequestCode,
                SchoolId = r.BranchId!.Value,
                SchoolName = r.Branch.BranchNameFr,
                Student = MapStudentDto(r.Student),
                Status = r.Status,
                CreatedAt = r.Created,
                SubmittedAt = r.IsDraft ? (DateTime?)null : r.SubmittedAt,
                Documents = docs
            };
        }

        public async Task<bool> UpdateEnrollmentRequestAsync(Guid requestId, UpdateEnrollmentRequestDTO dto, CancellationToken ct)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var r = await _dbContext.EnrollmentRequests.FindAsync(new object[] { requestId }, ct)
                    ?? throw new KeyNotFoundException("Request not found");

                if (!r.IsDraft) throw new InvalidOperationException("Cannot modify submitted request");
                r.BranchId = dto.BranchId;
                r.LastModified = DateTime.UtcNow;
                // … any other updatable fields …

                return true;
            });
        }

        public async Task<bool> SubmitEnrollmentRequestAsync(Guid requestId, CancellationToken ct)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var r = await _dbContext.EnrollmentRequests.FindAsync(new object[] { requestId }, ct)
                    ?? throw new KeyNotFoundException("Request not found");

                r.IsDraft = false;
                r.Status = EnrollmentRequestStatusEnum.Submitted;
                r.SubmittedAt = DateTime.UtcNow;
                return true;
            });
        }

        public async Task<bool> ApproveEnrollmentRequestAsync(Guid requestId, Guid adminUserId, CancellationToken ct)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var r = await _dbContext.EnrollmentRequests
                    .Include(r => r.Student)
                    .FirstOrDefaultAsync(r => r.Id == requestId, ct)
                    ?? throw new KeyNotFoundException("Request not found");

               

                // 1) Create actual Enrollment
                var enr = new Enrollment
                {
                    Id = Guid.NewGuid(),
                    StudentId = r.StudentId,
                    BranchId = r.BranchId!.Value,
                    Status = EnrollmentStatusEnum.Approved,
                    SubmittedAt = DateTime.UtcNow,
                    AdminComment = $"Approved from request {r.RequestCode}"
                };
                await _dbContext.Enrollments.AddAsync(enr, ct);

                // 2) Transition request
                r.Status = EnrollmentRequestStatusEnum.Approved;
               
                r.LastModified = DateTime.UtcNow;

                return true;
            });
        }

        public async Task<bool> BulkApproveEnrollmentRequestsAsync(IEnumerable<Guid> requestIds, Guid adminUserId, CancellationToken ct)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var reqs = await _dbContext.EnrollmentRequests
                    .Where(r => requestIds.Contains(r.Id))
                    .ToListAsync(ct);

                foreach (var r in reqs)
                {
                    if (r.Status != EnrollmentRequestStatusEnum.Submitted)
                        continue;

                    // create enrollment
                    var enr = new Enrollment
                    {
                        Id = Guid.NewGuid(),
                        StudentId = r.StudentId,
                        BranchId = r.BranchId!.Value,
                        Status = EnrollmentStatusEnum.Approved,
                        SubmittedAt = DateTime.UtcNow
                    };
                    await _dbContext.Enrollments.AddAsync(enr, ct);

                    // update request
                    r.Status = EnrollmentRequestStatusEnum.Approved;
                
                    r.LastModified = DateTime.UtcNow;
                }

                return true;
            });
        }

        // … keep your old Enrollment-only methods (GetEnrollment, GetPaginatedEnrollments, etc.) …


        public async Task<bool> SaveRequest(EnrollmentRequestDTO dto, CancellationToken ct)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                EnrollmentRequest request;

                if (dto.Id != null && dto.Id != Guid.Empty)
                {
                    // Update existing draft
                    request = await _dbContext.EnrollmentRequests
                        .FirstOrDefaultAsync(r => r.Id == dto.Id!=null && r.IsDraft);

                    if (request == null)
                        throw new Exception("Draft enrollment request not found.");

                    request.StudentId = dto.StudentId;
                    request.BranchId = dto.BranchId;
                    request.Status = dto.Status;
                    

                    // keep IsDraft true (user saved but did not submit)
                    request.IsDraft = true;

                    _dbContext.EnrollmentRequests.Update(request);
                }
                else
                {
                    // Create new draft
                    request = new EnrollmentRequest
                    {
                        Id = Guid.NewGuid(),
                        RequestCode = EnrollmentRequest.GenerateRequestCode(),
                        StudentId = dto.StudentId,
                        BranchId = dto.BranchId,
                        Status = dto.Status,
                        IsDraft = true,
                        SubmittedAt = DateTime.MinValue,
                       
                    };

                    await _dbContext.EnrollmentRequests.AddAsync(request);
                }

                await _dbContext.SaveChangesAsync(ct);
                return true;
            });
        }

        #region Private Helpers

        private async Task<Enrollment> GetEnrollmentEntity(Guid enrollmentId, CancellationToken ct)
        {
            return await _dbContext.Enrollments
                       .FirstOrDefaultAsync(e => e.Id == enrollmentId, ct)
                   ?? throw new KeyNotFoundException("Enrollment not found");
        }

        private async Task LogStatusChange(Enrollment enrollment, CancellationToken cancellationToken,
            EnrollmentStatusEnum oldStatus,
            EnrollmentStatusEnum newStatus,
            EnrollmentChangeReasonEnum reason = EnrollmentChangeReasonEnum.StatusUpdate)
        {
            var history = new EnrollmentStatusHistory
            {
                Id = Guid.NewGuid(),
                EnrollmentId = enrollment.Id,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                ChangedBy = enrollment.LastModifiedBy ?? "system",
                ChangedAt = DateTime.UtcNow,
                ChangeReason = reason
            };

            await _dbContext.EnrollmentStatusHistories.AddAsync(history, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        // ─── Helper to generate codes ────────────────────────────
        private static string GenerateRequestCode()
        {
            // e.g. "ER-2025-06-0001"
            var datePart = DateTime.UtcNow.ToString("yyyyMM");
            var seq = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
            return $"ER-{datePart}-{seq}";
        }

        private static StudentDTO MapStudentDto(Student s)
            => new StudentDTO
            {
                Id = s.Id,
                FirstNameFr = s.FirstNameFr,
                LastNameFr = s.LastNameFr,
                DateOfBirth = s.DateOfBirth,
                // …
            };
        private IQueryable<Enrollment> BuildEnrollmentQuery(EnrollmentFilterDTO filter)
        {
            // Start from the request set, include student & branch nav-props
            var query = _dbReadOnlyContext.Enrollments
                .Include(r => r.Student)
                .Include(r => r.Branch)
                .AsQueryable();

            // Filter by request status (Draft, Submitted, Approved, Rejected…)
            if (filter.Status.HasValue)
            {
                query = query.Where(r => r.Status == filter.Status.Value);
            }

            // Filter by branch
            if (filter.BranchId.HasValue)
            {
                query = query.Where(r => r.BranchId == filter.BranchId.Value);
            }

            // Text search against student’s name
            if (!string.IsNullOrWhiteSpace(filter.searchTerm))
            {
                var term = filter.searchTerm.Trim();
                query = query.Where(r =>
                    r.Student.FirstNameFr.Contains(term) ||
                    r.Student.LastNameFr.Contains(term) ||
                    r.Student.FirstNameAr.Contains(term) ||
                    r.Student.LastNameAr.Contains(term)
                );
            }

            // Order newest first by creation/submission
            return query
                .OrderByDescending(r => r.Created);
        }


        private async Task ValidateEnrollmentExists(Guid enrollmentId, CancellationToken ct)
        {
            if (!await _dbReadOnlyContext.Enrollments.AnyAsync(e => e.Id == enrollmentId, ct))
                throw new KeyNotFoundException("Enrollment not found");
        }

        private async Task<List<DocumentDTO>> GetEnrollmentDocuments(Guid enrollmentId, CancellationToken ct)
        {
            return await _dbReadOnlyContext.EnrollmentDocuments
                .Where(d => d.EnrollmentId == enrollmentId)
                .Select(d => new DocumentDTO
                {
                    DocumentType = d.DocumentType,
                    FilePath = d.FilePath,
                    UploadedAt = d.UploadedAt,
                    VerificationStatus = d.VerificationStatus // NEW: include verification status if needed
                })
                .ToListAsync(ct);
        }

        private static EnrollmentDTO MapToDto(Enrollment enrollment) => new()
        {
            StudentId = enrollment.StudentId,
            BranchId = enrollment.BranchId,
            Status = enrollment.Status,
            SubmittedAt = enrollment.SubmittedAt
        };

        private static EnrollmentStatusHistoryDTO MapHistoryToDto(EnrollmentStatusHistory history) => new()
        {
            EnrollmentId = history.EnrollmentId,
            OldStatus = history.OldStatus,
            NewStatus = history.NewStatus,
            ChangedBy = history.ChangedBy,
            ChangedAt = history.ChangedAt,
            ChangeReason = history.ChangeReason
        };

        private static PagedResult<EnrollmentDTO> CreatePagedResult(
            List<EnrollmentDTO> items,
            int totalCount,
            EnrollmentFilterDTO filter) => new()
            {
                Results = items,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };

        private static IQueryable<Enrollment> PaginateResults(
            IQueryable<Enrollment> query,
            EnrollmentFilterDTO filter) => query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize);

        #endregion
    }
}
