using MediatR;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Backoffice.Staff.Commands.DeleteStaff
{
    public class DeleteStaffCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteStaffCommandHandler : IRequestHandler<DeleteStaffCommand, bool>
    {
        private readonly IStaffService _service;

        public DeleteStaffCommandHandler(IStaffService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteStaffCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteStaffAsync(request.Id , cancellationToken);
        }
    }

}