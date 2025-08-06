using SchoolSaas.Domain.Common.DataObjects.Edulink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IReferentialService
    {
        // Regions
        Task<List<RegionDTO>> GetAllRegionsAsync(CancellationToken ct);
        Task<RegionDTO> GetRegionByIdAsync(Guid regionId, CancellationToken ct);
        Task<List<RegionDTO>> SearchRegionsAsync(string nameFragment, CancellationToken ct);

        // Cities
        Task<List<CityDTO>> GetAllCitiesAsync(CancellationToken ct);
        Task<CityDTO> GetCityByIdAsync(Guid cityId, CancellationToken ct);
        Task<List<CityDTO>> SearchCitiesAsync(string nameFragment, CancellationToken ct);

        // Cities in a given Region
        Task<List<CityDTO>> GetCitiesByRegionAsync(Guid regionId, CancellationToken ct);
    }

}
