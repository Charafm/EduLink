using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.UsersManagement.Roles.Commands
{
    
    public class UpdateRoleNameCommand : IRequest<Result>
    {
        public string id { get; set; }
        public string roleName { get; set; }
    }
    public class UpdateRoleNameCommandHandler : IRequestHandler<UpdateRoleNameCommand, Result>
    {
        private readonly IIdentityConnectedService _service;

        public UpdateRoleNameCommandHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(UpdateRoleNameCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateRole(request.id, request.roleName);
        }
    }
}
