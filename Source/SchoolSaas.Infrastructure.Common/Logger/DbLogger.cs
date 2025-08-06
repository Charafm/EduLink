using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Infrastructure.Common.Logger
{
    public class DbLogger : IDbLogger
    {
        /// <summary>
        /// Instance of <see cref="DbLoggerProvider" />.
        /// </summary>
        private readonly DbLoggerProvider _dbLoggerProvider;

        public DbLogger([NotNull] DbLoggerProvider dbLoggerProvider)
        {
            _dbLoggerProvider = dbLoggerProvider;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        /// Whether to log the entry.
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }


        /// <summary>
        /// Used to log the entry.
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel">An instance of <see cref="LogLevel"/>.</param>
        /// <param name="eventId">The event's ID. An instance of <see cref="EventId"/>.</param>
        /// <param name="state">The event's state.</param>
        /// <param name="exception">The event's exception. An instance of <see cref="Exception" /></param>
        /// <param name="formatter">A delegate that formats </param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                // Don't log the entry if it's not enabled.
                return;
            }

            var thread = Thread.CurrentThread.Name; // Get the current thread name to use in the log file. 
            var AssemblyName = Assembly.GetExecutingAssembly().FullName;
            var MethodName = new StackFrame(0, false).GetMethod().Name;
            // Store record.
            using (var connection = new SqlConnection(_dbLoggerProvider.Options.ConnectionString))
            {
                connection.Open();
                var values = new JObject();

                if (_dbLoggerProvider?.Options?.LogFields?.Any() ?? false)
                {
                    foreach (var logField in _dbLoggerProvider.Options.LogFields)
                    {
                        switch (logField)
                        {
                            case "Level":
                                if (!string.IsNullOrWhiteSpace(logLevel.ToString()))
                                {
                                    values["Level"] = logLevel.ToString();
                                }
                                break;
                            case "Thread":
                                values["Thread"] = thread;
                                break;
                            case "EventId":
                                values["EventId"] = eventId.Id;
                                break;
                            case "EventName":
                                values["EventName"] = eventId.Name;
                                break;
                            case "Message":
                                if (!string.IsNullOrWhiteSpace(formatter(state, exception)))
                                {
                                    values["Message"] = formatter(state, exception);
                                }
                                break;
                            case "Exception":
                                if (exception != null && !string.IsNullOrWhiteSpace(exception.Message))
                                {
                                    values["Exception"] = exception.ToString();
                                }
                                else
                                {
                                    values["Exception"] = string.Empty;
                                }
                                break;
                            case "Method":
                                values["Method"] = MethodName;
                                break;
                            case "Assembly":
                                values["Assembly"] = AssemblyName;
                                break;
                        }
                    }
                }
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = string.Format("INSERT INTO {0} ([Id],[CallId],[Thread],[Level],[EventId],[EventName],[Method],[Assembly],[Message],[Exception],[Created]) VALUES (@Id,@CallId,@Thread,@Level,@EventId,@EventName,@Method,@Assembly,@Message,@Exception,@Created)", _dbLoggerProvider.Options.LogTable);

                    //command.Parameters.Add(new SqlParameter("@Values", JsonConvert.SerializeObject(values, new JsonSerializerSettings
                    //{
                    //    NullValueHandling = NullValueHandling.Ignore,
                    //    DefaultValueHandling = DefaultValueHandling.Ignore,
                    //    Formatting = Formatting.None
                    //}).ToString()));
                    command.Parameters.Add(new SqlParameter("@Id", Guid.NewGuid()));
                    command.Parameters.Add(new SqlParameter("@CallId", Guid.NewGuid()));
                    command.Parameters.Add(new SqlParameter("@Thread", values["Thread"].ToString()));
                    command.Parameters.Add(new SqlParameter("@Level", values["Level"].ToString()));
                    command.Parameters.Add(new SqlParameter("@EventId", values["EventId"].ToString()));
                    command.Parameters.Add(new SqlParameter("@EventName", values["EventName"].ToString()));
                    command.Parameters.Add(new SqlParameter("@Method", values["Method"].ToString()));
                    command.Parameters.Add(new SqlParameter("@Assembly", values["Assembly"].ToString()));
                    command.Parameters.Add(new SqlParameter("@Message", values["Message"].ToString()));
                    command.Parameters.Add(new SqlParameter("@Exception", values["Exception"].ToString()));
                    command.Parameters.Add(new SqlParameter("@Created", DateTimeOffset.Now));

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void LogToDatabase(LogRequestDto logInfo)
        {
            using (var connection = new SqlConnection(_dbLoggerProvider.Options.ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = string.Format("INSERT INTO {0} ([Id],[CallId],[Thread],[Level],[EventId],[EventName],[Method],[Assembly],[Message],[Exception],[RootCallId],[Created]) VALUES (@Id,@CallId,@Thread,@Level,@EventId,@EventName,@Method,@Assembly,@Message,@Exception,@RootCallId,@Created)", _dbLoggerProvider.Options.LogTable);

                    //command.Parameters.Add(new SqlParameter("@Values", JsonConvert.SerializeObject(values, new JsonSerializerSettings
                    //{
                    //    NullValueHandling = NullValueHandling.Ignore,
                    //    DefaultValueHandling = DefaultValueHandling.Ignore,
                    //    Formatting = Formatting.None
                    //}).ToString()));
                    command.Parameters.Add(new SqlParameter("@Id", Guid.NewGuid()));
                    command.Parameters.Add(new SqlParameter("@CallId", logInfo.CallId));
                    command.Parameters.Add(new SqlParameter("@Thread", logInfo.Thread));
                    command.Parameters.Add(new SqlParameter("@Level", logInfo.Level.ToString()));
                    command.Parameters.Add(new SqlParameter("@EventId", (int)logInfo.EventType));
                    command.Parameters.Add(new SqlParameter("@EventName", logInfo.EventType.ToString()));
                    command.Parameters.Add(new SqlParameter("@Method", logInfo.MethodName));
                    command.Parameters.Add(new SqlParameter("@Assembly", logInfo.AssemblyName));
                    command.Parameters.Add(new SqlParameter("@Message", logInfo.Message ?? string.Empty));
                    command.Parameters.Add(new SqlParameter("@Exception", logInfo.Exception ?? string.Empty));
                    command.Parameters.Add(new SqlParameter("@RootCallId", logInfo.RootCallId ?? Guid.Empty));
                    command.Parameters.Add(new SqlParameter("@Created", DateTimeOffset.Now));

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }

}
