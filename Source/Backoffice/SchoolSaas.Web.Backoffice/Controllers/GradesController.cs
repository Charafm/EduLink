using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Backoffice.Grades.Commands.BulkRecordGrades;
using SchoolSaas.Application.Backoffice.Grades.Commands.RecordGrade;
using SchoolSaas.Application.Backoffice.Grades.Commands.UpdateGrade;
using SchoolSaas.Application.Backoffice.Grades.Queries.CalculateGPA;
using SchoolSaas.Application.Backoffice.Grades.Queries.GetGradeStatistics;
using SchoolSaas.Application.Backoffice.Grades.Queries.GetPaginatedGrades;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Academic;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class GradesController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<bool>> RecordGrade([FromBody] GradeDTO dto)
        {
            return await Mediator.Send(new RecordGradeCommand { DTO = dto });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateGrade(
            Guid id,
            [FromBody] GradeUpdateDTO dto)
        {
            return await Mediator.Send(new UpdateGradeCommand
            {
                Id = id,
                DTO = dto
            });
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<GradeDTO>>> GetPaginated(
            [FromQuery] GradeFilterDTO filter)
        {
            return await Mediator.Send(new GetPaginatedGradesQuery { DTO = filter });
        }

        [HttpPost("Bulk")]
        public async Task<ActionResult<bool>> BulkRecordGrades(
            [FromBody] BulkGradeDTO dto)
        {
            return await Mediator.Send(new BulkRecordGradesCommand { DTO = dto });
        }

        [HttpGet("Statistics")]
        public async Task<ActionResult<GradeStatisticsDTO>> GetStatistics(
            [FromQuery] GradeFilterDTO filter)
        {
            return await Mediator.Send(new GetGradeStatisticsQuery { DTO = filter });
        }

        [HttpGet("GPA/{studentId}")]
        public async Task<ActionResult<GpaDTO>> CalculateGPA(Guid studentId)
        {
            return await Mediator.Send(new CalculateGPAQuery { Id = studentId });
        }
    }
}