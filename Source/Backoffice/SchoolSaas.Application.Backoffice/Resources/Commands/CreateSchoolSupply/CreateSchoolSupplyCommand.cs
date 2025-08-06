using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.SchoolSupply;

namespace SchoolSaas.Application.Backoffice.Resources.Commands.CreateSchoolSupply
{
    public class CreateSchoolSupplyCommand : IRequest<bool>
    {
        public SchoolSupplyDTO DTO { get; set; }
    }

    public class CreateSchoolSupplyCommandHandler : IRequestHandler<CreateSchoolSupplyCommand, bool>
    {
        private readonly ISchoolSupplyService _service;

        public CreateSchoolSupplyCommandHandler(ISchoolSupplyService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(CreateSchoolSupplyCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateSchoolSupplyAsync(request.DTO, cancellationToken);
        }
    }


}