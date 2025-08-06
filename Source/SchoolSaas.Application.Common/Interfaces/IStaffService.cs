using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Staff;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IStaffService
    {
        Task<CreateUserRequestDto> CreateStaffAsync(CreateStaffDTO dto, CancellationToken cancellationToken);
        Task<bool> UpdateStaffAsync(Guid staffId, StaffDTO dto, CancellationToken cancellationToken);
        Task<StaffDTO> GetStaffByIdAsync(Guid staffId, CancellationToken cancellationToken);
        Task<PagedResult<StaffDTO>> GetPaginatedStaffAsync(StaffFilterDTO filter, CancellationToken cancellationToken);
        Task<bool> DeleteStaffAsync(Guid staffId, CancellationToken cancellationToken);
        Task<bool> BulkCreateStaffAsync(BulkStaffDTO dto, CancellationToken cancellationToken);
        Task<bool> AssignRoleAsync(Guid staffId, StaffRoleDTO dto, CancellationToken cancellationToken);
        Task<List<StaffAuditDTO>> GetStaffAuditLogsAsync(Guid staffId, CancellationToken cancellationToken);

        Task<StaffDTO> GetStaffByUserId(string userId);
        //Task<bool> ExportStaffDataAsync(Guid branchId, ExportFormat format, CancellationToken cancellationToken);
    }
}
