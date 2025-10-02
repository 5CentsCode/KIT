namespace ToolKIT.Services.LogService;
public class LogMessageListener
{
    public event EventHandler<LogMessage>? OnMessageLogged;

    public void MessageLogged(LogMessage message)
    {
        OnMessageLogged?.Invoke(this, message);
    }
}
