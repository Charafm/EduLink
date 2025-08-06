using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Roles.Commands
{
    public class DeleteFoRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteFoRoleCommand, bool>
    {
        private readonly IStaffIdentityService _Service;

        public DeleteRoleCommandHandler(IStaffIdentityService service)
        {
            _Service = service;
        }

        public async Task<bool> Handle(DeleteFoRoleCommand request, CancellationToken cancellationToken)
        {
            return await _Service.DeleteRoleAsync(request.RoleId);
        }
    }

}
