using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Book;

namespace SchoolSaas.Application.Backoffice.Resources.Commands.CreateBook
{
    public class CreateBookCommand : IRequest<bool>
    {
        public BookDTO DTO { get; set; }
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, bool>
    {
        private readonly ISchoolSupplyService _service;

        public CreateBookCommandHandler(ISchoolSupplyService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateBookAsync(request.DTO, cancellationToken);
        }
    }

}