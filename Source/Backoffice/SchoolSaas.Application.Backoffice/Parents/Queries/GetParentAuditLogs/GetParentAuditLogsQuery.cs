using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Parent;

namespace SchoolSaas.Application.Backoffice.Parents.Queries.GetParentAuditLogs
{
    public class GetParentAuditLogsQuery : IRequest<List<ParentAuditDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetParentAuditLogsQueryHandler : IRequestHandler<GetParentAuditLogsQuery, List<ParentAuditDTO>>
    {
        private readonly IParentService _service;

        public GetParentAuditLogsQueryHandler(IParentService service)
        {
            _service = service;
        }

        public async Task<List<ParentAuditDTO>> Handle(GetParentAuditLogsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetParentAuditLogsAsync(request.Id,cancellationToken);
        }
    }
}