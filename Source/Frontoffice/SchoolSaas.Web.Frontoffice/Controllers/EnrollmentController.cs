using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Application.Frontoffice.Enrollment;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;
using SchoolSaas.Domain.Common.DataObjects.Transfer;
using SchoolSaas.Web.Common.Controllers;

namespace SchoolSaas.Web.Frontoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class EnrollmentController : ApiController
    {
        [HttpPost("UploadEnrollmentDocument")]
        public async Task<ActionResult<bool>> UploadEnrollmentDocument(EnrollmentDocumentUploadDTO Data)
        {
            return await Mediator.Send(new UploadEnrollmentDocumentCommand { Dto = Data });
        }
        [HttpPost("SubmitEnrollment")]
        public async Task<ActionResult<bool>> SubmitEnrollment(EnrollmentDTO Data)
        {
            return Mediator.Send(new SubmitEnrollmentCommand { Data = Data }).Result.Data;
        }

        [HttpGet("GetEnrollmentTranscript/{EnrollmentId}")]
        public async Task<ActionResult<EnrollmentTranscriptDTO>> GetEnrollmentTranscript(Guid EnrollmentId)
        {
            return await Mediator.Send(new GetEnrollmentTranscriptQuery { Id = EnrollmentId });
        }

        [HttpGet("GetEnrollment/{EnrollmentId}")]
        public async Task<ActionResult<EnrollmentDetailDTO>> GetEnrollment(Guid EnrollmentId)
        {
            return await Mediator.Send(new GetEnrollmentQuery { Id = EnrollmentId });
        }

    }
}
