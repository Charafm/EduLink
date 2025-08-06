using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Users.Commands.ToggleUserStatus
{
    public class ToggleUserStatusCommand : IRequest<Unit>
    {
        public string Id { get; set; }

        public bool IsActive { get; set; }
    }

    public class ToggleUserStatusCommandHandler : IRequestHandler<ToggleUserStatusCommand, Unit>
    {
        private readonly IIdentityService _identityService;

        public ToggleUserStatusCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Unit> Handle(ToggleUserStatusCommand request,
            CancellationToken cancellationToken)
        {
            return await _identityService.ToggleUserStatusAsync(request.Id, request.IsActive);
        }
    }
}