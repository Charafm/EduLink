using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolSaas.Domain.Common.Localization;
using ValidationException = SchoolSaas.Application.Common.Exceptions.ValidationException;

namespace SchoolSaas.Application.Common.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        private readonly IStringLocalizer<Messages> _localizer;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, IStringLocalizer<Messages> localizer)
        {
            _validators = validators;
            _localizer = localizer;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults =
                    await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults
                    .SelectMany(r => r.Errors.Select(e => new ValidationFailure(e.PropertyName, _localizer.GetString(e.ErrorMessage))))
                    .Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}
