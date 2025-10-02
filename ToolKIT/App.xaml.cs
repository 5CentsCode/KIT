using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Windows;
using ToolKIT.Docking;
using ToolKIT.LogViewer;
using ToolKIT.Services.Dialog;
using ToolKIT.Services.EnvironmentService;
using ToolKIT.Services.LogService;
using ToolKIT.Services.LogService.LogConsole;
using ToolKIT.Services.LogService.LogDebug;
using ToolKIT.Services.LogService.LogEvent;
using ToolKIT.Services.LogService.LogFile;
using Environment = ToolKIT.Services.EnvironmentService.Environment;
using HHost = Microsoft.Extensions.Hosting.Host;

namespace ToolKIT;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ILogger m_log;

    private readonly IHost m_host;
    private readonly IServiceProvider m_serviceProvider;

    public App()
    {
        m_host = HHost.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddXmlFile("App.config", optional: true, reloadOnChange: true);
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            })
            .ConfigureServices((context, services) =>
            {
                // Register strongly typed options
                // Todo: Programmatically to get values from for a configuration.
                services.Configure<EnvironmentConfiguration>(context.Configuration.GetSection("Environment"));
                services.Configure<ConsoleLoggerConfiguration>(context.Configuration.GetSection("Logging:Console"));
                services.Configure<FileLoggerConfiguration>(context.Configuration.GetSection("Logging:File"));
                services.Configure<DebugLoggerConfiguration>(context.Configuration.GetSection("Logging:Debug"));
                services.Configure<EventLoggerConfiguration>(context.Configuration.GetSection("Logging:Event"));

                // Register your services here
                services.AddSingleton<IEnvironment, Environment>();
                services.AddSingleton<LogMessageListener>();

                services.AddSingleton<ILoggerProvider, ConsoleLoggerProvider>();
                services.AddSingleton<ILoggerProvider, FileLoggerProvider>();
                services.AddSingleton<ILoggerProvider, DebugLoggerProvider>();
                services.AddSingleton<ILoggerProvider, EventLoggerProvider>();

                services.AddSingleton<IDialogService, DialogService>();

                // ViewModels
                services.AddSingleton<DockingWindow>();
                // services.AddSingleton<MainWindow>();
                services.AddTransient<LogViewerVM>();
            })
            .Build();
        m_serviceProvider = m_host.Services;

        IConfiguration config = m_serviceProvider.GetRequiredService<IConfiguration>();
        IEnvironment environment = m_serviceProvider.GetRequiredService<IEnvironment>();
        m_log = m_serviceProvider.GetRequiredService<ILogger<App>>();

        foreach (LogLevel logLevel in Enum.GetValues<LogLevel>())
        {
            m_log.Log(logLevel, logLevel.ToString());
        }

        IDialogService dialogService = m_serviceProvider.GetRequiredService<IDialogService>();
        dialogService.Show<DockingWindow>();
    }
}
