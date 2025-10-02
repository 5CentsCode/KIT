using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using ToolKIT.Extensions;

namespace ToolKIT.Services.EnvironmentService;
public class Environment : IEnvironment
{
    private const string CaptureGroupName = "capture";
    private readonly EnvironmentConfiguration m_config;
    private readonly IConfiguration m_configuration;

    private readonly Regex m_regex;

    public Environment(IOptions<EnvironmentConfiguration> config, IConfiguration configuration)
    {
        m_config = config.Value.ThrowIfNull();
        m_configuration = configuration.ThrowIfNull();

        string pattern = $"{m_config.BeingPattern}(?<{CaptureGroupName}>{m_config.CapturePattern}){m_config.EndPattern}";
        m_regex = new Regex(pattern, RegexOptions.Compiled);
    }

    public string ExpandEnvironmentVariables(string str)
    {
        string expandedString = str;
        MatchCollection matches = m_regex.Matches(str);

        foreach (Match match in matches)
        {
            string replaceKey = match.Value;

            string? configKey = match.Groups[CaptureGroupName].Value;
            string? replaceValue = m_configuration[configKey];

            if (replaceValue != null)
            {
                expandedString = expandedString.Replace(replaceKey, replaceValue);
            }
        }

        return expandedString;
    }

    public string ShrinkEnvironmentVariables(string str)
    {
        string shrunkString = str;

        foreach ((string key, string? value) in m_configuration.AsEnumerable().OrderByDescending(kvp => kvp.Value?.Length ?? 0))
        {
            if (value == null)
            {
                continue;
            }

            shrunkString = shrunkString.Replace(value, $"{m_config.BeingPattern}{key}{m_config.EndPattern}");
        }

        return shrunkString;
    }

    public bool HasEnvironmentVariable(string str)
    {
        Match match = m_regex.Match(str);
        return match.Success;
    }
}
