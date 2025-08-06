using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Staff;

namespace SchoolSaas.Application.Backoffice.Staff.Queries.GetStaffById
{
    public class GetStaffByIdQuery : IRequest<StaffDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetStaffByIdQueryHandler : IRequestHandler<GetStaffByIdQuery, StaffDTO>
    {
        private readonly IStaffService _service;

        public GetStaffByIdQueryHandler(IStaffService service)
        {
            _service = service;
        }

        public async Task<StaffDTO> Handle(GetStaffByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetStaffByIdAsync(request.Id, cancellationToken);
        }
    }
}