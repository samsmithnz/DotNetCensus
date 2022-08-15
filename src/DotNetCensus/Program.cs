using CommandLine;

namespace DotNetCensus
{
    public class Program
    {
        private static string? _directory;
        private static bool _includeTotals;
        private static bool _includeInventory;
        private static string? _file;

        public static void Main(string[] args)
        {
            //process arguments
            ParserResult<Options>? result = Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(RunOptions)
                   .WithNotParsed(HandleParseError);

            //If there is a folder to scan, run the process against it
            if (string.IsNullOrEmpty(_directory) == false)
            {
                if (_includeInventory == true)
                {
                    DataAccess.GetInventoryResults(_directory, _file);
                }
                else
                {
                    DataAccess.GetFrameworkSummary(_directory, _includeTotals, _file);
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
            _includeInventory = opts.IncludeInventory;
            if (string.IsNullOrEmpty(opts.File) == false)
            {
                _file = opts.File;
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