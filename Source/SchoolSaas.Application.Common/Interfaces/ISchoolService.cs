using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Edulink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface ISchoolService
    {
        // Basic CRUD
        Task<SchoolMetadataDTO> CreateSchoolAsync(CreateSchoolMetadataDTO dto, CancellationToken ct);
        Task<SchoolMetadataDTO> GetSchoolByIdAsync(Guid schoolId, CancellationToken ct);
        Task<List<SchoolMetadataDTO>> GetAllSchoolsAsync(CancellationToken ct);
        Task<SchoolMetadataDTO> UpdateSchoolAsync(Guid schoolId, UpdateSchoolMetadataDTO dto, CancellationToken ct);
        Task<bool> DeleteSchoolAsync(Guid schoolId, CancellationToken ct);

        // Bulk operations
        Task<List<SchoolMetadataDTO>> BulkCreateSchoolsAsync(IEnumerable<CreateSchoolMetadataDTO> dtos, CancellationToken ct);
        Task<List<SchoolMetadataDTO>> BulkUpdateSchoolsAsync(IEnumerable<UpdateSchoolMetadataDTO> dtos, CancellationToken ct);
        Task<bool> BulkDeleteSchoolsAsync(IEnumerable<Guid> schoolIds, CancellationToken ct);

        // Custom queries
        Task<List<SchoolMetadataDTO>> GetSchoolsByRegionAsync(Guid regionId, CancellationToken ct);
        Task<List<SchoolMetadataDTO>> GetSchoolsByCityAsync(Guid cityId, CancellationToken ct);
        Task<List<SchoolMetadataDTO>> SearchSchoolsAsync(string nameFragment, CancellationToken ct);

        // (Optional) Reporting snippet
        //Task<SchoolStatsDTO> GetSchoolStatsAsync(Guid schoolId, DateRangeDTO range, CancellationToken ct);
    }

}
