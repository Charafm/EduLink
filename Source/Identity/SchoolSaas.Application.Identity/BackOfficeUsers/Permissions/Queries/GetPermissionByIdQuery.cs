using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Queries
{
    public class GetPermissionByIdQuery : IRequest<PermissionDto>
    {
        public Guid Id { get; set; }
    }

    public class GetPermissionByIdQueryHandler : IRequestHandler<GetPermissionByIdQuery, PermissionDto>
    {
        private readonly IPermissionService _permissionService;

        public GetPermissionByIdQueryHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<PermissionDto> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetPermissionByIdAsync(request.Id);
        }
    }
}
