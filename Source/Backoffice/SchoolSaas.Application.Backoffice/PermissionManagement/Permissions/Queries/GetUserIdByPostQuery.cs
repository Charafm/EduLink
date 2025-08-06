using SchoolSaas.Application.Common.Interfaces;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.Permissions.Queries
{
    
    public class GetUserIdByPostQuery : IRequest<List<Guid>>
    {
        public Guid PostID {  get; set; }
        public string PermissionName { get; set; }
    }

    public class GetUserIdByPostQueryHandler : IRequestHandler<GetUserIdByPostQuery, List<Guid>>
    {
        private readonly IIdentityConnectedService _service;

        public GetUserIdByPostQueryHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<List<Guid>> Handle(GetUserIdByPostQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetUserIdByPost(request.PostID, request.PermissionName);
        }
    }
}
