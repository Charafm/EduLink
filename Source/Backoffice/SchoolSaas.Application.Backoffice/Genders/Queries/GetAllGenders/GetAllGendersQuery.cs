using MediatR;
using SchoolSaas.Domain.Common.Enums;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Application.Backoffice.Genders.Queries.GetAllGenders
{
    public class GetAllGendersQuery : IRequest<List<GenderDto>>
    {
    }

    public class GetAllGendersQueryHandler : IRequestHandler<GetAllGendersQuery, List<GenderDto>>
    {
        

        public GetAllGendersQueryHandler()
        {
           
        }

        public async Task<List<GenderDto>> Handle(GetAllGendersQuery request,
    CancellationToken cancellationToken)
        {
            var genderList = Enum.GetValues(typeof(GenderEnum))
             .Cast<GenderEnum>()
             .Select(g => new GenderDto {  Title = g.ToString() })
             .ToList();

            return await Task.FromResult(genderList);
        }

    }
}
