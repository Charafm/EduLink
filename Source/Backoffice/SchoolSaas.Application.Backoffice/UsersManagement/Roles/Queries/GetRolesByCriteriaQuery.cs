using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.UsersManagement.Roles.Queries
{
    public class GetRolesByCriteriaQuery : IRequest<List<RoleDto>>
    {
        public GetRoleByCriteriaDto criteria { get; set; }
    }
    public class GetRolesByCriteriaQueryHandler : IRequestHandler<GetRolesByCriteriaQuery, List<RoleDto>>
    {
        private readonly IIdentityConnectedService _identityService;

        public GetRolesByCriteriaQueryHandler(IIdentityConnectedService identityService)
        {
            _identityService = identityService;
        }

        public async Task<List<RoleDto>> Handle(GetRolesByCriteriaQuery request, CancellationToken cancellationToken)
        {
            return await _identityService.GetRoleByCriteria(request.criteria);
        }
    }
}
