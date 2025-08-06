using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Backoffice.Resources.Commands.AssignResourceToGrade;
using SchoolSaas.Application.Backoffice.Resources.Commands.CreateBook;
using SchoolSaas.Application.Backoffice.Resources.Commands.CreateSchoolSupply;
using SchoolSaas.Application.Backoffice.Resources.Commands.UpdateBook;
using SchoolSaas.Application.Backoffice.Resources.Commands.UpdateSchoolSupply;
using SchoolSaas.Application.Backoffice.Resources.Queries.GetPaginatedResources;
using SchoolSaas.Application.Backoffice.Resources.Queries.GetPaginatedSupplies;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Book;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using SchoolSaas.Domain.Common.DataObjects.SchoolSupply;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class ResourcesController : ApiController
    {
        [HttpPost("Supplies")]
        public async Task<ActionResult<bool>> CreateSupply([FromBody] SchoolSupplyDTO dto)
        {
            return await Mediator.Send(new CreateSchoolSupplyCommand { DTO = dto });
        }

        [HttpPut("Supplies/{id}")]
        public async Task<ActionResult<bool>> UpdateSupply(Guid id, [FromBody] SchoolSupplyDTO dto)
        {
            return await Mediator.Send(new UpdateSchoolSupplyCommand
            {
                Id = id,
                DTO = dto
            });
        }

        [HttpGet("Supplies")]
        public async Task<ActionResult<PagedResult<SchoolSupplyDTO>>> GetPaginatedSupplies(
            [FromQuery] SchoolSupplyFilterDTO filter)
        {
            return await Mediator.Send(new GetPaginatedSuppliesQuery { DTO = filter });
        }

        [HttpPost("Books")]
        public async Task<ActionResult<bool>> CreateBook([FromBody] BookDTO dto)
        {
            return await Mediator.Send(new CreateBookCommand { DTO = dto });
        }

        [HttpPut("Books/{id}")]
        public async Task<ActionResult<bool>> UpdateBook(Guid id, [FromBody] BookDTO dto)
        {
            return await Mediator.Send(new UpdateBookCommand
            {
                Id = id,
                DTO = dto
            });
        }

        [HttpPost("Assign")]
        public async Task<ActionResult<bool>> AssignToGrade([FromBody] GradeResourceDTO dto)
        {
            return await Mediator.Send(new AssignResourceToGradeCommand { DTO    = dto });
        }

        [HttpGet("GetResourcesByCriteria")]
        public async Task<ActionResult<PagedResult<GradeResourceDTO>>> GetPaginatedResources(
            [FromQuery] ResourceFilterDTO filter)
        {
            return await Mediator.Send(new GetPaginatedResourcesQuery { DTO = filter });
        }
    }
}