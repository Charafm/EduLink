using SchoolSaas.Application.Common.Exceptions;
using SchoolSaas.Domain.Common.Configuration;
using SchoolSaas.Domain.Common.Localization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SchoolSaas.Web.Common.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;
        private readonly IStringLocalizer<Messages> _localizer;
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ErrorDetailsOptions ErrorDetails;
        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger, IStringLocalizer<Messages> localizer, IHostingEnvironment hostingEnvironment, IOptions<ErrorDetailsOptions> options)
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                {typeof(ValidationException), HandleValidationException},
                {typeof(NotFoundException), HandleNotFoundException},
                {typeof(BusinessValidationException), HandleBusinessValidationException},
                {typeof(Application.Common.Exceptions.ApplicationException), HandleApplicationExceptionException}
            };
            _logger = logger;
            _localizer = localizer;
            _hostingEnvironment = hostingEnvironment;
            ErrorDetails = options.Value;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception?.Message);

            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            var type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var exception = context.Exception;

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = _localizer.GetString(Messages.ServerError),
                Type = "http://tools.ietf.org/html/rfc7231#section-6.6.1",
                Detail = ErrorDetails.IsEnabled ? exception.Message : "Erreur de serveur"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private void HandleBusinessValidationException(ExceptionContext context)
        {
            var exception = context.Exception as BusinessValidationException;

            var details = new ProblemDetails
            {
                Title = _localizer.GetString(Messages.InvalideBusinessRule),
                Detail = exception.Message
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;

            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "http://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = _localizer.GetString(Messages.OneOrMoreValidationErrorsOccurred),
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            var details = new ProblemDetails
            {
                Type = "http://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = _localizer.GetString(Messages.ResourcesNotFound),
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleApplicationExceptionException(ExceptionContext context)
        {
            var exception = context.Exception as Application.Common.Exceptions.ApplicationException;

            var details = new ProblemDetails
            {
                Type = "http://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = _localizer.GetString(Messages.InvalideBusinessRule),
                Detail = exception.Message
            };


            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }
    }
}