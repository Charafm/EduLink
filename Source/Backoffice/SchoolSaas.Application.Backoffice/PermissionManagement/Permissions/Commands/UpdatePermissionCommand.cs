using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.Permissions.Commands
{
    public class UpdatePermissionCommand : IRequest<Result>
    {
        public Guid id {  get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
    }
    

    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, Result>
    {
        private readonly IIdentityConnectedService _service;

        public UpdatePermissionCommandHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdatePermission(request.id, request.name, request.description);
        }
    }
}
