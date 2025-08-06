using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Edulink.SchoolMetadata.Commands.Bulk;
using SchoolSaas.Application.Edulink.SchoolMetadata.Commands;
using SchoolSaas.Application.Edulink.SchoolMetadata.Queries;
using SchoolSaas.Web.Common.Controllers;
using SchoolSaas.Domain.Common.DataObjects.Edulink;

namespace SchoolSaas.Web.Edulink.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class SchoolController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<SchoolMetadataDTO>>> GetAll(CancellationToken ct) =>
            Ok(await Mediator.Send(new GetAllSchoolsQuery(), ct));

        [HttpGet("{schoolId:guid}")]
        public async Task<ActionResult<SchoolMetadataDTO>> GetById(Guid schoolId, CancellationToken ct) =>
            Ok(await Mediator.Send(new GetSchoolByIdQuery { SchoolId = schoolId }, ct));

        [HttpPost]
        public async Task<ActionResult<SchoolMetadataDTO>> Create([FromBody] CreateSchoolCommand cmd, CancellationToken ct)
        {
            var dto = await Mediator.Send(cmd, ct);
            return CreatedAtAction(nameof(GetById), new { schoolId = dto.Id }, dto);
        }

        [HttpPut("{schoolId:guid}")]
        public async Task<ActionResult<SchoolMetadataDTO>> Update(Guid schoolId, [FromBody] UpdateSchoolCommand cmd, CancellationToken ct)
        {
            cmd.SchoolId = schoolId;
            var dto = await Mediator.Send(cmd, ct);
            return Ok(dto);
        }

        [HttpDelete("{schoolId:guid}")]
        public async Task<ActionResult<bool>> Delete(Guid schoolId, CancellationToken ct) =>
            Ok(await Mediator.Send(new DeleteSchoolCommand { SchoolId = schoolId }, ct));

        // Bulk operations
        [HttpPost("bulk")]
        public async Task<IActionResult> BulkCreate([FromBody] BulkCreateSchoolsCommand cmd, CancellationToken ct) =>
            Ok(await Mediator.Send(cmd, ct));

        [HttpPut("bulk")]
        public async Task<IActionResult> BulkUpdate([FromBody] BulkUpdateSchoolsCommand cmd, CancellationToken ct) =>
            Ok(await Mediator.Send(cmd, ct));

        [HttpDelete("bulk")]
        public async Task<IActionResult> BulkDelete([FromBody] BulkDeleteSchoolsCommand cmd, CancellationToken ct) =>
            Ok(await Mediator.Send(cmd, ct));

        // Custom queries
        [HttpGet("byRegion/{regionId:guid}")]
        public async Task<ActionResult<List<SchoolMetadataDTO>>> GetByRegion(Guid regionId, CancellationToken ct) =>
            Ok(await Mediator.Send(new GetSchoolsByRegionQuery { RegionId = regionId }, ct));

        [HttpGet("byCity/{cityId:guid}")]
        public async Task<ActionResult<List<SchoolMetadataDTO>>> GetByCity(Guid cityId, CancellationToken ct) =>
            Ok(await Mediator.Send(new GetSchoolsByCityQuery { CityId = cityId }, ct));

        [HttpGet("search")]
        public async Task<ActionResult<List<SchoolMetadataDTO>>> Search([FromQuery] string nameFragment, CancellationToken ct) =>
            Ok(await Mediator.Send(new SearchSchoolsQuery { NameFragment = nameFragment }, ct));
    }
}
