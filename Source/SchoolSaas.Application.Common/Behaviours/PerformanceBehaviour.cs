using MediatR;
using Microsoft.Extensions.Logging;
using SchoolSaas.Application.Common.Interfaces;
using System.Diagnostics;

namespace SchoolSaas.Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<TRequest> _logger;
        private readonly Stopwatch _timer;

        public PerformanceBehaviour(
            ILogger<TRequest> logger,
            ICurrentUserService currentUserService)
        {
            _timer = new Stopwatch();
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;
                var userId = _currentUserService.UserId ?? string.Empty;
                var userName = _currentUserService.UserName ?? string.Empty;

                _logger.LogWarning(
                    "School Saas Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName}",
                    requestName, elapsedMilliseconds, userId, userName);
            }

            return response;
        }
    }
}
