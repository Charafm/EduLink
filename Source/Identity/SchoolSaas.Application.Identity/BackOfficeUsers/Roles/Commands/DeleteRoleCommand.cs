using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Roles.Commands
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public string RoleId { get; set; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IIdentityService _Service;

        public DeleteRoleCommandHandler(IIdentityService service)
        {
            _Service = service;
        }

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _Service.DeleteRoleAsync(request.RoleId);
        }
    }

}
