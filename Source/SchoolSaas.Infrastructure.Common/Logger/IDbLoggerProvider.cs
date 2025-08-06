using Microsoft.Extensions.Logging;

namespace SchoolSaas.Infrastructure.Common.Logger
{
    public interface IDbLoggerProvider : ILoggerProvider
    {
        IDbLogger CreateDbLogger(string categoryName);
    }
}
