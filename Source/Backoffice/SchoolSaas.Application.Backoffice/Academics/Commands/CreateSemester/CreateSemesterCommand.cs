using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Academic;

namespace SchoolSaas.Application.Backoffice.Academics.Commands.CreateSemester
{
    public class CreateSemesterCommand : IRequest<bool>
    {
        public SemesterDTO semester { get; set; }
    }

    public class CreateSemesterCommandHandler : IRequestHandler<CreateSemesterCommand, bool>
    {
        private readonly IAcademicsService _service;

        public CreateSemesterCommandHandler(IAcademicsService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(CreateSemesterCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateSemesterAsync(request.semester, cancellationToken);
        }
    }
}