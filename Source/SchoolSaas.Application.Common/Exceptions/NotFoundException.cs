namespace SchoolSaas.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NotFoundException(string name, object key)
            : base($"Resource '{name}' ({key}) was not found.")
        {
        }

        public NotFoundException(object? name, string? key)
            : base($"Resource '{name}' ({key}) was not found.")
        {
        }

        public static void ThrowIfNull(object? argument, string? paramName = null)
        {
            if (argument == null)
            {
                throw new NotFoundException(argument, paramName);
            }
        }
    }
}