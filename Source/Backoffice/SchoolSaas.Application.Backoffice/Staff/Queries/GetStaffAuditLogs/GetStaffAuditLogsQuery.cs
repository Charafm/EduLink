using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Staff;

namespace SchoolSaas.Application.Backoffice.Staff.Queries.GetStaffAuditLogs
{
    public class GetStaffAuditLogsQuery : IRequest<List<StaffAuditDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetStaffAuditLogsQueryHandler : IRequestHandler<GetStaffAuditLogsQuery, List<StaffAuditDTO>>
    {
        private readonly IStaffService _service;

        public GetStaffAuditLogsQueryHandler(IStaffService service)
        {
            _service = service;
        }

        public async Task<List<StaffAuditDTO>> Handle(GetStaffAuditLogsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetStaffAuditLogsAsync(request.Id, cancellationToken);    
        }
    }
}