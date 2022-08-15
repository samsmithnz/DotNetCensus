using CommandLine;
using DotNetCensus.Core.Models;

namespace DotNetCensus;

public class Program
{
    private static string? _directory;
    private static Repo? _repo;
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
        if (string.IsNullOrEmpty(_directory) == false || _repo != null)
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
        _directory = opts.Directory;

        //setup the GitHub repo details
        if (opts.Owner != null && opts.Repo != null)
        {
            _repo = new Repo(opts.Owner, opts.Repo)
            {
                User = opts.Repo,
                Password = opts.Password
            };
        }
        if (_directory == null && _repo == null)
        {
            //If both directory and repo are null, use the current directory
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