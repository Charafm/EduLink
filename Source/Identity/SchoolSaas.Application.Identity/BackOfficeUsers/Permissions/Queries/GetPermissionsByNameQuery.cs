using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Permissions.Queries
{
    public class GetPermissionsByNameQuery : IRequest<List<PermissionDto>>
    {
        public string name { get; set; }
    }

    public class GetPermissionsByNameQueryHandler : IRequestHandler<GetPermissionsByNameQuery, List<PermissionDto>>
    {
        private readonly IPermissionService _permissionService;

        public GetPermissionsByNameQueryHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<PermissionDto>> Handle(GetPermissionsByNameQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetPermissionsByNameAsync(request.name);
        }
    }
}
