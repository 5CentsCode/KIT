using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using ToolKIT.Extensions;

namespace ToolKIT.Services.LogService.LogFile;

public class FileLogger : ILogger
{
    private readonly FileLoggerConfiguration m_config;
    private readonly string m_name;
    private readonly StreamWriter m_streamWriter;

    public FileLogger(FileLoggerConfiguration config, string name, StreamWriter streamWriter)
    {
        m_config = config.ThrowIfNull();
        m_name = name.ThrowIfNull();
        m_streamWriter = streamWriter.ThrowIfNull();
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

        string message = new StringBuilder()
            .Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss "))
            .Append($"[{logLevel.ToString().First()}] ")
            .Append($"[{m_name}] - ")
            .Append(formatter(state, exception))
            .AppendLine()
            .ToString();

        m_streamWriter.Write(message);
    }
}
