using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.UsersManagement.Roles.Commands
{

    public class DeleteRoleCommand : IRequest<Result>
    {
        public string id { get; set; }
    }
    public class DeletePemrissionCommandHandler : IRequestHandler<DeleteRoleCommand, Result>
    {
        private readonly IIdentityConnectedService _service;

        public DeletePemrissionCommandHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteRole(request.id);
        }
    }
}
