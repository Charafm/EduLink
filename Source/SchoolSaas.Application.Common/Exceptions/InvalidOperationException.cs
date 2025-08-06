using SchoolSaas.Domain.Common.Localization;

namespace SchoolSaas.Application.Common.Exceptions
{
    public class InvalidOperationException : Exception
    {
        public InvalidOperationException()
            : base(Messages.InvalidOperation)
        {
        }

        public InvalidOperationException(string message)
            : base(message)
        {
        }

        public InvalidOperationException(string resource, object operation)
            : base($"Operation '{operation}' on resource ({resource}) is not valid.")
        {
        }
    }
}