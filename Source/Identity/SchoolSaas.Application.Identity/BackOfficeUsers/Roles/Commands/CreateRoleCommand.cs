using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Roles.Commands
{
    public class CreateRoleCommand : IRequest<bool>
    {
        public string RoleName { get; set; }
    }
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, bool>
    {
        private readonly IIdentityService _identityService;

        public CreateRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.CreateRoleAsync(request.RoleName);
        }
    }
}
