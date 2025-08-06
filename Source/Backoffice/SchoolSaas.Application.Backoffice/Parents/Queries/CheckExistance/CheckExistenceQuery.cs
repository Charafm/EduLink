using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Parent;

namespace SchoolSaas.Application.Backoffice.Parents.Queries.CheckExistance
{
    public class CheckExistanceQuery : IRequest<bool>
    {
        public string email { get; set; }
    }

    public class CheckExistanceQueryHandler : IRequestHandler<CheckExistanceQuery, bool>
    {
        private readonly IParentService _service;

        public CheckExistanceQueryHandler(IParentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(CheckExistanceQuery request, CancellationToken cancellationToken)
        {
            var filter = new ParentFilterDTO
            {
                SearchTerm = request.email,
            };
            var parent = await _service.GetPaginatedParentsAsync(filter, cancellationToken);

            if (parent != null)
            {
                return true;
            }
            else { 
                return false; 
            }
        }
    }
}
