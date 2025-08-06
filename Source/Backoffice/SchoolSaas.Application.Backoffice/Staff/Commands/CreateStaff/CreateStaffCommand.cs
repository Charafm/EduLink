using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Staff;

namespace SchoolSaas.Application.Backoffice.Staff.Commands.CreateStaff
{
    public class CreateStaffCommand : IRequest<CreateUserRequestDto>
    {
        public CreateStaffDTO DTO { get; set; }
    }

    public class CreateStaffCommandHandler : IRequestHandler<CreateStaffCommand, CreateUserRequestDto>
    {
        private readonly IStaffService _service;

        public CreateStaffCommandHandler(IStaffService service)
        {
            _service = service;
        }

        public async Task<CreateUserRequestDto> Handle(CreateStaffCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateStaffAsync(request.DTO, cancellationToken);
        }
    }

}