using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.Permissions.Commands
{
    public class DeletePermissionCommand : IRequest<Result>
    {
        public Guid id { get; set; }
    }
    public class DeletePemrissionCommandHandler : IRequestHandler<DeletePermissionCommand, Result>
    {
        private readonly IIdentityConnectedService _service;

        public DeletePemrissionCommandHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeletePermission(request.id);
        }
    }
}
