using System;

namespace RNIDS.WHOIS.Application.Base
{
    public interface ILogger : IDisposable
    {
        void LogDebug(string message);

        void LogWarning(string message);

        void LogInformation(string message);

        void LogError(string message);

        void LogError(Exception exception, string message);
    }
}