using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice;
using MediatR;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Application.Backoffice.Notifications.Commands.UpdateNotificationStatus
{
    public class UpdateNotificationStatusCommand : IRequest<Notification>
    {
        public UpdateNotificationStatusRequestDTO Data { get; set; }
    }
    public class UpdateNotificationStatusCommandHandler(IBackofficeService backofficeService, ICurrentUserService currentUserService) : IRequestHandler<UpdateNotificationStatusCommand, Notification>
    {

        private readonly IBackofficeService _backofficeService = backofficeService;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<Notification> Handle(UpdateNotificationStatusCommand request, CancellationToken cancellationToken)
        {
            return await _backofficeService.UpdateNotificationStatus(request.Data, cancellationToken);
        }

    }
}



