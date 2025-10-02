using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace ToolKIT.Services.LogService;

public abstract class LoggerProviderBase : ILoggerProvider
{
    private readonly ConcurrentDictionary<string, ILogger> m_loggers;

    protected LoggerProviderBase()
    {
        m_loggers = new(StringComparer.OrdinalIgnoreCase);
    }

    public virtual void Dispose()
    {
        m_loggers.Clear();
        GC.SuppressFinalize(this);
    }

    public ILogger CreateLogger(string categoryName)
    {
        return m_loggers.GetOrAdd(categoryName, Create);
    }

    protected abstract ILogger Create(string name);
}
