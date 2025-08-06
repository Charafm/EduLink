using SchoolSaas.Domain.Common.Localization;
using FluentValidation.Results;

namespace SchoolSaas.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base(Messages.validationFailuresHaveOccurred)
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            var failureGroups = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                Errors.Add(propertyName.Replace("Data.", ""), propertyFailures);
            }
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}