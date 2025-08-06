using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.SchoolSupply;

namespace SchoolSaas.Application.Backoffice.Resources.Commands.UpdateSchoolSupply
{
    public class UpdateSchoolSupplyCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public SchoolSupplyDTO DTO { get; set; }
    }

    public class UpdateSchoolSupplyCommandHandler : IRequestHandler<UpdateSchoolSupplyCommand, bool>
    {
        private readonly ISchoolSupplyService _service;

        public UpdateSchoolSupplyCommandHandler(ISchoolSupplyService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateSchoolSupplyCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateSchoolSupplyAsync(request.Id, request.DTO, cancellationToken);
        }
    }


}