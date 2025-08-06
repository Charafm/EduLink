using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Permissions.Queries
{
    public class GetFoPermissionByIdQuery : IRequest<PermissionDto>
    {
        public Guid Id { get; set; }
    }

    public class GetPermissionByIdQueryHandler : IRequestHandler<GetFoPermissionByIdQuery, PermissionDto>
    {
        private readonly IFoPermissionService _permissionService;

        public GetPermissionByIdQueryHandler(IFoPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<PermissionDto> Handle(GetFoPermissionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _permissionService.GetCitizenPermissionByIdAsync(request.Id);
        }
    }
}
