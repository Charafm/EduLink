using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Infrastructure.Common.Services
{
    public class ServiceHelper : IServiceHelper
    {
        private readonly IBackofficeContext _dbContext;
        public ServiceHelper(IBackofficeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> ExecuteWithTransactionAsync<T>(Func<Task<T>> operation)
        {
            await _dbContext.BeginTransactionAsync();
            try
            {
                var result = await operation();
                await _dbContext.CommitTransactionAsync();
                return result;
            }
            catch (Exception ex)
            {
                // Consider replacing Console.WriteLine with a logging framework.
                Console.WriteLine($"Error executing operation: {ex}");
                _dbContext.RollbackTransaction();
                throw;
            }
        }
    }
}
    