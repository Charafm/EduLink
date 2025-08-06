using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice;
using MediatR;

namespace SchoolSaas.Application.Backoffice.Notifications.Commands.AddNotification
{
    public class AddNotificationCommand : IRequest<bool>
    {
        public AddNotificationRequestDTO Data { get; set; }
    }
    public class AddNotificationCommandHandler(ICommunicationService backofficeService, ICurrentUserService currentUserService) : IRequestHandler<AddNotificationCommand, bool>
    {

        private readonly ICommunicationService _backofficeService = backofficeService;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<bool> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            return await _backofficeService.CreateAndSendNotification(request.Data.UserId, request.Data., cancellationToken);
        }

    }
}



