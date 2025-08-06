using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Grade;

namespace SchoolSaas.Application.Backoffice.Grades.Commands.UpdateGrade
{
    public class UpdateGradeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public GradeUpdateDTO DTO { get; set; }
    }

    public class UpdateGradeCommandHandler : IRequestHandler<UpdateGradeCommand, bool>
    {
        private readonly IGradebookService _service;

        public UpdateGradeCommandHandler(IGradebookService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateGradeAsync(request.Id, request.DTO, cancellationToken);
        }
    }

}