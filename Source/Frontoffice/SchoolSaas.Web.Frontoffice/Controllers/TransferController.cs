using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Application.Frontoffice.Transfer;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using SchoolSaas.Domain.Common.DataObjects.Transfer;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Frontoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class TransferController : ApiController
    {
        [HttpPost("SubmitTransferRequest")]
        public async Task<ActionResult<bool>> SubmitTransferRequest(TransferRequestDTO Data)
        {
            return await Mediator.Send(new SubmitTransferRequestCommand { Dto = Data });
        }

        [HttpGet("GetTransferHistory")]
        public async Task<ActionResult<PagedResult<TransferRequestHistoryDTO>>> GetTransferHistory(TransferHistoryFilterDTO filterDTO)
        {
            return  Mediator.Send(new GetTransferHistoryQuery { Filter = filterDTO }).Result.Data;
        }
        [HttpGet("GetTransferDetails")]
        public async Task<ActionResult<TransferRequestDetailDTO>> GetTransferDetails(Guid RequestId)
        {
            return await Mediator.Send(new GetTransferDetailsQuery { RequestId = RequestId });
        }
        [HttpPost("CheckTransferEligibility")]
        public async Task<ActionResult<TransferEligibilityResultDTO>> CheckTransferEligibility(Guid StudentId)
        {
            return await Mediator.Send(new CheckTransferEligibilityQuery { StudentId = StudentId });
        }
        [HttpPost("CancelTransferRequest")]
        public async Task<ActionResult<bool>> CancelTransferRequest(Guid RequestId)
        {
            return await Mediator.Send(new CancelTransferRequestCommand { RequestId = RequestId });
        }

    }
}
