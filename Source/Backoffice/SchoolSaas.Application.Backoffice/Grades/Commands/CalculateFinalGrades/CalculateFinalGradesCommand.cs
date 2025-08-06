using MediatR;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Backoffice.Grades.Commands.CalculateFinalGrades
{
    public class CalculateFinalGradesCommand : IRequest<bool>
    {
        public Guid CourseId { get; set; }
        public Guid SemesterId { get; set; }
    }

    public class CalculateFinalGradesCommandHandler : IRequestHandler<CalculateFinalGradesCommand, bool>
    {
        private readonly IGradebookService _service;

        public CalculateFinalGradesCommandHandler(IGradebookService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(CalculateFinalGradesCommand request, CancellationToken cancellationToken)
        {
            return await _service.CalculateFinalGradesAsync(request.CourseId, request.SemesterId,cancellationToken);
        }
    }

}