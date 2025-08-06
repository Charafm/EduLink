using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Grade;

namespace SchoolSaas.Application.Backoffice.Grades.Commands.SubmitGradeAppeal
{
    public class SubmitGradeAppealCommand : IRequest<bool>
    {
        public GradeAppealDTO DTO { get; set; }
    }

    public class SubmitGradeAppealCommandHandler : IRequestHandler<SubmitGradeAppealCommand, bool>
    {
        private readonly IGradebookService _service;

        public SubmitGradeAppealCommandHandler(IGradebookService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(SubmitGradeAppealCommand request, CancellationToken cancellationToken)
        {
            return await _service.SubmitGradeAppealAsync(request.DTO,cancellationToken);
        }
    }

}