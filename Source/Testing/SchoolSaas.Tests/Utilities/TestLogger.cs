using Serilog;

namespace SchoolSaas.Tests.Utilities
{
    public static class TestLogger
    {
        private static readonly string LogFilePath;
        private static readonly ILogger Logger;

        static TestLogger()
        {
            string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestLogs");
            Directory.CreateDirectory(logDirectory); // Ensure the directory exists.

            LogFilePath = Path.Combine(logDirectory, "TestResults.log");

            Logger = new LoggerConfiguration()
                .WriteTo.Console() // Optional: Logs to console
                .WriteTo.File(LogFilePath, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
                .CreateLogger();
        }

        public static void Log(string message)
        {
            Logger.Information(message);
        }

        public static void LogError(string message, Exception ex)
        {
            Logger.Error(ex, message);
        }
    }
}
