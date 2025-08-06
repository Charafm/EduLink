using Microsoft.Extensions.Logging;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Infrastructure.Common.Logger
{
    public interface IDbLogger : ILogger
    {
        void LogToDatabase(LogRequestDto logInfo);
    }

   
}
