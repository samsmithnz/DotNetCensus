using CommandLine;

namespace DotNetCensus;

public class Options
{
    [Option('d', "directory", Required = false, HelpText = "Root directory to search for projects. Can't be used at the same time as -r|--repo")]
    public string? Directory { get; set; }

    [Option('r', "repo", Required = false, HelpText = "Git repo to search for projects. Can't be used at the same time as -d|--directory. Note that this -r argument was formed used by --raw - which is now achieved with -i|--inventory")]
    public string? Repo { get; set; }

    [Option('u', "user", Required = false, HelpText = "Git repo user")]
    public string? User { get; set; }
    
    [Option('p', "password", Required = false, HelpText = "Git repo password")]
    public string? Password { get; set; }

    [Option('f', "file", Required = false, HelpText = "File to create CSV file")]
    public string? File { get; set; }

    [Option('t', "total", Required = false, Default = false, HelpText = "Include totals in results. Ignored when used with -i|--inventory")]
    public bool IncludeTotals { get; set; }

    [Option('i', "inventory", Required = false, Default = false, HelpText = "Show inventory of results, instead of an aggregate")]
    public bool IncludeInventory { get; set; }

    
}
