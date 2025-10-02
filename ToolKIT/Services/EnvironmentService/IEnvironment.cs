namespace ToolKIT.Services.EnvironmentService;
public interface IEnvironment
{
    string ExpandEnvironmentVariables(string str);

    string ShrinkEnvironmentVariables(string str);

    public bool HasEnvironmentVariable(string str);
}
