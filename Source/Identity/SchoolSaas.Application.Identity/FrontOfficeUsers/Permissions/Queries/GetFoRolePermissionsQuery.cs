using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Permissions.Queries
{
    public class GetFoRolePermissionsQuery : IRequest<List<PermissionDto>>
    {
        public string RoleId { get; set; }
    }

    public class GetRolePermissionsQueryHandler : IRequestHandler<GetFoRolePermissionsQuery, List<PermissionDto>>
    {
        private readonly IFoPermissionService _permissionService;

        public GetRolePermissionsQueryHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<PermissionDto>> Handle(GetFoRolePermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetCitizenRolePermissionsAsync(request.RoleId);
        }
    }

}
