using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.UsersManagement.Roles.Commands
{
  
    public class CreateRoleCommand : IRequest<Result>
    {
        public string RoleName { get; set; }
    }
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result>
    {
        private readonly IIdentityConnectedService _service;

        public CreateRoleCommandHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            return await _service.AddRole(request.RoleName);
        }
    }
}
