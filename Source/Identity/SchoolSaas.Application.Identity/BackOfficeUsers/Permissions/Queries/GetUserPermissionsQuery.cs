using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Queries
{
    public class GetUserPermissionsQuery : IRequest<List<PermissionDto>>
    {
        public string UserId { get; set; }
     
    }

    public class GetUserPermissionsQueryHandler : IRequestHandler<GetUserPermissionsQuery, List<PermissionDto>>
    {
        private readonly IPermissionService _permissionService;

        public GetUserPermissionsQueryHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<PermissionDto>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetUserPermissionsAsync(request.UserId);
        }
    }

}
