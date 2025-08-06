using MediatR.Behaviors.Authorization;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Common.Authorizations
{
    public class QueryCommandBasedAuthorizer<TRequest>
     : AbstractRequestAuthorizer<TRequest>
    {

        private readonly ICurrentUserService _currentUserService;

        public QueryCommandBasedAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override void BuildPolicy(TRequest request)
        {

        }



    }
}