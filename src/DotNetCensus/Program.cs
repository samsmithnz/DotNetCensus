using CommandLine;
using ConsoleTables;
using DotNetCensus.Core;
using DotNetCensus.Core.Models;

namespace DotNetCensus
{
    public class Program
    {
        private static string? _directory;
        private static bool _includeTotals;
        //private static string _outputFile;

        public static void Main(string[] args)
        {
            //process arguments
            ParserResult<Options>? result = Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(RunOptions)
                   .WithNotParsed(HandleParseError);

            //If there is a folder to scan, run the process against it
            if (string.IsNullOrEmpty(_directory) == false)
            {
                //Run the calculations to get and aggregate the results
                List<Project> projects = ProjectScanning.SearchDirectory(_directory);
                List<FrameworkSummary> results = Census.AggregateFrameworks(projects, _includeTotals);

                //Create and output the table
                ConsoleTable
                    .From<FrameworkSummary>(results)
                    .Configure(o => o.NumberAlignment = Alignment.Right)
                    .Write(Format.Minimal);
            }
        }

        static void RunOptions(Options opts)
        {
            //handle options
            _directory = opts.Directory;
            _includeTotals = opts.IncludeTotals;
            //_outputFile = opts.OutputFile;
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            //handle errors
            foreach (Error err in errs)
            {
                Console.WriteLine(err.ToString());
            }
        }
    }
}