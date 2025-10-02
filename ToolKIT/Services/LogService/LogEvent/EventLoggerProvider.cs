using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ToolKIT.Extensions;

namespace ToolKIT.Services.LogService.LogEvent;

[ProviderAlias("Event")]
public class EventLoggerProvider : LoggerProviderBase
{
    private readonly EventLoggerConfiguration m_config;
    private readonly LogMessageListener m_logMessageListener;

    public EventLoggerProvider(
        IOptions<EventLoggerConfiguration> config,
        LogMessageListener logMessageListener)
    {
        m_config = config.Value.ThrowIfNull();
        m_logMessageListener = logMessageListener.ThrowIfNull();
    }

    protected override ILogger Create(string name)
    {
        EventLogger eventLogger = new EventLogger(m_config, name, m_logMessageListener);
        return eventLogger;
    }
}
