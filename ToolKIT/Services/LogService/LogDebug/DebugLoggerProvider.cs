using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ToolKIT.Extensions;

namespace ToolKIT.Services.LogService.LogDebug;

[ProviderAlias("Debug")]
public sealed class DebugLoggerProvider : LoggerProviderBase
{
    private readonly DebugLoggerConfiguration m_config;

    public DebugLoggerProvider(IOptions<DebugLoggerConfiguration> config)
    {
        m_config = config.Value.ThrowIfNull();
    }

    protected override ILogger Create(string name)
    {
        return new DebugLogger(m_config, name);
    }
}
