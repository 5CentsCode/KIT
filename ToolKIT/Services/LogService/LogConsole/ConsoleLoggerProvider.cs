using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ToolKIT.Extensions;

namespace ToolKIT.Services.LogService.LogConsole;

[ProviderAlias("Console")]
public sealed class ConsoleLoggerProvider : LoggerProviderBase
{
    private readonly ConsoleLoggerConfiguration m_config;

    public ConsoleLoggerProvider(IOptions<ConsoleLoggerConfiguration> config)
    {
        m_config = config.Value.ThrowIfNull();
    }

    protected override ILogger Create(string name)
    {
        return new ConsoleLogger(m_config, name);
    }
}
