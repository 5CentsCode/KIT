namespace ToolKIT.Services.LogService.LogFile;
public sealed class FileLoggerConfiguration : LoggerConfigurationBase
{
    public FileLoggerConfiguration()
    {
        LogDirectory = string.Empty;
    }

    public string LogDirectory { get; set; }
}
