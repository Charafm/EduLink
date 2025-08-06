using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Backoffice.Traceability;
using SchoolSaas.Domain.Common.DataObjects.Parent;
using SchoolSaas.Domain.Common.DataObjects.Student;
using SchoolSaas.Domain.Common.Enums;
using System.Text;
using System.Text.Json;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class StudentService : IStudentService
    {
        #region Defining Needed services 
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly IServiceHelper _serviceHelper;
        private readonly IHttpClientFactory _httpClientFactory;
        #endregion
        #region Constructor
        public StudentService(IBackofficeContext dbContext, IBackofficeReadOnlyContext dbReadOnlyContext, 
            IServiceHelper serviceHelper, IHttpClientFactory httpClientFactory)
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _serviceHelper = serviceHelper;
            _httpClientFactory = httpClientFactory;
        }
        #endregion
     
        public class IdentityUserResponse
        {
            public Guid Id { get; set; }
            public string Email { get; set; }
            // Add other fields if needed
        }   
        public async Task<CreateStudentUserDTO> CreateStudentAsync(CreateStudentDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var httpClient = _httpClientFactory.CreateClient("EdulinkIdentityFrontal");
                var identityUrl = $"{httpClient.BaseAddress}identity/createstudentuser";

                var password = $"Edu{Guid.NewGuid().ToString("N").Substring(0, 8)}*";
                var userDto = new CreateStudentUserDTO
                {
                    FirstNameFr = dto.FirstNameFr,
                    LastNameFr = dto.LastNameFr,
                    FirstNameAr = dto.FirstNameAr,
                    LastNameAr = dto.LastNameAr,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    AddressFr = null,
                    Password = password
                };

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(userDto),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await httpClient.PostAsync(identityUrl, jsonContent, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Identity creation failed: {error}");
                }
                var json = await response.Content.ReadAsStringAsync(cancellationToken);
                var identityUser = JsonSerializer.Deserialize<IdentityUserResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var student = MapToEntity(dto);
                student.UserId = identityUser?.Id.ToString() ?? throw new Exception("Missing user ID from identity response");
                var studentInfo = new StudentDetail
                {
                    StudentId = student.Id,
                    Email = dto.Email,
                    EmergencyContact = dto.EmergencyContact,
                    Phone = dto.Phone,

                };
                await _dbContext.StudentParents.AddAsync(new StudentParent
                {
                    StudentId = student.Id,
                    ParentId = dto.ParentId
                }, cancellationToken);  
                await _dbContext.Students.AddAsync(student, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return userDto;
            });
        }


        public async Task<bool> UpdateStudentAsync(Guid studentId, StudentDTO dto, 
            CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var student = await GetStudentEntity(studentId, cancellationToken);
                var originalValues = CaptureOriginalValues(student);

                student.FirstNameFr = dto.FirstNameFr;
                student.FirstNameAr = dto.FirstNameAr;
                student.LastNameFr = dto.LastNameFr;
                student.LastNameAr = dto.LastNameAr;
                student.DateOfBirth = dto.DateOfBirth;
                student.Gender = dto.Gender;
         
                student.Status = dto.Status;
                student.LastModified = DateTime.UtcNow;

                //await LogStudentHistory(student, cancellationToken, originalValues);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<PagedResult<StudentDTO>> GetPaginatedStudentsAsync(StudentFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = BuildStudentQuery(filter);

            var totalCount = await query.CountAsync(cancellationToken);
            var results = await PaginateResults(query, filter)
                .Select(s => MapToDto(s))
                .ToListAsync(cancellationToken);

            return CreatePagedResult(results, totalCount, filter);
        }
        public async Task<StudentDTO> GetStudentByUserId(string userId)
        {
            var query = _dbReadOnlyContext.Students
               .AsNoTracking().FirstOrDefaultAsync(s => s.UserId == userId).Result;
            if (query == null)
            {
                throw new KeyNotFoundException("Student not found");
            }
            return MapToDto(query);
        }
        public async Task<StudentDetailDTO> GetStudentWithDetailsAsync(Guid studentId, CancellationToken cancellationToken)
        {
            var student = await _dbReadOnlyContext.Students
                .Include(s => s.Detail)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == studentId, cancellationToken)
                ?? throw new KeyNotFoundException("Student not found");

            return new StudentDetailDTO
            {
                Student = MapToDto(student),
                MedicalInfo = student.Detail?.MedicalInfo,
                EmergencyContact = student.Detail?.EmergencyContact,
                PreviousSchool = student.Detail?.PreviousSchool,
                AdditionalNotes = student.Detail?.AdditionalNotes
            };
        }
        public async Task<StudentTranscriptDTO> GetStudentTranscriptAsync(Guid studentId, CancellationToken cancellationToken)
        {
            var student = await _dbReadOnlyContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == studentId, cancellationToken)
                ?? throw new KeyNotFoundException("Student not found.");

            // Query the transcript records table directly
            var records = await _dbReadOnlyContext.TranscriptRecords
                .AsNoTracking()
                .Where(tr => tr.StudentId == studentId)
                .Include(tr => tr.Course)
                .ToListAsync(cancellationToken);

            var dto = new StudentTranscriptDTO
            {
                StudentId = student.Id,
                FullName = $"{student.FirstNameFr} {student.LastNameFr}",
                TranscriptRecords = records.Select(tr => new TranscriptRecordDTO
                {
                    CourseId = tr.CourseId,
                    CourseName = tr.Course.TitleFr,
                    Grade = tr.Grade,
                    Term = tr.Term,
                    AcademicYear = tr.AcademicYear
                }).ToList()
            };

            return dto;
        }

        public async Task<bool> UpdateStudentParentsAsync(Guid studentId, List<ParentLinkDTO> parentInfo, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                // Ensure the student exists.
                var studentExists = await _dbContext.Students.AnyAsync(s => s.Id == studentId, cancellationToken);
                if (!studentExists)
                {
                    throw new KeyNotFoundException("Student not found.");
                }

                // Retrieve existing parent links for the student directly from the StudentParents table.
                var existingLinks = await _dbContext.StudentParents
                    .Where(sp => sp.StudentId == studentId)
                    .ToListAsync(cancellationToken);

                // Remove all existing parent associations.
                _dbContext.StudentParents.RemoveRange(existingLinks);

                // Add new parent associations from the provided list.
                foreach (var parentLink in parentInfo)
                {
                    var newLink = new StudentParent
                    {
                        StudentId = studentId,
                        ParentId = parentLink.ParentId,
                        RelationshipType = parentLink.RelationshipType
                    };

                    await _dbContext.StudentParents.AddAsync(newLink, cancellationToken);
                }

                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        


        public async Task<bool> TransitionStudentStatusAsync(Guid studentId, StudentStatusTransitionDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var student = await GetStudentEntity(studentId, cancellationToken);
                ValidateStatusTransition(student.Status, dto.NewStatus);

                var originalStatus = student.Status;
                student.Status = dto.NewStatus;

                //await LogStatusChange(student, cancellationToken, originalStatus, dto.Reason);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }
        public async Task<bool> BulkUpdateStudentsAsync(BulkStudentUpdateDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var students = await _dbContext.Students
                    .Where(s => dto.StudentIds.Contains(s.Id))
                    .ToListAsync(cancellationToken);

                foreach (var student in students)
                {
                    UpdateStudentEntity(student, dto.UpdateData);
                }

                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<bool> UpdateStudentDetailsAsync(Guid studentId, StudentDetailDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var student = await GetStudentEntity(studentId, cancellationToken);
                student.Detail ??= new StudentDetail { StudentId = studentId };

                student.Detail.MedicalInfo = dto.MedicalInfo;
                student.Detail.EmergencyContact = dto.EmergencyContact;
                student.Detail.PreviousSchool = dto.PreviousSchool;
                student.Detail.AdditionalNotes = dto.AdditionalNotes;

                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<PagedResult<StudentParentDTO>> GetStudentParentsAsync(Guid studentId, CancellationToken cancellationToken)
        {
            var query = _dbReadOnlyContext.StudentParents
                .Where(sp => sp.StudentId == studentId)
                .Include(sp => sp.Parent);

            var totalCount = await query.CountAsync(cancellationToken);
            var results = await query
                .Select(sp => new StudentParentDTO
                {
                    ParentId = sp.ParentId,
                    FullName = $"{sp.Parent.FirstNameFr} {sp.Parent.LastNameFr}",
                    Relationship = sp.RelationshipType
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<StudentParentDTO>
            {
                Results = results,
                CurrentPage = 1,
                PageSize = totalCount,
                RowCount = totalCount,
                PageCount = 1
            };
        }

        public async Task<PagedResult<StudentHistoryDTO>> GetStudentHistoryAsync(StudentHistoryFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = _dbReadOnlyContext.StudentHistories
                .Where(h => h.StudentId == filter.StudentId)
                .OrderByDescending(h => h.ChangedAt);

            var totalCount = await query.CountAsync(cancellationToken);
            var results = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(h => new StudentHistoryDTO
                {
                    StudentId = h.StudentId,
                    OldStatus = h.OldStatus,
                    NewStatus = h.NewStatus,
                    ChangedBy = h.ChangedBy,
                    ChangedAt = h.ChangedAt,
                    FieldChanges = h.FieldChanges
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<StudentHistoryDTO>
            {
                Results = results,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };
        }

        #region Private Helpers

        private IQueryable<Student> BuildStudentQuery(StudentFilterDTO filter)
        {
            // Start from Students
            var students = _dbReadOnlyContext.Students
                // bring in both collections so EF can resolve them
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Branch)
                .Include(s => s.EnrollmentRequests)
                    .ThenInclude(r => r.Branch)
                .AsQueryable();

            // 1) Branch filter: match if student has either
            //    a) a non-draft EnrollmentRequest in that branch, OR
            //    b) a finalized Enrollment in that branch
            if (filter.BranchId.HasValue)
            {
                var branchId = filter.BranchId.Value;
                students = students.Where(s =>
                    // any non-draft request targeting that branch
                    s.EnrollmentRequests.Any(r => !r.IsDraft && r.BranchId == branchId)
                    ||
                    // any finalized enrollment in that branch
                    s.Enrollments.Any(e => e.BranchId == branchId)
                );
            }

            // 2) Status filter: assume this maps to the student.Status string
            if (!string.IsNullOrEmpty(filter.Status))
            {
                students = students.Where(s => s.Status == filter.Status);
            }

            // 3) Enrollment date filtering: we’ll pick the most recent SubmittedAt
            //    across both enrollments and non-draft requests
            if (filter.EnrollmentStart.HasValue)
            {
                var from = filter.EnrollmentStart.Value;
                students = students.Where(s =>
                    // max of all SubmittedAt timestamps
                    (new[] {
                s.Enrollments.Max(e => (DateTime?)e.SubmittedAt)   ?? DateTime.MinValue,
                s.EnrollmentRequests.Where(r => !r.IsDraft)
                                     .Max(r => (DateTime?)r.SubmittedAt) ?? DateTime.MinValue
                    }).Max() >= from
                );
            }
            if (filter.EnrollmentEnd.HasValue)
            {
                var to = filter.EnrollmentEnd.Value;
                students = students.Where(s =>
                    (new[] {
                s.Enrollments.Max(e => (DateTime?)e.SubmittedAt)   ?? DateTime.MinValue,
                s.EnrollmentRequests.Where(r => !r.IsDraft)
                                     .Max(r => (DateTime?)r.SubmittedAt) ?? DateTime.MinValue
                    }).Max() <= to
                );
            }

            // 4) Text search on names
            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                var term = filter.SearchTerm;
                students = students.Where(s =>
                    s.FirstNameFr.Contains(term) ||
                    s.LastNameFr.Contains(term) ||
                    (s.FirstNameAr ?? "").Contains(term) ||
                    (s.LastNameAr ?? "").Contains(term)
                );
            }

            // Final ordering: by the same “most recent” timestamp
            return students
                .OrderByDescending(s =>
                    (new[] {
                s.Enrollments.Max(e => (DateTime?)e.SubmittedAt)   ?? DateTime.MinValue,
                s.EnrollmentRequests.Where(r => !r.IsDraft)
                                     .Max(r => (DateTime?)r.SubmittedAt) ?? DateTime.MinValue
                    }).Max()
                );
        }


        private async Task<Student> GetStudentEntity(Guid studentId, CancellationToken ct)
        {
            return await _dbContext.Students
                .Include(s => s.Detail)
                .FirstOrDefaultAsync(s => s.Id == studentId, ct)
                ?? throw new KeyNotFoundException("Student not found");
        }

        private static Student CaptureOriginalValues(Student student)
        {
            return new Student
            {
                FirstNameFr = student.FirstNameFr,
                FirstNameAr = student.FirstNameAr,
                LastNameFr = student.LastNameFr,
                LastNameAr = student.LastNameAr,
                Status = student.Status
            };
        }

        private static void UpdateStudentEntity(Student entity, StudentUpdateDTO dto)
        {
            if (!string.IsNullOrEmpty(dto.FirstNameFr))
                entity.FirstNameFr = dto.FirstNameFr;

            if (!string.IsNullOrEmpty(dto.FirstNameAr))
                entity.FirstNameAr = dto.FirstNameAr;

            if (!string.IsNullOrEmpty(dto.LastNameFr))
                entity.LastNameFr = dto.LastNameFr;

            if (!string.IsNullOrEmpty(dto.LastNameAr))
                entity.LastNameAr = dto.LastNameAr;

            if (!string.IsNullOrEmpty(dto.Status))
                entity.Status = dto.Status;
        }

        private async Task LogStatusChange(Student student, CancellationToken ct, string originalStatus, string reason)
        {
            var history = new StudentHistory
            {
                StudentId = student.Id,
                OldStatus = originalStatus,
                NewStatus = student.Status,
                ChangedBy = "system",
                ChangedAt = DateTime.UtcNow,
                FieldChanges = $"Status changed: {reason}"
            };

            await _dbContext.StudentHistories.AddAsync(history, ct);
        }
        private static Student MapToEntity(CreateStudentDTO dto) => new()
        {
            Id = Guid.NewGuid(),
            
          
            FirstNameFr = dto.FirstNameFr,
            FirstNameAr = dto.FirstNameAr,
            LastNameFr = dto.LastNameFr,
            LastNameAr = dto.LastNameAr,
            DateOfBirth = dto.DateOfBirth,
            Gender = dto.Gender,
   
            Status = dto.Status
        };

        private static StudentDTO MapToDto(Student entity) => new()
        {

            UserId = entity.UserId,

            FirstNameFr = entity.FirstNameFr,
            FirstNameAr = entity.FirstNameAr,
            LastNameFr = entity.LastNameFr,
            LastNameAr = entity.LastNameAr,
            DateOfBirth = entity.DateOfBirth,
            Gender = entity.Gender,

            Status = entity.Status
        };

        private async Task LogStudentHistory(Student student,CancellationToken cancellationToken, Student originalValues = null, bool isNew = false)
        {
            var history = new StudentHistory
            {
                StudentId = student.Id,
                OldStatus = originalValues?.Status ?? (isNew ? null : student.Status),
                NewStatus = student.Status,
                ChangedBy = student.LastModifiedBy ?? "system",
                ChangedAt = DateTime.UtcNow,
                FieldChanges = isNew ? "New student created" : GetFieldChanges(originalValues, student)
            };

            await _dbContext.StudentHistories.AddAsync(history);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private string GetFieldChanges(Student original, Student updated)
        {
            var changes = new List<string>();

            if (original?.FirstNameFr != updated.FirstNameFr)
                changes.Add($"FirstNameFr: {original?.FirstNameFr} → {updated.FirstNameFr}");

            if (original?.Status != updated.Status)
                changes.Add($"Status: {original?.Status} → {updated.Status}");

            return string.Join(", ", changes);
        }

        private static PagedResult<StudentDTO> CreatePagedResult(
            List<StudentDTO> items,
            int totalCount,
            StudentFilterDTO filter) => new()
            {
                Results = items,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };

        private static IQueryable<Student> PaginateResults(
            IQueryable<Student> query,
            StudentFilterDTO filter) => query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);

        private void ValidateStatusTransition(string currentStatus, string newStatus)
        {
            var allowedTransitions = new Dictionary<string, List<string>>
            {
                ["Active"] = new List<string> { "Suspended", "Graduated", "Transferred" },
                ["Suspended"] = new List<string> { "Active", "Withdrawn" },
                ["Graduated"] = new List<string> { },
                ["Transferred"] = new List<string> { }
            };

            if (!allowedTransitions[currentStatus].Contains(newStatus))
                throw new InvalidOperationException($"Invalid status transition from {currentStatus} to {newStatus}");
        }

        #endregion

    }
}
