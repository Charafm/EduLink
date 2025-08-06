using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Queries
{
    public class GetRolePermissionsQuery : IRequest<List<PermissionDto>>
    {
        public string RoleId { get; set; }
    }

    public class GetRolePermissionsQueryHandler : IRequestHandler<GetRolePermissionsQuery, List<PermissionDto>>
    {
        private readonly IPermissionService _permissionService;

        public GetRolePermissionsQueryHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<PermissionDto>> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetRolePermissionsAsync(request.RoleId);
        }
    }

}
