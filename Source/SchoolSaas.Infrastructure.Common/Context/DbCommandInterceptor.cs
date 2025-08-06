using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace SchoolSaas.Infrastructure.Common.Context
{
    public class CommandInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            Console.WriteLine(command.CommandText);

            return base.ReaderExecuting(command, eventData, result);
        }
    }
}
