using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Academic;

namespace SchoolSaas.Application.Backoffice.Grades.Queries.CalculateGPA
{
    public class CalculateGPAQuery : IRequest<GpaDTO>
    {
        public Guid Id { get; set; }
    }

    public class CalculateGPAQueryHandler : IRequestHandler<CalculateGPAQuery, GpaDTO>
    {
        private readonly IGradebookService _service;

        public CalculateGPAQueryHandler(IGradebookService service)
        {
            _service = service;
        }

        public async Task<GpaDTO> Handle(CalculateGPAQuery request, CancellationToken cancellationToken)
        {
            return await _service.CalculateGPAForStudentAsync(request.Id,cancellationToken);
        }
    }
}