using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using ToolKIT.Extensions;

namespace ToolKIT.Services.LogService.LogFile;

[ProviderAlias("File")]
public class FileLoggerProvider : LoggerProviderBase
{
    private readonly FileLoggerConfiguration m_config;
    private readonly StreamWriter m_streamWriter;

    public FileLoggerProvider(IOptions<FileLoggerConfiguration> config)
    {
        m_config = config.Value.ThrowIfNull();

        Directory.CreateDirectory(m_config.LogDirectory);

        DateTime fileDate = DateTime.UtcNow;
        string fileName = $"{fileDate:yyyy-MM-dd HH-mm-ss}.Log";
        string filePath = Path.Combine(m_config.LogDirectory, fileName);

        m_streamWriter = new StreamWriter(filePath, append: true)
        {
            AutoFlush = true
        };
    }

    public override void Dispose()
    {
        m_streamWriter.Close();
        base.Dispose();
        GC.SuppressFinalize(this);
    }

    protected override ILogger Create(string name)
    {
        return new FileLogger(m_config, name, m_streamWriter);
    }
}
