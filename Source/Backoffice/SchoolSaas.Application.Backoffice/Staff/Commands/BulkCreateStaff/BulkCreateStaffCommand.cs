using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Staff;

namespace SchoolSaas.Application.Backoffice.Staff.Commands.BulkCreateStaff
{
    public class BulkCreateStaffCommand : IRequest<bool>
    {
        public BulkStaffDTO DTO { get; set; }
    }

    public class BulkCreateStaffCommandHandler : IRequestHandler<BulkCreateStaffCommand, bool>
    {
        private readonly IStaffService _service;

        public BulkCreateStaffCommandHandler(IStaffService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(BulkCreateStaffCommand request, CancellationToken cancellationToken)
        {
            return await _service.BulkCreateStaffAsync(request.DTO, cancellationToken);
        }
    }

}