using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Commands.ToggleUserStatus
{
    public class ToggleFoUserStatusCommand : IRequest<Unit>
    {
        public string Id { get; set; }

        public bool IsActive { get; set; }
    }

    public class ToggleUserStatusCommandHandler : IRequestHandler<ToggleFoUserStatusCommand, Unit>
    {
        private readonly IStaffIdentityService _identityService;

        public ToggleUserStatusCommandHandler(IStaffIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Unit> Handle(ToggleFoUserStatusCommand request,
            CancellationToken cancellationToken)
        {
            return await _identityService.ToggleUserStatusAsync(request.Id, request.IsActive);
        }
    }
}