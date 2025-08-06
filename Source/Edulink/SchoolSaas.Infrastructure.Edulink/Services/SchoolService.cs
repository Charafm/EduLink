using Microsoft.EntityFrameworkCore;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Edulink;
using SchoolSaas.Domain.Edulink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Edulink.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly IEdulinkContext _db;
        private readonly IEdulinkServiceHelper _helper;

        public SchoolService(IEdulinkContext db, IEdulinkServiceHelper helper)
        {
            _db = db;
            _helper = helper;
        }

        public Task<SchoolMetadataDTO> CreateSchoolAsync(CreateSchoolMetadataDTO dto, CancellationToken ct) =>
            _helper.ExecuteWithTransactionAsync(async () =>
            {
                var entity = new SchoolMetadata
                {
                    NameFr = dto.NameFr,
                    NameAr = dto.NameAr,
                    NameEn = dto.NameEn,
                    Code = dto.Code,
                    AddressFr = dto.AddressFr,
                    AddressAr = dto.AddressAr,
                    AddressEn = dto.AddressEn,
                    CityId = dto.CityId,
                    UseIsolatedDatabase = dto.UseIsolatedDatabase,
                    LogoUrl = dto.LogoUrl,
                    TimeZoneId = dto.TimeZoneId
                };
                _db.Schools.Add(entity);
                await _db.SaveChangesAsync(ct);
                return MapToDto(entity);
            });

        public Task<bool> DeleteSchoolAsync(Guid schoolId, CancellationToken ct) =>
            _helper.ExecuteWithTransactionAsync(async () =>
            {
                var entity = await _db.Schools.FindAsync(new object[] { schoolId }, ct);
                if (entity == null) return false;
                entity.IsDeleted = true;
                _db.Schools.Remove(entity);
                await _db.SaveChangesAsync(ct);
                return true;
            });

        public async Task<List<SchoolMetadataDTO>> GetAllSchoolsAsync(CancellationToken ct)
        {
            var schools = await _db.Schools
                .Include(s => s.City)
                .ThenInclude(c => c.Region)
                .ToListAsync(ct);

            return MapToDtoList(schools);
        }


        public async Task<SchoolMetadataDTO> GetSchoolByIdAsync(Guid schoolId, CancellationToken ct)
        {
            var school = await _db.Schools
                .Include(s => s.City)
                .ThenInclude(c => c.Region)
                .FirstOrDefaultAsync(s => s.Id == schoolId, ct);

            return school == null ? null : MapToDto(school);
        }

        public Task<SchoolMetadataDTO> UpdateSchoolAsync(Guid schoolId, UpdateSchoolMetadataDTO dto, CancellationToken ct) =>
            _helper.ExecuteWithTransactionAsync(async () =>
            {
                var entity = await _db.Schools.FindAsync(new object[] { schoolId }, ct);
                if (entity == null) throw new KeyNotFoundException();
                entity.NameFr = dto.NameFr;
                entity.NameAr = dto.NameAr;
                entity.NameEn = dto.NameEn;
                entity.Code = dto.Code;
                entity.AddressFr = dto.AddressFr;
                entity.AddressAr = dto.AddressAr;
                entity.AddressEn = dto.AddressEn;
                entity.CityId = dto.CityId;
                entity.UseIsolatedDatabase = dto.UseIsolatedDatabase;
                entity.LogoUrl = dto.LogoUrl;
                entity.TimeZoneId = dto.TimeZoneId;
                await _db.SaveChangesAsync(ct);
                return MapToDto(entity);
            });

        public Task<List<SchoolMetadataDTO>> BulkCreateSchoolsAsync(IEnumerable<CreateSchoolMetadataDTO> dtos, CancellationToken ct) =>
            _helper.ExecuteWithTransactionAsync<List<SchoolMetadataDTO>>(async () =>
            {
                var entities = dtos.Select(dto => new SchoolMetadata
                {
                    NameFr = dto.NameFr,
                    NameAr = dto.NameAr,
                    NameEn = dto.NameEn,
                    Code = dto.Code,
                    AddressFr = dto.AddressFr,
                    AddressAr = dto.AddressAr,
                    AddressEn = dto.AddressEn,
                    CityId = dto.CityId,
                    UseIsolatedDatabase = dto.UseIsolatedDatabase,
                    LogoUrl = dto.LogoUrl,
                    TimeZoneId = dto.TimeZoneId
                }).ToList();
                _db.Schools.AddRange(entities);
                await _db.SaveChangesAsync(ct);
                return entities.Select(MapToDto).ToList();
            });

        public Task<List<SchoolMetadataDTO>> BulkUpdateSchoolsAsync(IEnumerable<UpdateSchoolMetadataDTO> dtos, CancellationToken ct) =>
            _helper.ExecuteWithTransactionAsync(async () =>
            {
                var results = new List<SchoolMetadataDTO>();
                foreach (var dto in dtos)
                {
                    var entity = await _db.Schools.FindAsync(new object[] { dto.Id }, ct);
                    if (entity == null) continue;
                    entity.NameFr = dto.NameFr;
                    entity.NameAr = dto.NameAr;
                    entity.NameEn = dto.NameEn;
                    entity.Code = dto.Code;
                    entity.AddressFr = dto.AddressFr;
                    entity.AddressAr = dto.AddressAr;
                    entity.AddressEn = dto.AddressEn;
                    entity.CityId = dto.CityId;
                    entity.UseIsolatedDatabase = dto.UseIsolatedDatabase;
                    entity.LogoUrl = dto.LogoUrl;
                    entity.TimeZoneId = dto.TimeZoneId;
                    results.Add(MapToDto(entity));
                }
                await _db.SaveChangesAsync(ct);
                return (List<SchoolMetadataDTO>)results;
            });

        public Task<bool> BulkDeleteSchoolsAsync(IEnumerable<Guid> schoolIds, CancellationToken ct) =>
            _helper.ExecuteWithTransactionAsync(async () =>
            {
                var entities = _db.Schools.Where(s => schoolIds.Contains(s.Id));
                foreach (var e in entities)
                    e.IsDeleted = true;
                await _db.SaveChangesAsync(ct);
                return true;
            });

        public async Task<List<SchoolMetadataDTO>> GetSchoolsByRegionAsync(Guid regionId, CancellationToken ct)
        {
            var schools = await _db.Schools
                .Include(s => s.City)
                .ThenInclude(c => c.Region)
                .Where(s => s.City.RegionId == regionId)
                .ToListAsync(ct);

            return MapToDtoList(schools);
        }

        public async Task<List<SchoolMetadataDTO>> GetSchoolsByCityAsync(Guid cityId, CancellationToken ct)
        {
            var schools = await _db.Schools
                .Include(s => s.City)
                .ThenInclude(c => c.Region)
                .Where(s => s.CityId == cityId)
                .ToListAsync(ct);

            return MapToDtoList(schools);
        }
        public async Task<List<SchoolMetadataDTO>> SearchSchoolsAsync(string nameFragment, CancellationToken ct)
        {
            var schools = await _db.Schools
                .Include(s => s.City)
                .ThenInclude(c => c.Region)
                .Where(s =>
                    s.NameFr.Contains(nameFragment) ||
                    s.NameAr.Contains(nameFragment) ||
                    s.NameEn.Contains(nameFragment))
                .ToListAsync(ct);

            return MapToDtoList(schools);
        }

        //public async Task<SchoolStatsDTO> GetSchoolStatsAsync(Guid schoolId, DateRangeDTO range, CancellationToken ct)
        //{
        //    // stub implementation
        //    return await Task.FromResult(new SchoolStatsDTO
        //    {
        //        SchoolId = schoolId,
        //        TotalStudents = 0,
        //        AvgGPA = 0,
        //        AttendanceRate = 0,
        //        TransferCount = 0
        //    });
        //}
        private List<SchoolMetadataDTO> MapToDtoList(List<SchoolMetadata> schools)
        {
            List<SchoolMetadataDTO> list = new List<SchoolMetadataDTO>();

            foreach (var s in schools)
            {
                list.Add(new SchoolMetadataDTO
                {
                    Id = s.Id,
                    NameFr = s.NameFr,
                    NameAr = s.NameAr,
                    NameEn = s.NameEn,
                    Code = s.Code,
                    AddressFr = s.AddressFr,
                    AddressAr = s.AddressAr,
                    AddressEn = s.AddressEn,
                    CityId = s.CityId,
                    RegionId = s.City.RegionId,
                    RegionNameFr = s.City?.Region?.NameFr,
                    RegionNameAr = s.City?.Region?.NameAr,
                    RegionNameEn = s.City?.Region?.NameEn,
                    CityNameFr = s.City?.NameFr,
                    CityNameAr = s.City?.NameAr,
                    CityNameEn = s.City?.NameEn,
                    UseIsolatedDatabase = s.UseIsolatedDatabase,
                    HasCustomConnectionString = !string.IsNullOrWhiteSpace(s.BackOfficeDbConnectionString),
                    LogoUrl = s.LogoUrl,
                    TimeZoneId = s.TimeZoneId
                });
            }

            return list;
        }

        private SchoolMetadataDTO MapToDto(SchoolMetadata s)
        {
            return new SchoolMetadataDTO
            {
                Id = s.Id,
                NameFr = s.NameFr,
                NameAr = s.NameAr,
                NameEn = s.NameEn,
                Code = s.Code,
                AddressFr = s.AddressFr,
                AddressAr = s.AddressAr,
                AddressEn = s.AddressEn,
                CityId = s.CityId,
                RegionId = s.City.RegionId,
                RegionNameFr = s.City.Region.NameFr,
                RegionNameAr = s.City.Region.NameAr,
                RegionNameEn = s.City.Region.NameEn,
                CityNameFr = s.City.NameFr,
                CityNameAr = s.City.NameAr,
                CityNameEn = s.City.NameEn,
                UseIsolatedDatabase = s.UseIsolatedDatabase,
                HasCustomConnectionString = !string.IsNullOrWhiteSpace(s.BackOfficeDbConnectionString),
                LogoUrl = s.LogoUrl,
                TimeZoneId = s.TimeZoneId
            };
        }
    }

}
