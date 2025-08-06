using SchoolSaas.Domain.Common.Localization;

namespace SchoolSaas.Application.Common.Exceptions
{
    public class BusinessValidationException : Exception
    {
        public BusinessValidationException()
          : base(Messages.InvalideBusinessRule)
        {
        }

        public BusinessValidationException(string message)
            : base(message)
        {
        }
    }
}
