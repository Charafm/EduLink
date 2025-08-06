using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Permissions.Queries
{
    public class GetFoPermissionsQuery : IRequest<List<PermissionDto>>
    {
        public string UserId { get; set; }
     
    }

    public class GetUserPermissionsQueryHandler : IRequestHandler<GetFoPermissionsQuery, List<PermissionDto>>
    {
        private readonly IFoPermissionService _permissionService;

        public GetUserPermissionsQueryHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<PermissionDto>> Handle(GetFoPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetCitizenUserPermissionsAsync(request.UserId);
        }
    }

}
