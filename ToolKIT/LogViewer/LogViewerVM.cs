using ToolKIT.Extensions;
using ToolKIT.Services.LogService;

namespace ToolKIT.LogViewer;
public class LogViewerVM : BaseVM, IDisposable
{
    private readonly LogMessageListener m_logMessageProvider;

    public LogViewerVM(LogMessageListener logMessageListener)
    {
        m_logMessageProvider = logMessageListener.ThrowIfNull();

        m_logMessageProvider.OnMessageLogged += OnMessageLogged;
    }

    public void Dispose()
    {
        m_logMessageProvider.OnMessageLogged -= OnMessageLogged;
        GC.SuppressFinalize(this);
    }

    public static void OnMessageLogged(object? sender, LogMessage e)
    {
    }
}
