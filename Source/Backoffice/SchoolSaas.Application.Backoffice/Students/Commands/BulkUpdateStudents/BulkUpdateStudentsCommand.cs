using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Application.Backoffice.Students.Commands.BulkUpdateStudents
{
    public class BulkUpdateStudentsCommand : IRequest<bool>
    {
        public BulkStudentUpdateDTO DTO { get; set; }
    }

    public class BulkUpdateStudentsCommandHandler : IRequestHandler<BulkUpdateStudentsCommand, bool>
    {
        private readonly IStudentService _service;

        public BulkUpdateStudentsCommandHandler(IStudentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(BulkUpdateStudentsCommand request, CancellationToken cancellationToken)
        {
            return await _service.BulkUpdateStudentsAsync(request.DTO , cancellationToken);
        }
    }

}