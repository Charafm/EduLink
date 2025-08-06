using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
         where TRequest : notnull
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger _logger;

        public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId ?? string.Empty;
            var userName = _currentUserService.UserName ?? string.Empty;

            _logger.LogInformation("School Saas Request: {Name} {@UserId} {@UserName}",
                requestName, userId, userName);
        }
    }
}