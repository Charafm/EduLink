using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Utility_Scopes.Queries
{
    public class GetFoUtilityScopesByCriteriaQuery : IRequest<List<UtilityScopeDto>>
    {
        public UtilityScopeSearchCriteria Criteria { get; set; }
    }

    public class GetUtilityScopesByCriteriaQueryHandler : IRequestHandler<GetFoUtilityScopesByCriteriaQuery, List<UtilityScopeDto>>
    {
        private readonly IFoPermissionService _permissionService;

        public GetUtilityScopesByCriteriaQueryHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<UtilityScopeDto>> Handle(GetFoUtilityScopesByCriteriaQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetCitizenUtilityScopesByCriteriaAsync(request.Criteria);
        }
    }

}
