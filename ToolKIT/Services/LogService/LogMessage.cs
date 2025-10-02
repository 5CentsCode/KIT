using Microsoft.Extensions.Logging;

namespace ToolKIT.Services.LogService;
public readonly struct LogMessage
{
    public LogMessage(
        LogLevel logLevel,
        EventId eventId,
        string message,
        Exception? exception)
    {
        LogLevel = logLevel;
        EventId = eventId;
        Message = message;
        Exception = exception;
    }

    public LogLevel LogLevel { get; }

    public EventId EventId { get; }

    public string Message { get; }

    Exception? Exception { get; }
}
