using ConsoleTables;
using DotNetCensus.Core.APIs;
using DotNetCensus.Core.Models;
using DotNetCensus.Core.Models.GitHub;
using DotNetCensus.Core.Projects;
using System.Text;

namespace DotNetCensus.Core;

public static class Main
{
    public static string? GetInventoryResultsAsString(string? directory, Repo? repo, string? file)
    {
        DateTime startTime = DateTime.Now;
        List<Project> projects = GetProjects(directory, repo);
        bool includeOrganizations = false;
        bool includeRepos = false;

        //If it's inventory output, remove the full path from each project
        if (directory != null)
        {
            foreach (Project item in projects)
            {
                if (OperatingSystem.IsWindows())
                {
                    //Fix any Linux path separators to be Windows ones
                    item.Path = item.Path.Replace(directory.Replace("/", "\\"), "");
                }
                else
                {
                    item.Path = item.Path.Replace(directory, "");
                }
            }
        }

        if (string.IsNullOrEmpty(file))
        {
            ConsoleTable table = new(headers.ToArray());
            foreach (Project item in projects)
            {
                if (includeOrganizations == true && includeRepos == true)
                {
                    table.AddRow(item.Organization, item.Repo, item.Path, item.FileName, item.FrameworkCode, item.FrameworkName, item.Family, item.Language, item.Status);
                }
                else
                {
                    table.AddRow(item.Path, item.FileName, item.FrameworkCode, item.FrameworkName, item.Family, item.Language, item.Status);
                }
            }
            string result = table.ToMinimalString();
            Console.WriteLine(result);
            Console.WriteLine("Time to process: " + TimingHelper.GetTime(DateTime.Now - startTime));
            return result;
        }
        else
        {
            //Create a CSV file
            StreamWriter sw = File.CreateText(file);
            StringBuilder header = new();
            for (int i = 0; i <= headers.Count - 1; i++)
            {
                header.Append(headers[i]);
                if (i < headers.Count - 1)
                {
                    header.Append(',');
                }
            }
            sw.WriteLine(header.ToString());
            foreach (Project item in projects)
            {
                if (includeOrganizations == true && includeRepos == true)
                {
                    sw.WriteLine(item.Organization + "," +
                       item.Repo + "," +
                       item.Path + "," +
                       item.FileName + "," +
                       item.FrameworkCode + "," +
                       item.FrameworkName + "," +
                       item.Family + "," +
                       item.Language + "," +
                       item.Status);
                }
                else
                {
                    sw.WriteLine(item.Path + "," +
                        item.FileName + "," +
                        item.FrameworkCode + "," +
                        item.FrameworkName + "," +
                        item.Family + "," +
                        item.Language + "," +
                        item.Status);
                }
            }
            string? result = sw?.ToString();
            sw?.Close();

            Console.WriteLine($"Exported results to '{file}'");
            Console.WriteLine("Time to process: " + TimingHelper.GetTime(DateTime.Now - startTime));
            return result;
        }
    }

    /// <summary>
    /// Return a string of the framework summary. Can also write to a file
    /// </summary>
    /// <param name="directory">directory to scan</param>
    /// <param name="repo">GitHub repo to scan</param>
    /// <param name="includeTotals">include a totals row</param>
    /// <param name="file">output string to a file</param>
    /// <returns></returns>
    public static string? GetFrameworkSummaryAsString(string? directory, Repo? repo, bool includeTotals, string? file)
    {
        DateTime startTime = DateTime.Now;
        List<FrameworkSummary> frameworks = GetFrameworkSummary(directory, repo, includeTotals);

        if (string.IsNullOrEmpty(file))
        {
            //Create and output the table
            ConsoleTable table = new("Framework", "FrameworkFamily", "Count", "Status");
            foreach (FrameworkSummary item in frameworks)
            {
                table.AddRow(item.Framework, item.FrameworkFamily, item.Count, item.Status);
            }
            string result = table.ToMinimalString();
            Console.WriteLine(result);
            Console.WriteLine("Time to process: " + TimingHelper.GetTime(DateTime.Now - startTime));
            return result;
        }
        else
        {
            //Create a CSV file
            StreamWriter sw = File.CreateText(file);
            sw.WriteLine("Framework,FrameworkFamily,Count,Status");
            foreach (FrameworkSummary item in frameworks)
            {
                sw.WriteLine(item.Framework + "," +
                    item.FrameworkFamily + "," +
                    item.Count + "," +
                    item.Status);
            }
            string? result = sw?.ToString();
            sw?.Close();

            Console.WriteLine($"Exported results to '{file}'");
            Console.WriteLine("Time to process: " + TimingHelper.GetTime(DateTime.Now - startTime));
            return result;
        }
    }

    /// <summary>
    /// Return a list of Framework summary for each framework found 
    /// </summary>
    /// <param name="directory">directory to scan</param>
    /// <param name="repo">GitHub repo to scan</param>
    /// <param name="includeTotals">include a totals row</param>
    /// <returns></returns>
    public static List<FrameworkSummary> GetFrameworkSummary(string? directory, Repo? repo, bool includeTotals)
    {
        List<Project> projects = GetProjects(directory, repo);
        List<FrameworkSummary> frameworkSummary = Census.AggregateFrameworks(projects, includeTotals);
        return frameworkSummary;
    }


    private static List<Project> GetProjects(string? directory, Repo? repo)
    {
        List<Project> projects = new();
        List<Project> sortedProjects = new();
        if (!string.IsNullOrEmpty(directory))
        {
            //Run the calculations to get and aggregate the results
            projects = DirectoryScanning.SearchDirectory(directory);
        }
        else if (repo != null)
        {
            string? owner = repo.Owner;
            string? repository = repo.Repository;
            string? clientId = repo.User;
            string? clientSecret = repo.Password;
            string? branch = repo.Branch;
            if (string.IsNullOrEmpty(branch))
            {
                branch = "main";
            }
            projects = Task.Run(async () =>
                await RepoScanning.SearchRepo(clientId, clientSecret,
                owner, repository, branch)).Result;
        }
        //Need to sort so that Linux + Windows results are the same
        if (projects != null)
        {
            sortedProjects = projects.OrderBy(o => o.Path).ToList();
        }
        return sortedProjects;
    }
}
