using Microsoft.EntityFrameworkCore;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice.Staff;
using SchoolSaas.Domain.Common.DataObjects.Staff;
using SchoolSaas.Domain.Common.Enums;
using System.Text.Json;
using static SchoolSaas.Infrastructure.Backoffice.Services.StudentService;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class StaffService : IStaffService
    {
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly IServiceHelper _serviceHelper;
        private readonly IHttpClientFactory _httpClientFactory;

        public StaffService(
            IBackofficeContext dbContext,
            IBackofficeReadOnlyContext dbReadOnlyContext,
            IServiceHelper serviceHelper, IHttpClientFactory httpClientFactory)
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _serviceHelper = serviceHelper;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CreateUserRequestDto> CreateStaffAsync(CreateStaffDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var httpClient = _httpClientFactory.CreateClient("EdulinkIdentityFrontal");
                var identityUrl = $"{httpClient.BaseAddress}identity/createstaffuser";

                var password = $"Edu{Guid.NewGuid().ToString("N").Substring(0, 8)}*";
                var userDto = new CreateUserRequestDto
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
                    System.Text.Encoding.UTF8,
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

               
                
                var staff = new Staff
                {
                    UserId = identityUser?.Id.ToString(),
                    BranchId = Guid.NewGuid(),
                    FirstNameFr = dto.FirstNameFr,
                    FirstNameAr = dto.FirstNameAr,
                    LastNameFr = dto.LastNameFr,
                    LastNameAr = dto.LastNameAr,
                    DepartmentFr = dto.DepartmentFr,
                    DepartmentAr = dto.DepartmentAr,
                    JobTitleFr = dto.JobTitleFr,
                    JobTitleAr = dto.JobTitleAr,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Role = dto.Role
                };

                await _dbContext.Staffs.AddAsync(staff, cancellationToken);
                //await LogStaffAudit(staff, StaffActionType.Created, staff.CreatedBy, cancellationToken);
                return userDto;
            });
        }
        public async Task<bool> BulkCreateStaffAsync(BulkStaffDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var staffList = dto.Records.Select(r => new Staff
                {
                    UserId = r.UserId,
                    BranchId = r.BranchId,
                    FirstNameFr = r.FirstNameFr,
                    FirstNameAr = r.FirstNameAr,
                    LastNameFr = r.LastNameFr,
                    LastNameAr = r.LastNameAr,
                    DepartmentFr = r.DepartmentFr,
                    DepartmentAr = r.DepartmentAr,
                    JobTitleFr = r.JobTitleFr,
                    JobTitleAr = r.JobTitleAr,
                    Email = r.Email,
                    Phone = r.Phone,
                    Role = r.Role
                }).ToList();

                await _dbContext.Staffs.AddRangeAsync(staffList, cancellationToken);

                foreach (var staff in staffList)
                {
                    //await LogStaffAudit(staff, StaffActionType.Created, dto.InitiatedBy, cancellationToken);
                }

                return true;
            });
        }
        public async Task<StaffDTO> GetStaffByUserId(string userId)
        {
            var query = _dbReadOnlyContext.Staffs
               .AsNoTracking().FirstOrDefaultAsync(s => s.UserId == userId).Result;
            if (query == null)
            {
                throw new KeyNotFoundException("Staff not found");
            }
            return MapToDto(query);
        }
        public async Task<Application.Common.Models.PagedResult<StaffDTO>> GetPaginatedStaffAsync(
            StaffFilterDTO filter,
            CancellationToken cancellationToken)
        {
            var query = _dbReadOnlyContext.Staffs
                .AsNoTracking();
            if(filter.BranchId != null)
            {
                query.Where(s => s.BranchId == filter.BranchId);
            }

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(s =>
                    s.FirstNameFr.Contains(filter.SearchTerm) ||
                    s.FirstNameAr.Contains(filter.SearchTerm) ||
                    s.LastNameFr.Contains(filter.SearchTerm) ||
                    s.LastNameAr.Contains(filter.SearchTerm));
            }

            if (filter.Role.HasValue)
            {
                query = query.Where(s => s.Role == filter.Role.Value);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var results = await query
                .OrderBy(s => s.LastNameFr)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(s => MapToDto(s))
                .ToListAsync(cancellationToken);

            return new PagedResult<StaffDTO>
            {
                Results = results,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };
        }

        public async Task<bool> AssignRoleAsync(Guid staffId, StaffRoleDTO dto,
            CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
              

                var staff = await _dbContext.Staffs
                    .FirstOrDefaultAsync(s => s.Id == staffId, cancellationToken)
                    ?? throw new KeyNotFoundException("Staff not found");

                staff.Role = dto.Role;
                //await LogStaffAudit(staff, StaffActionType.Created, staff.LastModifiedBy, cancellationToken);

                return true;
            });
        }
        public async Task<bool> UpdateStaffAsync(Guid staffId, StaffDTO dto, CancellationToken cancellationToken)
        {
            await _dbContext.BeginTransactionAsync();
            try
            {
                var staff = await _dbContext.Staffs.FirstOrDefaultAsync(s => s.Id == staffId, cancellationToken);
                if (staff == null)
                    throw new KeyNotFoundException("Staff record not found.");

                staff.UserId = dto.UserId;
                staff.BranchId = dto.BranchId;
                staff.FirstNameFr = dto.FirstNameFr;
                staff.FirstNameAr = dto.FirstNameAr;
                staff.LastNameFr = dto.LastNameFr;
                staff.LastNameAr = dto.LastNameAr;
                staff.DepartmentFr = dto.DepartmentFr;
                staff.DepartmentAr = dto.DepartmentAr;
                staff.Role= dto.Role;
                staff.JobTitleFr = dto.JobTitleFr;
                staff.JobTitleAr = dto.JobTitleAr;
                staff.Email = dto.Email;
                staff.Phone = dto.Phone;
                staff.LastModified = DateTime.UtcNow;

                _dbContext.Staffs.Update(staff);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await _dbContext.CommitTransactionAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _dbContext.RollbackTransaction();
                return false;
            }
        }
        public async Task<bool> DeleteStaffAsync(Guid staffId, CancellationToken cancellationToken)
        {
            await _dbContext.BeginTransactionAsync();
            try
            {
                var staff = await _dbContext.Staffs.FirstOrDefaultAsync(s => s.Id == staffId, cancellationToken);
                if (staff == null)
                    throw new KeyNotFoundException("Staff record not found.");

                // Soft delete
                staff.IsDeleted = true;
                staff.LastModified = DateTime.UtcNow;

                _dbContext.Staffs.Update(staff);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await _dbContext.CommitTransactionAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _dbContext.RollbackTransaction();
                return false;
            }
        }
        public async Task<StaffDTO> GetStaffByIdAsync(Guid staffId, CancellationToken cancellationToken)
        {
            try
            {
                var staff = await _dbReadOnlyContext.Staffs
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == staffId, cancellationToken);
                if (staff == null)
                    throw new KeyNotFoundException("Staff record not found.");

                return new StaffDTO
                {
                    UserId = staff.UserId,
                    BranchId = staff.BranchId,
                    FirstNameFr = staff.FirstNameFr,
                    FirstNameAr = staff.FirstNameAr,
                    LastNameFr = staff.LastNameFr,
                    LastNameAr = staff.LastNameAr,
                    DepartmentFr = staff.DepartmentFr,
                    DepartmentAr = staff.DepartmentAr,
                    JobTitleFr = staff.JobTitleFr,
                    JobTitleAr = staff.JobTitleAr,
                    Email = staff.Email,
                    Phone = staff.Phone
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public async Task<List<StaffAuditDTO>> GetStaffAuditLogsAsync(Guid staffId, CancellationToken cancellationToken)
        {
            return await _dbReadOnlyContext.StaffAudits
                .Where(a => a.StaffId == staffId)
                .OrderByDescending(a => a.ActionDate)
                .Select(a => new StaffAuditDTO
                {
                    ActionDate = a.ActionDate,
                    ActionType = a.ActionType,
                    Details = a.Details,
                    PerformedBy = a.PerformedBy
                })
                .ToListAsync(cancellationToken);
        }
        

        //public async Task<bool> ExportStaffDataAsync(Guid branchId, ExportFormat format, CancellationToken cancellationToken)
        //{
        //    return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
        //    {
        //        var staffData = await _dbReadOnlyContext.Staffs
        //            .Where(s => s.BranchId == branchId)
        //            .ToListAsync(cancellationToken);

        //        var exportData = staffData.Select(MapToDto).ToList();

        //        return await _exportService.ExportDataAsync(exportData, format, "StaffData");
        //    });
        //}

        // Existing methods (UpdateStaffAsync, GetStaffByIdAsync, DeleteStaffAsync) updated with audit logging

        #region Private Helpers
        private async Task LogStaffAudit(Staff staff,  StaffActionType actionType, string performedBy, CancellationToken cancellationToken, string originalValues = null)
        {
            var audit = new StaffAudit
            {
                StaffId = staff.Id,
                ActionType = actionType,
                ActionDate = DateTime.UtcNow,
                Details = originalValues ?? JsonSerializer.Serialize(staff),
                PerformedBy = performedBy
            };

            await _dbContext.StaffAudits.AddAsync(audit);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        private static string CaptureOriginalValues(Staff staff)
        {
            return JsonSerializer.Serialize(new
            {
                staff.UserId,
                staff.BranchId,
                staff.FirstNameFr,
                staff.FirstNameAr,
                staff.LastNameFr,
                staff.LastNameAr,
                staff.DepartmentFr,
                staff.DepartmentAr,
                staff.JobTitleFr,
                staff.JobTitleAr,
                staff.Email,
                staff.Phone,
                staff.Role
            });
        }
        private static StaffDTO MapToDto(Staff staff) => new()
        {
            Id = staff.Id,
            UserId = staff.UserId,
            BranchId = staff.BranchId,
            FirstNameFr = staff.FirstNameFr,
            FirstNameAr = staff.FirstNameAr,
            LastNameFr = staff.LastNameFr,
            LastNameAr = staff.LastNameAr,
            DepartmentFr = staff.DepartmentFr,
            DepartmentAr = staff.DepartmentAr,
            JobTitleFr = staff.JobTitleFr,
            JobTitleAr = staff.JobTitleAr,
            Email = staff.Email,
            Phone = staff.Phone,
            Role = staff.Role
        };
        #endregion
    }
}
