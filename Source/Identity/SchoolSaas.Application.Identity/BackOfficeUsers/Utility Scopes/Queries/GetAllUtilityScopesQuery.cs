using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Utility_Scopes.Queries
{
    public class GetAllUtilityScopesQuery : IRequest<List<UtilityScopeDto>> { 
   
    }
    public class GetAllUtilityScopesQueryHandler : IRequestHandler<GetAllUtilityScopesQuery, List<UtilityScopeDto>>
    {
        private readonly IPermissionService _permissionService;

        public GetAllUtilityScopesQueryHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<UtilityScopeDto>> Handle(GetAllUtilityScopesQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetAllUtilityScopesAsync();
        }
    }
}
