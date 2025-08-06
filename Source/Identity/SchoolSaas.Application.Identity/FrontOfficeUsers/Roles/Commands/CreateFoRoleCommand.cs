using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Roles.Commands
{
    public class CreateFoRoleCommand : IRequest<bool>
    {
        public string RoleName { get; set; }
    }
    public class CreateRoleCommandHandler : IRequestHandler<CreateFoRoleCommand, bool>
    {
        private readonly IStaffIdentityService _identityService;

        public CreateRoleCommandHandler(IStaffIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<bool> Handle(CreateFoRoleCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.CreateRoleAsync(request.RoleName);
        }
    }
}
