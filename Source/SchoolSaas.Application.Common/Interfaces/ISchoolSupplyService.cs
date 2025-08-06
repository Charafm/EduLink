using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Book;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using SchoolSaas.Domain.Common.DataObjects.SchoolSupply;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface ISchoolSupplyService
    {
        Task<bool> CreateSchoolSupplyAsync(SchoolSupplyDTO dto, CancellationToken cancellationToken);
        Task<bool> UpdateSchoolSupplyAsync(Guid supplyId, SchoolSupplyDTO dto, CancellationToken cancellationToken);
        Task<PagedResult<SchoolSupplyDTO>> GetPaginatedSuppliesAsync(
            SchoolSupplyFilterDTO filter, CancellationToken cancellationToken);
        Task<bool> BulkCreateSuppliesAsync(BulkSchoolSupplyDTO dto,
            CancellationToken cancellationToken);
        Task<bool> CreateBookAsync(BookDTO dto, CancellationToken cancellationToken);
        Task<bool> UpdateBookAsync(Guid bookId, BookDTO dto, CancellationToken cancellationToken);
        Task<bool> AssignResourceToGradeAsync(GradeResourceDTO dto, CancellationToken cancellationToken);
        Task<PagedResult<GradeResourceDTO>> GetPaginatedResourcesAsync(
            ResourceFilterDTO filter, CancellationToken cancellationToken);
        Task<bool> BulkAssignResourcesAsync(BulkResourceAssignmentDTO dto,
            CancellationToken cancellationToken);
      
        Task<bool> DeleteSupplyAsync(Guid supplyId, CancellationToken cancellationToken);
        //Task<List<AssignmentHistoryDTO>> GetResourceAssignmentHistoryAsync(Guid resourceId, CancellationToken cancellationToken);
    }
}
