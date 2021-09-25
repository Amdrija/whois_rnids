using System;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using Serilog;
using ILogger = RNIDS.WHOIS.Application.Base.ILogger;

namespace RNIDS.WHOIS.SerilogLogger
{
    public class SerilogLogger : ILogger
    {
        private readonly Serilog.ILogger logger;
        
        public SerilogLogger(IOptions<SentryOptions> sentryOptions)
        {
            this.logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .AddSentrySink(sentryOptions)
                .CreateLogger();

            Log.Logger = this.logger;

            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
        }

        public void Dispose()
        {
            Log.CloseAndFlush();
        }

        public void LogDebug(string message)
        {
            this.logger.Debug(message);
        }

        public void LogError(string message)
        {
            this.logger.Error(message);
        }

        public void LogError(Exception exception, string message)
        {
            this.logger.Error(exception, message);
        }

        public void LogInformation(string message)
        {
            this.logger.Information(message);
        }

        public void LogWarning(string message)
        {
            this.logger.Warning(message);
        }
    }
}