using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.Permissions.Queries
{
    public class GetPermissionByIdQuery : IRequest<PermissionDto>
    {
        public Guid Id { get; set; }
    }
    public class GetPermissionByIdQueryHandler : IRequestHandler<GetPermissionByIdQuery, PermissionDto>
    {
        private readonly IIdentityConnectedService _service;

        public GetPermissionByIdQueryHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<PermissionDto> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPermissionByIdAsync(request.Id);
        }
    }
}
