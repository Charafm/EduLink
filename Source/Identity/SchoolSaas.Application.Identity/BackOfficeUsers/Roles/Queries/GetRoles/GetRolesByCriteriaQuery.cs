using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.BackOfficeUsers.Roles.Queries.GetRoles
{
    public class GetRolesByCriteriaQuery : IRequest<List<RoleDto>>
    {
        public GetRoleByCriteriaDto Criteria { get; set; }
    }
    public class GetRolesByCriteriaQueryHandler : IRequestHandler<GetRolesByCriteriaQuery, List<RoleDto>>
    {
        private readonly IIdentityService _identityService;

        public GetRolesByCriteriaQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<List<RoleDto>> Handle(GetRolesByCriteriaQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.GetRolesByCriteriaAsync(request.Criteria);
        }
    }
}
