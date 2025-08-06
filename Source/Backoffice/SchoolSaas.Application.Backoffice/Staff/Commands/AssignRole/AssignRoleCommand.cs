using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Staff;

namespace SchoolSaas.Application.Backoffice.Staff.Commands.AssignRole
{
    public class AssignRoleCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public StaffRoleDTO DTO { get; set; }
    }

    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, bool>
    {
        private readonly IStaffService _service;

        public AssignRoleCommandHandler(IStaffService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            return await _service.AssignRoleAsync(request.Id, request.DTO, cancellationToken);  
        }
    }

}