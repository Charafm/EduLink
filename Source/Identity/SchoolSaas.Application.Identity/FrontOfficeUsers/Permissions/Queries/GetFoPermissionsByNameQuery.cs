using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Permissions.Queries
{
    public class GetFoPermissionsByNameQuery : IRequest<List<PermissionDto>>
    {
        public string name { get; set; }
    }

    public class GetPermissionsByNameQueryHandler : IRequestHandler<GetFoPermissionsByNameQuery, List<PermissionDto>>
    {
        private readonly IFoPermissionService _permissionService;

        public GetPermissionsByNameQueryHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<PermissionDto>> Handle(GetFoPermissionsByNameQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetCitizenPermissionsByNameAsync(request.name);
        }
    }
}
