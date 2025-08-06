using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Backoffice.Transfer.Commands.CancelTransferRequest;
using SchoolSaas.Application.Backoffice.Transfer.Commands.SubmitTransferRequest;
using SchoolSaas.Application.Backoffice.Transfer.Commands.UpdateTransferRequest;
using SchoolSaas.Application.Backoffice.Transfer.Commands.UpdateTransferStatus;
using SchoolSaas.Application.Backoffice.Transfer.Queries.CheckTransferEligibility;
using SchoolSaas.Application.Backoffice.Transfer.Queries.GetPaginatedTransferRequests;
using SchoolSaas.Application.Backoffice.Transfer.Queries.GetTransferHistory;
using SchoolSaas.Application.Backoffice.Transfer.Queries.GetTransferRequestDetails;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Transfer;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class TransferController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<bool>> SubmitRequest([FromBody] TransferRequestDTO dto)
        {
            return await Mediator.Send(new SubmitTransferRequestCommand { DTO = dto });
        }

        [HttpPut("{id}/Status")]
        public async Task<ActionResult<bool>> UpdateStatus(
            Guid id,
            [FromBody] TransferStatusUpdateDTO dto)
        {
            return await Mediator.Send(new UpdateTransferStatusCommand
            {
                Id = id,
                DTO = dto
            });
        }
        // GET /Transfer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TransferRequestDetailDTO>> GetDetails(Guid id)
            => await Mediator.Send(new GetTransferRequestDetailsQuery { RequestId = id });

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateRequest(
        Guid id,
        [FromBody] TransferRequestUpdateDTO dto)
        => await Mediator.Send(new UpdateTransferRequestCommand
        {
            TransferId = id,
            TransferRequest = dto
        });
        [HttpPost("{id}/Cancel")]
        public async Task<ActionResult<bool>> CancelRequest(Guid id)
                     => await Mediator.Send(new CancelTransferRequestCommand { RequestId = id });
        [HttpGet("Eligibility/{studentId}")]
        public async Task<ActionResult<TransferEligibilityResultDTO>> CheckEligibility(Guid studentId)
        {
            return await Mediator.Send(new CheckTransferEligibilityQuery { Id = studentId });
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<TransferRequestDTO>>> GetPaginated(
            [FromQuery] TransferRequestFilterDTO filter)
        {
            return await Mediator.Send(new GetPaginatedTransferRequestsQuery { DTO = filter });
        }

        [HttpGet("History")]
        public async Task<ActionResult<PagedResult<TransferRequestHistoryDTO>>> GetHistory(
            [FromQuery] TransferHistoryFilterDTO filter)
        {
            return await Mediator.Send(new GetTransferHistoryQuery { DTO = filter });
        }
    }
}