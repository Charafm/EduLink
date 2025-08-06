using MediatR;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Backoffice.Teachers.Commands.DeleteTeacher
{
    public class DeleteTeacherCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand, bool>
    {
        private readonly ITeacherService _service;

        public DeleteTeacherCommandHandler(ITeacherService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteTeacherAsync(request.Id, cancellationToken);
        }
    }
}