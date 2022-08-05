using CommandLine;
using ConsoleTables;
using DotNetCensus.Core;
using DotNetCensus.Core.Models;
using Spectre.Console;

namespace DotNetCensus
{
    public class Program
    {
        private static string? _directory;
        private static bool _includeTotals;
        private static bool _includeRawResults;
        private static string? _outputFile;

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
                //Need to sort so that Linux + Windows results are the same
                List<Project> sortedProjects = projects.OrderBy(o => o.FileName).ThenBy(o => o.Path).ToList();
                if (_includeRawResults == true)
                {
                    DataAccess.GetRawResults(_directory, _outputFile);
                }
                else
                {
                    DataAccess.GetFrameworkSummary(_directory, _includeTotals, _outputFile);
                }
            }
        }

        static void RunOptions(Options opts)
        {
            //handle options
            if (string.IsNullOrEmpty(opts.Directory) == true)
            {
                _directory = Directory.GetCurrentDirectory();
            }
            else
            {
                _directory = opts.Directory;
            }
            _includeTotals = opts.IncludeTotals;
            _includeRawResults = opts.IncludeRawResults;
            if (string.IsNullOrEmpty(opts.OutputFile) == false)
            {
                _outputFile = opts.OutputFile;
            }
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            //handle errors
            var excList = new List<Exception>();
            foreach (var err in errs)
            {
                excList.Add(new ArgumentException(err.ToString()));
            }
            if (excList.Any())
            {
                throw new AggregateException(excList);
            }
        }
    }
}