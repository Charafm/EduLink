using SchoolSaas.Application.Common.Interfaces;
using MediatR;
using SchoolSaas.Domain.Backoffice.BackOffice;
using SchoolSaas.Application.Backoffice.Helpers;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Application.Backoffice.Logging.Commands
{
    public class LogToDatabaseCommand : IRequest<bool>
    {
        public LogRequestDto LogInfo { get; set; }
    }
    public class LogToDatabaseCommandHandler(ILoggingContext LoggingContext) : IRequestHandler<LogToDatabaseCommand, bool>
    {
        private readonly ILoggingContext _dbContext = LoggingContext;
        public async Task<bool> Handle(LogToDatabaseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _dbContext.Set<LogInfo>().AddAsync(LogInfoMapper.MapToLogInfo(request.LogInfo));
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
   
}
