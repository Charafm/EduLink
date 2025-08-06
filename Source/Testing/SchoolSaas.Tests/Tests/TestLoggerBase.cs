using Serilog;
using Xunit.Abstractions;

namespace SchoolSaas.Tests.Tests
{
    public abstract class TestLoggerBase : IDisposable
    {
        protected readonly ITestOutputHelper _output;
        private static readonly ILogger Logger;

        static TestLoggerBase()
        {
            string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestLogs");
            Directory.CreateDirectory(logDirectory);
            Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(Path.Combine(logDirectory, "TestResults.log"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
                .CreateLogger();
        }

        protected TestLoggerBase(ITestOutputHelper output)
        {
            _output = output;
            Log("🟢 Test Initialized...");
        }

        protected void Log(string message)
        {
            Logger.Information(message);
            _output.WriteLine(message);
        }

        protected void LogError(string message, Exception ex)
        {
            Logger.Error(ex, message);
            _output.WriteLine($"ERROR: {message} - {ex.Message}");
        }

        public void Dispose() => Log("🔴 Test Completed...");
    }
}
