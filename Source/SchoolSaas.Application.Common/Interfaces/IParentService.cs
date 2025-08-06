using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Parent;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IParentService
    {
        Task<bool> CreateParentAsync(ParentDTO dto, CancellationToken cancellationToken);
        Task<ParentDTO> GetParentByUserId(string userId);
        Task<bool> UpdateParentAsync(Guid parentId, ParentDTO dto, CancellationToken cancellationToken);
        Task<ParentDTO> GetParentByIdAsync(Guid parentId, CancellationToken cancellationToken);
        Task<PagedResult<ParentDTO>> GetPaginatedParentsAsync(ParentFilterDTO filter, CancellationToken cancellationToken);
        Task<bool> DeleteParentAsync(Guid parentId, CancellationToken cancellationToken);
        Task<bool> BulkCreateParentsAsync(BulkParentDTO dto, CancellationToken cancellationToken);
        Task<List<ParentAuditDTO>> GetParentAuditLogsAsync(Guid parentId, CancellationToken cancellationToken);
        Task<bool> VerifyParentIdentityAsync(Guid parentId, ParentVerificationDTO dto, CancellationToken cancellationToken);
        Task<PagedResult<ParsedStudentDto>> GetAssociatedStudentsAsync(Guid parentId, int page, int? size, CancellationToken cancellationToken);
        Task<bool> UpdateContactPreferencesAsync(Guid parentId, ContactPreferencesDTO dto, CancellationToken cancellationToken);
       
        //Task<bool> ExportParentDataAsync(Guid branchId, ExportFormat format, CancellationToken cancellationToken);
    }
}
