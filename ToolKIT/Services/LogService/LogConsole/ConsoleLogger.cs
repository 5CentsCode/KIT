using Microsoft.Extensions.Logging;
using ToolKIT.Extensions;

namespace ToolKIT.Services.LogService.LogConsole;
public sealed class ConsoleLogger : ILogger
{
    private readonly ConsoleLoggerConfiguration m_config;
    private readonly string m_name;

    public ConsoleLogger(ConsoleLoggerConfiguration config, string name)
    {
        m_config = config.ThrowIfNull();
        m_name = name.ThrowIfNull();
    }

    public IDisposable? BeginScope<TState>(TState state)
        where TState : notnull
    {
        return default;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= m_config.LogLevel &&
            logLevel != LogLevel.None;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        ConsoleColor originalColor = Console.ForegroundColor;
        Console.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss "));

        if (m_config.LogLevelToColorMap.TryGetValue(logLevel, out ConsoleColor logColor) == false)
        {
            logColor = originalColor;
        }

        Console.ForegroundColor = logColor;
        Console.Write($"[{logLevel.ToString().First()}] ");

        Console.ForegroundColor = originalColor;
        Console.Write($"[{m_name}] - ");
        Console.Write($"{formatter(state, exception)}");
        Console.WriteLine();
    }
}