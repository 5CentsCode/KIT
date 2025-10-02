using Microsoft.Extensions.Logging;

namespace ToolKIT.Services.LogService.LogConsole;

public sealed class ConsoleLoggerConfiguration : LoggerConfigurationBase
{
    public ConsoleLoggerConfiguration()
    {
        LogLevelToColorMap = new Dictionary<LogLevel, ConsoleColor>();
    }

    public Dictionary<LogLevel, ConsoleColor> LogLevelToColorMap { get; set; }
}
