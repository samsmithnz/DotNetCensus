using CommandLine;

namespace DotNetCensus
{
    public class Options
    {
        [Option('d', "directory", Required = false, HelpText = "Root directory to search for projects")]
        public string? Directory { get; set; }

        [Option('t', "Total", Required = false, Default = false, HelpText = "Include totals in results")]
        public bool IncludeTotals { get; set; }

        [Option('r', "Raw", Required = false, Default = false, HelpText = "Show raw results, instead of an aggregate")]
        public bool IncludeRawResults { get; set; }
        
        [Option('o', "output", Required = false, HelpText = "Output file to create json file")]
        public string? OutputFile { get; set; }
    }
}
