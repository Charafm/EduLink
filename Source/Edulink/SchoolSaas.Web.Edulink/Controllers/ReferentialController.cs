using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Edulink.Referential.Cities.Queries;
using SchoolSaas.Application.Edulink.Referential.Regions.Queries;
using SchoolSaas.Domain.Common.DataObjects.Edulink;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Edulink.Controllers
{
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class ReferentialController : ApiController
    {
        // Regions
        [HttpGet("regions")]
        public async Task<ActionResult<List<RegionDTO>>> GetAllRegions(CancellationToken ct)
        {
            return await Mediator.Send(new GetAllRegionsQuery(), ct);
        }
            

        [HttpGet("regions/{regionId:guid}")]
        public async Task<ActionResult<RegionDTO>> GetRegionById(Guid regionId, CancellationToken ct)
        {
            return await Mediator.Send(new GetRegionByIdQuery { RegionId = regionId }, ct);
        }
            

        [HttpGet("regions/search")]
        public async Task<ActionResult<List<RegionDTO>>> SearchRegions([FromQuery] string nameFragment, CancellationToken ct) =>
            Ok(await Mediator.Send(new SearchRegionsQuery { NameFragment = nameFragment }, ct));

        // Cities
        [HttpGet("cities")]
        public async Task<ActionResult<List<CityDTO>>> GetAllCities(CancellationToken ct) =>
            Ok(await Mediator.Send(new GetAllCitiesQuery(), ct));

        [HttpGet("cities/{cityId:guid}")]
        public async Task<ActionResult<CityDTO>> GetCityById(Guid cityId, CancellationToken ct) =>
            Ok(await Mediator.Send(new GetCityByIdQuery { CityId = cityId }, ct));

        [HttpGet("cities/search")]
        public async Task<ActionResult<List<CityDTO>>> SearchCities([FromQuery] string nameFragment, CancellationToken ct) =>
            Ok(await Mediator.Send(new SearchCitiesQuery { NameFragment = nameFragment }, ct));

        [HttpGet("regions/{regionId:guid}/cities")]
        public async Task<ActionResult<List<CityDTO>>> GetCitiesByRegion(Guid regionId, CancellationToken ct) =>
            Ok(await Mediator.Send(new GetCitiesByRegionQuery { RegionId = regionId }, ct));
    }
}
