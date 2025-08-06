using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Backoffice.Academics.Queries.GetCurrentAcademicYear;
using SchoolSaas.Application.Backoffice.Referential.Cities.Queries;
using SchoolSaas.Application.Backoffice.Referential.Regions.Queries;
using SchoolSaas.Application.Backoffice.SchoolMetadata.Queries;
using SchoolSaas.Domain.Common.DataObjects.Edulink;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Backoffice.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    
    public class ReferencialController : ApiController
    {
        [HttpGet("GetAllCities")]
        public async Task<ActionResult<List<CityDTO>>> GetAllCities()
        {
            return await Mediator.Send(new GetAllCitiesQuery { });
        }
        [HttpGet("GetAllRegions")]
        public async Task<ActionResult<List<RegionDTO>>> GetAllRegions()
        {
            return await Mediator.Send(new GetAllRegionsQuery { });
        }

        [HttpGet("GetCitiesByRegion/{regionId}")]
        public async Task<ActionResult<List<CityDTO>>> GetCitiesByRegion(Guid regionId)
        {
            return await Mediator.Send(new GetCitiesByRegionQuery { RegionId = regionId });
        }

        [HttpGet("GetRegionById/{regionId}")]
        public async Task<ActionResult<RegionDTO>> GetRegionById(Guid regionId)
        {
            return await Mediator.Send(new GetRegionByIdQuery { RegionId = regionId });
        }

        [HttpGet("GetCityById/{cityId}")]
        public async Task<ActionResult<CityDTO>> GetCityById(Guid cityId)
        {
            return await Mediator.Send(new GetCityByIdQuery { CityId = cityId });
        }
        [HttpGet("SearchCities/{nameFragment}")]
        public async Task<ActionResult<List<CityDTO>>> SearchCities(string nameFragment)
        {
            return await Mediator.Send(new SearchCitiesQuery { NameFragment = nameFragment });
        }

        [HttpGet("SearchRegions/{nameFragment}")]
        public async Task<ActionResult<List<RegionDTO>>> SearchRegions(string nameFragment)
        {
            return await Mediator.Send(new SearchRegionsQuery { NameFragment = nameFragment });
        }
        [HttpGet("GetAllSchools")]
        public async Task<ActionResult<List<SchoolMetadataDTO>>> GetAllSchools()
        {
            return await Mediator.Send(new GetAllSchoolsQuery { });
        }
        [HttpGet("GetSchoolById/{schoolId}")]
        public async Task<ActionResult<SchoolMetadataDTO>> GetSchoolById(Guid schoolId)
        {
            return await Mediator.Send(new GetSchoolByIdQuery { SchoolId = schoolId });
        }
        [HttpGet("GetSchoolsByCity/{cityId}")]
        public async Task<ActionResult<List<SchoolMetadataDTO>>> GetSchoolByCity(Guid cityId)
        {
            return await Mediator.Send(new GetSchoolsByCityQuery { CityId = cityId });
        }
        [HttpGet("GetSchoolsByRegion/{regionId}")]
        public async Task<ActionResult<List<SchoolMetadataDTO>>> GetSchoolsByRegion(Guid regionId)
        {
            return await Mediator.Send(new GetSchoolsByRegionQuery { RegionId = regionId });
        }
        [HttpGet("SearchSchools/{nameFragment}")]
        public async Task<ActionResult<List<SchoolMetadataDTO>>> SearchSchools(string nameFragment)
        {
            return await Mediator.Send(new SearchSchoolsQuery { NameFragment = nameFragment });
        }
    }
}
