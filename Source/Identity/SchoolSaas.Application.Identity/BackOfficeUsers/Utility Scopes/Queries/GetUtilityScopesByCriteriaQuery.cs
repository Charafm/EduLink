using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Utility_Scopes.Queries
{
    public class GetUtilityScopesByCriteriaQuery : IRequest<List<UtilityScopeDto>>
    {
        public UtilityScopeSearchCriteria Criteria { get; set; }
    }

    public class GetUtilityScopesByCriteriaQueryHandler : IRequestHandler<GetUtilityScopesByCriteriaQuery, List<UtilityScopeDto>>
    {
        private readonly IPermissionService _permissionService;

        public GetUtilityScopesByCriteriaQueryHandler(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<List<UtilityScopeDto>> Handle(GetUtilityScopesByCriteriaQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetUtilityScopesByCriteriaAsync(request.Criteria);
        }
    }

}
