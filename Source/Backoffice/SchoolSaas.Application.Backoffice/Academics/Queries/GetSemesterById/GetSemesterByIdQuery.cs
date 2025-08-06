using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Academic;

namespace SchoolSaas.Application.Backoffice.Academics.Queries.GetSemesterById
{
    public class GetSemesterByIdQuery : IRequest<SemesterDTO>
    {
        public Guid semesterId { get; set; }
    }

    public class GetSemesterByIdQueryHandler : IRequestHandler<GetSemesterByIdQuery, SemesterDTO>
    {
        private readonly IAcademicsService _service;

        public GetSemesterByIdQueryHandler(IAcademicsService service)
        {
            _service = service;
        }

        public async Task<SemesterDTO> Handle(GetSemesterByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetSemesterByIdAsync(request.semesterId, cancellationToken);
        }
    }
}
