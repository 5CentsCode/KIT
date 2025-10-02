using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ToolKIT.Services.LogService;
public abstract class LoggerConfigurationBase
{
    [ConfigurationKeyName("LogLevel:Default")]
    public LogLevel LogLevel { get; set; }
}
