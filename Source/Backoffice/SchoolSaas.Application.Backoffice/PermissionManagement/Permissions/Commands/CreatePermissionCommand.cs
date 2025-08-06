using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.Permissions.Commands
{
    public class CreatePermissionCommand : IRequest<Result>
    {
        public CreatePermissionDto PermissionDto { get; set; }
    }
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, Result>
    {
        private readonly IIdentityConnectedService _service;

        public CreatePermissionCommandHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreatePermissionAsync(request.PermissionDto);
        }
    }
}
