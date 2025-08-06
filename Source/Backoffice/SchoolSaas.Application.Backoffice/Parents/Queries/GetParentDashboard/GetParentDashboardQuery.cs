using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Parent;

namespace SchoolSaas.Application.Backoffice.Parents.Queries.GetParentDashboard
{
    public class GetParentDashboardQuery : IRequest<ParentDashboardDTO>
    {
        public Guid ParentId { get; set; }
    }
    public class GetParentDashboardQueryHandler
    : IRequestHandler<GetParentDashboardQuery, ParentDashboardDTO>
    {
        private readonly IUserProfileService _service;
        public GetParentDashboardQueryHandler(IUserProfileService service) => _service = service;

        public Task<ParentDashboardDTO> Handle(GetParentDashboardQuery req, CancellationToken ct) =>
            _service.GetParentDashboardAsync(req.ParentId, ct);
    }
}
