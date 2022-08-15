using CommandLine;

namespace DotNetCensus
{
    public class Program
    {
        private static string? _directory;
        private static string? _repo;
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
            if (string.IsNullOrEmpty(_directory) == false || string.IsNullOrEmpty(_repo) == false)
            {
                if (_includeInventory == true)
                {
                    DataAccess.GetInventoryResults(_directory, _repo, _file);
                }
                else
                {
                    DataAccess.GetFrameworkSummary(_directory, _repo, _includeTotals, _file);
                }
            }
        }

        static void RunOptions(Options opts)
        {
            //handle options
            //if (string.IsNullOrEmpty(opts.Directory) == true)
            //{
            //    //if (string.IsNullOrEmpty(opts.RepoUrl) == false)
            //    //{
            //    //    _repo = opts.RepoUrl;
            //    //}
            //    //else
            //    //{
            //    _directory = Directory.GetCurrentDirectory();
            //    //}
            //}
            //else
            //{
            //    _directory = opts.Directory;
            //}
            _directory = opts.Directory;
            _repo = opts.Repo;
            if (_directory == null && _repo == null)
            {
                _directory = Directory.GetCurrentDirectory();
            }
            _includeTotals = opts.IncludeTotals;
            _includeInventory = opts.IncludeInventory;
            _file = opts.File;
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