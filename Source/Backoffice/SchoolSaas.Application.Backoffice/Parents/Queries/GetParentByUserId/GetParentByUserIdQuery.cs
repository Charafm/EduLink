using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Parent;

namespace SchoolSaas.Application.Backoffice.Parents.Queries.GetParentByUserId
{
    public class GetParentByUserIdQuery : IRequest<ParentDTO>
    {
        public string userId   { get; set; }
    }

    public class GetParentByUserIdQueryHandler : IRequestHandler<GetParentByUserIdQuery, ParentDTO>
    {
        private readonly IParentService _service;

        public GetParentByUserIdQueryHandler(IParentService service)
        {
            _service = service;
        }

        public async Task<ParentDTO> Handle(GetParentByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetParentByUserId(request.userId);
        }
    }
}
