using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Roles.Commands
{
    public class UpdateRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }
        public string NewRoleName { get; set; }
    }

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly IIdentityService _Service;

        public UpdateRoleCommandHandler(IIdentityService service)
        {
            _Service = service;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            return await _Service.UpdateRoleAsync(request.RoleId, request.NewRoleName);
        }
    }

}
