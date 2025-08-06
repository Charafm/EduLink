using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Staff;

namespace SchoolSaas.Application.Backoffice.Staff.Queries.GetStaffByUserId
{
    public class GetStaffByUserIdQuery : IRequest<StaffDTO>
    {
        public string userId { get; set; }
    }

    public class GetStaffByUserIdQueryHandler : IRequestHandler<GetStaffByUserIdQuery, StaffDTO>
    {
        private readonly IStaffService _service;

        public GetStaffByUserIdQueryHandler(IStaffService service)
        {
            _service = service;
        }

        public async Task<StaffDTO> Handle(GetStaffByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetStaffByUserId(request.userId);
        }
    }
}
