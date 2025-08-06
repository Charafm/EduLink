using SchoolSaas.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Common.Services
{
    public class EdulinkServiceHelper : IEdulinkServiceHelper
    {
        private readonly IEdulinkContext _dbContext;

        public EdulinkServiceHelper(IEdulinkContext dbContext)
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
