using CommandLine;

namespace DotNetCensus
{
    public class Options
    {
        [Option('d', "directory", Required = false, HelpText = "Root directory to search for projects")]
        public string? Directory { get; set; }

        [Option('t', "total", Required = false, Default = false, HelpText = "Include totals in results")]
        public bool IncludeTotals { get; set; }

        [Option('r', "raw", Required = false, Default = false, HelpText = "Show raw results, instead of an aggregate")]
        public bool IncludeRawResults { get; set; }

        [Option('f', "file", Required = false, HelpText = "File to create CSV file")]
        public string? File { get; set; }
    }
}
