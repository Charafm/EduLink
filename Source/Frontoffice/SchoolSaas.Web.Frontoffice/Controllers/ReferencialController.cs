using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;
using SchoolSaas.Domain.Common.DataObjects;
using SchoolSaas.Domain.Common.DataObjects.Edulink;


namespace SchoolSaas.Web.Edulink.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class ReferentialController : ControllerBase
    {
        // --- mock data store ---
        private static readonly List<RegionDTO> _regions = new()
        {
            new RegionDTO {
                Id     = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                NameEn = "Casablanca-Settat",
                NameFr = "Casablanca-Settat",
                NameAr = "الدار البيضاء - سطات"
            },
            new RegionDTO {
                Id     = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                NameEn = "Rabat-Salé-Kénitra",
                NameFr = "Rabat-Salé-Kénitra",
                NameAr = "الرباط - سلا - القنيطرة"
            }
            // …add the other 10+ regions here
        };

        private static readonly List<CityDTO> _cities = new()
        {
            new CityDTO {
                Id       = Guid.Parse("6B1E521E-AFF2-4E07-9221-88D2774A3A4E"),
                NameEn   = "Casablanca",
                NameFr   = "Casablanca",
                NameAr   = "الدار البيضاء",
                RegionId = _regions[0].Id
            },
            new CityDTO {
                Id       = Guid.Parse("1E061BB6-92A6-498A-A1EF-35EF46C5732E"),
                NameEn   = "Marrakech",
                NameFr   = "Marrakech",
                NameAr   = "مراكش",
                RegionId = _regions[1].Id
            }
            // …and so on
        };

        private static readonly List<SchoolMetadataDTO> _schools = new()
        {
            new SchoolMetadataDTO {
                Id                 = Guid.Parse("5E6E7D89-D45E-4F65-8AD2-78BD9E0201A4"),
                NameEn             = "Central School",
                NameFr             = "École Centrale",
                NameAr             = "المدرسة المركزية",
                Code               = "CENTRAL2025",
                AddressFr          = "123 Bd Zerktouni",
                AddressAr          = "123 شارع الزرقطوني",
                RegionId           = _regions[0].Id,
                RegionNameEn       = _regions[0].NameEn,
                RegionNameFr       = _regions[0].NameFr,
                RegionNameAr       = _regions[0].NameAr,
                CityId             = _cities[0].Id,
                CityNameEn         = _cities[0].NameEn,
                CityNameFr         = _cities[0].NameFr,
                CityNameAr         = _cities[0].NameAr,
                UseIsolatedDatabase= true,
                HasCustomConnectionString = false,
                LogoUrl            = null,
                TimeZoneId         = "Africa/Casablanca"
            }
            // …more schools if you like
        };

        // --- Regions ---
        [HttpGet("GetAllRegions")]
        public ActionResult<List<RegionDTO>> GetAllRegions()
            => Ok(_regions);

        [HttpGet("GetRegionById/{regionId:guid}")]
        public ActionResult<RegionDTO> GetRegionById(Guid regionId)
            => _regions.FirstOrDefault(r => r.Id == regionId) is var r && r != null
                ? Ok(r)
                : NotFound();

        [HttpGet("SearchRegions/{fragment}")]
        public ActionResult<List<RegionDTO>> SearchRegions(string fragment)
            => Ok(_regions.Where(r =>
                    r.NameEn.Contains(fragment, StringComparison.OrdinalIgnoreCase) ||
                    r.NameFr.Contains(fragment, StringComparison.OrdinalIgnoreCase))
                .ToList());

        // --- Cities ---
        [HttpGet("GetAllCities")]
        public ActionResult<List<CityDTO>> GetAllCities()
            => Ok(_cities);

        [HttpGet("GetCityById/{cityId:guid}")]
        public ActionResult<CityDTO> GetCityById(Guid cityId)
            => _cities.FirstOrDefault(c => c.Id == cityId) is var cty && cty != null
                ? Ok(cty)
                : NotFound();

        [HttpGet("GetCitiesByRegion/{regionId:guid}")]
        public ActionResult<List<CityDTO>> GetCitiesByRegion(Guid regionId)
            => Ok(_cities.Where(c => c.RegionId == regionId).ToList());

        [HttpGet("SearchCities/{fragment}")]
        public ActionResult<List<CityDTO>> SearchCities(string fragment)
            => Ok(_cities.Where(c =>
                    c.NameEn.Contains(fragment, StringComparison.OrdinalIgnoreCase) ||
                    c.NameFr.Contains(fragment, StringComparison.OrdinalIgnoreCase))
                .ToList());

        // --- Schools ---
        [HttpGet("GetAllSchools")]
        public ActionResult<List<SchoolMetadataDTO>> GetAllSchools()
            => Ok(_schools);

        [HttpGet("GetSchoolById/{schoolId:guid}")]
        public ActionResult<SchoolMetadataDTO> GetSchoolById(Guid schoolId)
            => _schools.FirstOrDefault(s => s.Id == schoolId) is var srm && srm != null
                ? Ok(srm)
                : NotFound();

        [HttpGet("GetSchoolsByCity/{cityId:guid}")]
        public ActionResult<List<SchoolMetadataDTO>> GetSchoolsByCity(Guid cityId)
            => Ok(_schools.Where(s => s.CityId == cityId).ToList());

        [HttpGet("GetSchoolsByRegion/{regionId:guid}")]
        public ActionResult<List<SchoolMetadataDTO>> GetSchoolsByRegion(Guid regionId)
            => Ok(_schools.Where(s => s.RegionId == regionId).ToList());

        [HttpGet("SearchSchools/{fragment}")]
        public ActionResult<List<SchoolMetadataDTO>> SearchSchools(string fragment)
            => Ok(_schools.Where(s =>
                    s.NameEn.Contains(fragment, StringComparison.OrdinalIgnoreCase) ||
                    s.NameFr.Contains(fragment, StringComparison.OrdinalIgnoreCase))
                .ToList());
    }
}
