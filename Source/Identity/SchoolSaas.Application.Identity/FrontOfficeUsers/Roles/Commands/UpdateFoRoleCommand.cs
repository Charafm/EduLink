using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Roles.Commands
{
    public class UpdateFoRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }
        public string NewRoleName { get; set; }
    }

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateFoRoleCommand, bool>
    {
        private readonly IStaffIdentityService _Service;

        public UpdateRoleCommandHandler(IStaffIdentityService service)
        {
            _Service = service;
        }

        public async Task<bool> Handle(UpdateFoRoleCommand request, CancellationToken cancellationToken)
        {
            return await _Service.UpdateRoleAsync(request.RoleId, request.NewRoleName);
        }
    }

}
