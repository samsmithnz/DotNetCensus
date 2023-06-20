namespace DotNetCensus;

public class Program
{
    public static void Main(string[] args)
    {
        CommandLineParser newClass = new(args);

        //If there is a folder to scan, run the process against it
        if (!string.IsNullOrEmpty(newClass.Directory) || newClass.Repo != null)
        {
            if (newClass.IncludeInventory)
            {
                Core.Main.GetInventoryResultsAsString(newClass.Directory, newClass.Repo, newClass.OutputFile);
            }
            else
            {
                Core.Main.GetFrameworkSummaryAsString(newClass.Directory, newClass.Repo, newClass.IncludeTotals, newClass.OutputFile);
            }
        }
    }
}