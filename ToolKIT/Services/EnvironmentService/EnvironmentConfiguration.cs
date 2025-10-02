namespace ToolKIT.Services.EnvironmentService;
public class EnvironmentConfiguration
{
    public EnvironmentConfiguration()
    {
        BeingPattern = "%";
        CapturePattern = @"\S*?";
        EndPattern = "%";
    }

    public string BeingPattern { get; set; }

    public string CapturePattern { get; set; }

    public string EndPattern { get; set; }
}
