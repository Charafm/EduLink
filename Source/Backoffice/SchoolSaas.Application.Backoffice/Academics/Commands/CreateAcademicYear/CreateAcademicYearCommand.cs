


using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Academic;

namespace SchoolSaas.Application.Backoffice.Academics.Commands.CreateAcademicYear
{
    public class CreateAcademicYearCommand : IRequest<bool>
    {
        public AcademicYearDTO year { get; set; }
    }

    public class CreateAcademicYearCommandHandler : IRequestHandler<CreateAcademicYearCommand, bool>
    {
        private readonly IAcademicsService _service;

        public CreateAcademicYearCommandHandler(IAcademicsService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(CreateAcademicYearCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateAcademicYearAsync(request.year, cancellationToken);
        }
    }
}

