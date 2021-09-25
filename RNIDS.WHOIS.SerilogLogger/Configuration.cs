using Microsoft.Extensions.Options;
using Serilog;

namespace RNIDS.WHOIS.SerilogLogger
{
    public static class Configuration
    {
        public static LoggerConfiguration AddSentrySink(this LoggerConfiguration configuration,
            IOptions<SentryOptions> options)
        {
            return configuration.WriteTo.Sentry(o =>
            {
                o.Dsn = options.Value.Dsn;
                o.MinimumBreadcrumbLevel = options.Value.MinimumBreadcrumbLevel;
                o.MinimumEventLevel = options.Value.MinimumEventLevel;
            });
        }
    }
}