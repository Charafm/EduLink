using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Academic;

namespace SchoolSaas.Application.Backoffice.Academics.Commands.UpdateAcademicYear
{
    public class UpdateAcademicYearCommand : IRequest<bool>
    {
        public Guid academicYearId { get; set; }
        public AcademicYearDTO data { get; set; }
    }

    public class UpdateAcademicYearCommandHandler : IRequestHandler<UpdateAcademicYearCommand, bool>
    {
        private readonly IAcademicsService _service;

        public UpdateAcademicYearCommandHandler(IAcademicsService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateAcademicYearCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateAcademicYearAsync(request.academicYearId, request.data, cancellationToken);
        }
    }
}