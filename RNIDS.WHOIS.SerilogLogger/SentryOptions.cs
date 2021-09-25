using Serilog.Events;

namespace RNIDS.WHOIS.SerilogLogger
{
    public class SentryOptions
    {
        public string Dsn { get; set; } 
        public LogEventLevel MinimumEventLevel { get; set; }
        public LogEventLevel MinimumBreadcrumbLevel { get; set; }
    }
}