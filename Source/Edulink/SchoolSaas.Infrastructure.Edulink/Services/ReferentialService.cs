using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Edulink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Edulink.Services
{
    public class ReferentialService : IReferentialService
    {
        private readonly IEdulinkContext _db;
        

        public ReferentialService(IEdulinkContext db)
        {
            _db = db;
        
        }

        public async Task<List<RegionDTO>> GetAllRegionsAsync(CancellationToken ct)
        {
            var list = _db.Regions
              
                .Select(r => new RegionDTO { Id = r.Id, NameFr = r.NameFr, NameAr = r.NameAr, NameEn = r.NameEn })
                .ToList();
            return await Task.FromResult((List<RegionDTO>)list);
        }

        public async Task<RegionDTO> GetRegionByIdAsync(Guid regionId, CancellationToken ct)
        {
            var r = await _db.Regions.FindAsync(new object[] { regionId }, ct);
            if (r == null) return null;
            return new RegionDTO { Id = r.Id, NameFr = r.NameFr, NameAr = r.NameAr, NameEn = r.NameEn };
        }

        public async Task<List<RegionDTO>> SearchRegionsAsync(string nameFragment, CancellationToken ct)
        {
            var list = _db.Regions
                .Where(r => (r.NameFr.Contains(nameFragment) || r.NameAr.Contains(nameFragment) || r.NameEn.Contains(nameFragment)))
                .Select(r => new RegionDTO { Id = r.Id, NameFr = r.NameFr, NameAr = r.NameAr, NameEn = r.NameEn })
                .ToList();
            return await Task.FromResult((List<RegionDTO>)list);
        }

        public async Task<List<CityDTO>> GetAllCitiesAsync(CancellationToken ct)
        {
            var list = _db.Cities
                
                .Select(c => new CityDTO { Id = c.Id, NameFr = c.NameFr, NameAr = c.NameAr, NameEn = c.NameEn, RegionId = c.RegionId })
                .ToList();
            return await Task.FromResult((List<CityDTO>)list);
        }

        public async Task<CityDTO> GetCityByIdAsync(Guid cityId, CancellationToken ct)
        {
            var c = await _db.Cities.FindAsync(new object[] { cityId }, ct);
            if (c == null) return null;
            return new CityDTO { Id = c.Id, NameFr = c.NameFr, NameAr = c.NameAr, NameEn = c.NameEn, RegionId = c.RegionId };
        }

        public async Task<List<CityDTO>> SearchCitiesAsync(string nameFragment, CancellationToken ct)
        {
            var list = _db.Cities
                .Where(c =>  (c.NameFr.Contains(nameFragment) || c.NameAr.Contains(nameFragment) || c.NameEn.Contains(nameFragment)))
                .Select(c => new CityDTO { Id = c.Id, NameFr = c.NameFr, NameAr = c.NameAr, NameEn = c.NameEn, RegionId = c.RegionId })
                .ToList();
            return await Task.FromResult((List<CityDTO>)list);
        }

        public async Task<List<CityDTO>> GetCitiesByRegionAsync(Guid regionId, CancellationToken ct)
        {
            var list = _db.Cities
                .Where(c =>   c.RegionId == regionId)
                .Select(c => new CityDTO { Id = c.Id, NameFr = c.NameFr, NameAr = c.NameAr, NameEn = c.NameEn, RegionId = c.RegionId })
                .ToList();
            return await Task.FromResult((List<CityDTO>)list);
        }
    }
}
