using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Grade;

namespace SchoolSaas.Application.Backoffice.Resources.Commands.AssignResourceToGrade
{
    public class AssignResourceToGradeCommand : IRequest<bool>
    {
        public GradeResourceDTO DTO { get; set; }
    }

    public class AssignResourceToGradeCommandHandler : IRequestHandler<AssignResourceToGradeCommand, bool>
    {
        private readonly ISchoolSupplyService _service;

        public AssignResourceToGradeCommandHandler(ISchoolSupplyService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(AssignResourceToGradeCommand request, CancellationToken cancellationToken)
        {
            return await _service.AssignResourceToGradeAsync(request.DTO,cancellationToken);
        }
    }

}