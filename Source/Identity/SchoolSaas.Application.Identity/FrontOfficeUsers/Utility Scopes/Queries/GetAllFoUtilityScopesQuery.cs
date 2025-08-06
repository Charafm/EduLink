using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Utility_Scopes.Queries
{
    public class GetAllFoUtilityScopesQuery : IRequest<List<UtilityScopeDto>> { 
   
    }
    public class GetAllUtilityScopesQueryHandler : IRequestHandler<GetAllFoUtilityScopesQuery, List<UtilityScopeDto>>
    {
        private readonly IFoPermissionService _permissionService;

        public GetAllUtilityScopesQueryHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<UtilityScopeDto>> Handle(GetAllFoUtilityScopesQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetAllCitizenUtilityScopesAsync();
        }
    }
}
