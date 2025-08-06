using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Parent;

namespace SchoolSaas.Application.Backoffice.Parents.Queries.GetParentById
{
    public class GetParentByIdQuery : IRequest<ParentDTO>
    {
        public  Guid Id { get; set; }
    }

    public class GetParentByIdQueryHandler : IRequestHandler<GetParentByIdQuery, ParentDTO>
    {
        private readonly IParentService _service;

        public GetParentByIdQueryHandler(IParentService service)
        {
            _service = service;
        }

        public async Task<ParentDTO> Handle(GetParentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetParentByIdAsync(request.Id, cancellationToken);
        }
    }
}