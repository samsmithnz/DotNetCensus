using CommandLine;
using DotNetCensus.Core.Models;

namespace DotNetCensus
{
    public class CommandLineParser
    {
        public string? Directory { get; set; }
        public Repo? Repo { get; set; }
        public bool IncludeTotals { get; set; }
        public bool IncludeInventory { get; set; }
        public string? OutputFile { get; set; }

        public CommandLineParser(string[] args)
        {
            //process arguments
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);
        }

        internal void RunOptions(Options opts)
        {
            //setup the GitHub repo details
            if (opts.Owner != null)
            {
                Repo = new Repo(opts.Owner, opts.Repo)
                {
                    User = opts.User,
                    Password = opts.Password
                };
            }
            //Setup working directory details
            Directory = opts.Directory;
            if (Directory == null && Repo == null)
            {
                //If both directory and repo are null, use the current directory
                Directory = System.IO.Directory.GetCurrentDirectory();
            }
            //misc options
            IncludeTotals = opts.IncludeTotals;
            IncludeInventory = opts.IncludeInventory;
            OutputFile = opts.File;
        }

        internal void HandleParseError(IEnumerable<Error> errs)
        {
            //handle errors
            List<Exception> excList = new();
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
