using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Parent;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IStudentService
    {
        Task<CreateStudentUserDTO> CreateStudentAsync(CreateStudentDTO dto, CancellationToken cancellationToken);
        Task<bool> UpdateStudentAsync(Guid studentId, StudentDTO dto, CancellationToken cancellationToken);
        Task<StudentDTO> GetStudentByUserId(string userId);
        Task<PagedResult<StudentDTO>> GetPaginatedStudentsAsync(StudentFilterDTO filter, CancellationToken cancellationToken);
        Task<StudentDetailDTO> GetStudentWithDetailsAsync(Guid studentId, CancellationToken cancellationToken);
        Task<bool> TransitionStudentStatusAsync(Guid studentId, StudentStatusTransitionDTO dto, CancellationToken cancellationToken);
        Task<bool> BulkUpdateStudentsAsync(BulkStudentUpdateDTO dto, CancellationToken cancellationToken);
        Task<bool> UpdateStudentDetailsAsync(Guid studentId, StudentDetailDTO dto, CancellationToken cancellationToken);
        Task<PagedResult<StudentParentDTO>> GetStudentParentsAsync(Guid studentId, CancellationToken cancellationToken);
        Task<PagedResult<StudentHistoryDTO>> GetStudentHistoryAsync(StudentHistoryFilterDTO filter, CancellationToken cancellationToken);
        Task<StudentTranscriptDTO> GetStudentTranscriptAsync(Guid studentId, CancellationToken cancellationToken);
        Task<bool> UpdateStudentParentsAsync(Guid studentId, List<ParentLinkDTO> parentInfo, CancellationToken cancellationToken);
       
    }
}
