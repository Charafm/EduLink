namespace SchoolSaas.Application.Common.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string message)
            : base(message)
        {
        }
    }
}