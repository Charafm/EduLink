using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Roles.Queries.GetRoles
{
    public class GetFoRolesByCriteriaQuery : IRequest<List<RoleDto>>
    {
        public GetRoleByCriteriaDto Criteria { get; set; }
    }
    public class GetRolesByCriteriaQueryHandler : IRequestHandler<GetFoRolesByCriteriaQuery, List<RoleDto>>
    {
        private readonly IStaffIdentityService _identityService;

        public GetRolesByCriteriaQueryHandler(IStaffIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<List<RoleDto>> Handle(GetFoRolesByCriteriaQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.GetRolesByCriteriaAsync(request.Criteria);
        }
    }
}
