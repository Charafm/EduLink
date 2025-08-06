using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Backoffice.Enrollment.Commands.SubmitEnrollment;
using SchoolSaas.Application.Backoffice.Enrollment.Commands.UpdateEnrollmentStatus;
using SchoolSaas.Application.Backoffice.Enrollment.Queries.GetEnrollment;
using SchoolSaas.Application.Backoffice.Enrollment.Queries.GetEnrollmentDashboard;
using SchoolSaas.Application.Backoffice.Enrollment.Queries.GetPaginatedEnrollments;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class EnrollmentController : ApiController
    {
        private readonly IEnrollmentService _Service;

        public EnrollmentController(IEnrollmentService service)
        {
            _Service = service;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> SubmitEnrollment([FromBody] EnrollmentDTO dto)
        {
            return await Mediator.Send(new SubmitEnrollmentCommand { DTO = dto });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentDetailDTO>> GetEnrollment(Guid id)
        {
            return await Mediator.Send(new GetEnrollmentQuery { Id = id });
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<EnrollmentDTO>>> GetPaginated(
            [FromQuery] EnrollmentFilterDTO filter)
        {
            return await Mediator.Send(new GetPaginatedEnrollmentsQuery { DTO = filter });
        }

        [HttpPut("Status/{id}")]
        public async Task<ActionResult<bool>> UpdateStatus(
            Guid id,
            [FromBody] EnrollmentStatusUpdateDTO dto)
        {
            return await Mediator.Send(new UpdateEnrollmentStatusCommand
            {
                Id = id,
                DTO = dto
            });
        }
        [HttpPost("CreateRequest")]
        public async Task<ActionResult<Guid>> CreateRequest(
           
           [FromBody] CreateEnrollmentRequestDTO dto)
        {
            CancellationToken cancellationToken = default(CancellationToken);
            return await _Service.CreateEnrollmentRequestAsync(dto, cancellationToken);
        }
        [HttpGet("GetEnrollmentRequests")]
        public async Task<ActionResult<PagedResult<EnrollmentRequestDTO>>> GetEnrollmentRequests(
         [FromQuery] EnrollmentRequestFilterDTO filter )
        {
            CancellationToken cancellationToken= default(CancellationToken);
            return await _Service.GetEnrollmentRequestsAsync(filter, cancellationToken);
        }
        [HttpGet("GetEnrollmentRequestById/{Id}")]
        public async Task<ActionResult<EnrollmentRequestDetailDTO>> GetEnrollmentRequestById(
        [FromBody] Guid Id)
        {
            CancellationToken cancellationToken = default(CancellationToken);
            return await _Service.GetEnrollmentRequestByIdAsync(Id, cancellationToken);
        }

        [HttpPut("UpdateRequest/{requestId}")]
        public async Task<ActionResult<bool>> UpdateRequest(
    [FromRoute] Guid requestId,
    [FromBody] UpdateEnrollmentRequestDTO dto)
        {
            CancellationToken cancellationToken = default;
            return await _Service.UpdateEnrollmentRequestAsync(requestId, dto, cancellationToken);
        }
        [HttpPost("SubmitEnrollmentRequest")]
        public async Task<ActionResult<bool>> SubmitEnrollmentRequest(

         [FromBody] Guid requestId)
        {
            CancellationToken cancellationToken = default(CancellationToken);
            return await _Service.SubmitEnrollmentRequestAsync(requestId, cancellationToken);
        }
        [HttpPost("ApproveEnrollmentRequest")]
        public async Task<ActionResult<bool>> ApproveEnrollmentRequest(

        [FromBody] Guid requestId, Guid adminUserId)
        {
            CancellationToken cancellationToken = default(CancellationToken);
            return await _Service.ApproveEnrollmentRequestAsync(requestId, adminUserId, cancellationToken);
        }
        [HttpPost("BULKApproveEnrollmentRequest")]
        public async Task<ActionResult<bool>> BulkApproveEnrollmentRequest(

      [FromBody] List<Guid> requestIds, Guid adminUserId)
        {
            CancellationToken cancellationToken = default(CancellationToken);
            return await _Service.BulkApproveEnrollmentRequestsAsync(requestIds, adminUserId, cancellationToken);
        }

        [HttpPost("SaveRequest")]
        public async Task<ActionResult<bool>> SaveRequest(

        [FromBody] EnrollmentRequestDTO request)
        {
            CancellationToken cancellationToken = default(CancellationToken);
            return await _Service.SaveRequest(request, cancellationToken);
        }
        //[HttpPost("Documents")]
        //public async Task<ActionResult<bool>> BulkUploadDocuments(
        //    [FromBody] BulkEnrollmentDocumentsDTO dto)
        //{
        //    return await Mediator.Send(new BulkUploadDocumentsCommand { DTO = dto });
        //}

        [HttpGet("Dashboard")]
        public async Task<ActionResult<EnrollmentMetricsDTO>> GetDashboardMetrics(
            [FromQuery] DateRangeDTO dateRange)
        {
            return await Mediator.Send(new GetEnrollmentDashboardQuery { DTO = dateRange });
        }
    }
}