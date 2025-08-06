using SchoolSaas.Domain.Common.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SchoolSaas.Infrastructure.Common.Logger
{
    [ProviderAlias("Database")]
    public class DbLoggerProvider : IDbLoggerProvider
    {
        public readonly DbLoggerOptions Options;

        public DbLoggerProvider(IOptions<DbLoggerOptions> _options)
        {
            Options = _options.Value; // Stores all the options.
        }

        /// <summary>
        /// Creates a new instance of the db logger.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(this);
        }
        public IDbLogger CreateDbLogger(string categoryName)
        {
            return new DbLogger(this);
        }

        public void Dispose()
        {
        }
    }
}
