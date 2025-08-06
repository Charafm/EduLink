using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Staff;

namespace SchoolSaas.Application.Backoffice.Staff.Commands.UpdateStaff
{
    public class UpdateStaffCommand : IRequest<bool>
    {
        public Guid Id  { get; set; }
        public StaffDTO DTO { get; set; }
    }

    public class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommand, bool>
    {
        private readonly IStaffService _service;

        public UpdateStaffCommandHandler(IStaffService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateStaffCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateStaffAsync(request.Id, request.DTO, cancellationToken);
        }
    }

}