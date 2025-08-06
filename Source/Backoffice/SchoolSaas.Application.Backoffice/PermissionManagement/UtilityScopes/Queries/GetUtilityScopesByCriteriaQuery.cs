using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.UtilityScopes.Queries
{
    public class GetUtilityScopesByCriteriaQuery : IRequest<List<UtilityScopeDto>>
    {
        public UtilityScopeSearchCriteria Criteria { get; set; }
    }
    public class GetUtilityScopesByCriteriaQueryHandler : IRequestHandler<GetUtilityScopesByCriteriaQuery, List<UtilityScopeDto>>
    {
        private readonly IIdentityConnectedService _service;

        public GetUtilityScopesByCriteriaQueryHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<List<UtilityScopeDto>> Handle(GetUtilityScopesByCriteriaQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetUtilityScopesByCriteriaAsync(request.Criteria);
        }

    }
}
