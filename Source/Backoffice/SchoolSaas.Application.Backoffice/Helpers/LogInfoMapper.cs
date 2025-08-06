using SchoolSaas.Domain.Backoffice.BackOffice;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Application.Backoffice.Helpers
{
    public static class LogInfoMapper
    {
        public static LogInfo MapToLogInfo(LogRequestDto request)
        {
            return new LogInfo
            {
                Id = Guid.NewGuid(), 
                CallId = request.CallId != null ? Guid.Parse(request.CallId) : Guid.NewGuid(),
                RootCallId = request.RootCallId,
                Thread = request.Thread,
                Level = Enum.TryParse<LogLevelEnum>(request.Level, out var level) ? level : LogLevelEnum.Info,
                EventType = (LogEventTypeEnum)request.EventType,
                Method = request.MethodName,
                Assembly = request.AssemblyName,
                Message = request.Message,
                Exception = request.Exception
            };
        }
    }
}
