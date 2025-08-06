using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Book;

namespace SchoolSaas.Application.Backoffice.Resources.Commands.UpdateBook
{
    public class UpdateBookCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public BookDTO DTO { get; set; }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly ISchoolSupplyService _service;

        public UpdateBookCommandHandler(ISchoolSupplyService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateBookAsync(request.Id, request.DTO, cancellationToken);
        }
    }


}