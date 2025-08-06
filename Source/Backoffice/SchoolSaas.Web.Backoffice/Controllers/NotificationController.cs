using SchoolSaas.Application.Backoffice.Notifications.Commands.AddNotification;
using SchoolSaas.Application.Backoffice.Notifications.Commands.UpdateNotificationStatus;
using SchoolSaas.Application.Backoffice.Notifications.Queries.GetAllNotificationsByCriteria;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice;
using SchoolSaas.Domain.Common.Constants;
using SchoolSaas.Web.Common;
using SchoolSaas.Web.Common.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.DataObjects;

namespace SchoolSaas.Web.Backoffice.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[Controller]")]
    public class NotificationController : ApiController
    {


        [HttpPost("AddNotificationCommand")]
        public async Task<ActionResult<bool>> AddNotificationCommand([FromBody] AddNotificationRequestDTO data)
        {
            var result = await Mediator.Send(new AddNotificationCommand { Data = data });
            return result;
        }


        [HttpPut("UpdateNotificationStatus")]
        public async Task<Notification> UpdateNotificationStatus([FromBody] UpdateNotificationStatusRequestDTO data)
        {
            return await Mediator.Send(new UpdateNotificationStatusCommand { Data = data });
        }


        [HttpGet]
        public async Task<GetAllNotificationsByCriteriaResponseDTO> GetAllNotificationsByCriteria(int? page, int? size, [FromQuery] GetAllNotificationsByCriteriaRequestDTO criteria)
        {
            return await Mediator.Send(new GetAllNotificationsByCriteriaQuery { Page = page, Size = size, criteria = criteria });
        }
    }
}