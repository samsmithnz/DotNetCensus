using ConsoleTables;
using DotNetCensus.Core.APIs;
using DotNetCensus.Core.Models;
using DotNetCensus.Core.Models.GitHub;
using DotNetCensus.Core.Projects;
using System.Text;

namespace DotNetCensus.Core;

public static class Main
{
    private static List<Project> GetProjects(string? directory, Target? repo)
    {
        List<Project> projects = new();
        List<Project> sortedProjects = new();
        if (string.IsNullOrEmpty(directory) == false)
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
            if (repository != null)
            {
                projects = Task.Run(async () =>
                    await RepoScanning.SearchRepo(clientId, clientSecret,
                    owner, repository, "main")).Result;
            }
            else
            {
                List<RepoResponse>? repos = Task.Run(async () =>
                    await GitHubAPI.GetGitHubOrganizationRepos(clientId, clientSecret, owner)).Result;
                if (repos != null)
                {
                    foreach (RepoResponse item in repos)
                    {
                        if (item != null &&
                            item.name != null &&
                            item.default_branch != null)
                        {
                            List<Project> newProjects = Task.Run(async () =>
                                   await RepoScanning.SearchRepo(clientId, clientSecret,
                                   owner, item.name, item.default_branch)).Result;
                            //Add the organization and repo name to these projects
                            foreach (Project project in newProjects)
                            {
                                project.Organization = owner;
                                project.Repo = item.name;
                            }
                            projects.AddRange(newProjects);
                        }
                    }
                }
            }
        }
        //Need to sort so that Linux + Windows results are the same
        if (projects != null)
        {
            sortedProjects = projects.OrderBy(o => o.Organization).
                ThenBy(o => o.Repo).
                ThenBy(o => o.Path).ToList();
        }
        return sortedProjects;
    }

    public static string? GetInventoryResults(string? directory, Target? repo, string? file)
    {
        List<Project> projects = GetProjects(directory, repo);
        bool includeOrganizations = false;
        bool includeRepos = false;

        //If it's inventory output, remove the full path from each project
        if (directory != null)
        {
            if (projects.Count > 0)
            {
                if (string.IsNullOrEmpty(projects[0].Organization) == false)
                {
                    includeOrganizations = true;
                }
                if (string.IsNullOrEmpty(projects[0].Organization) == false)
                {
                    includeRepos = true;
                }
                foreach (Project item in projects)
                {
                    item.Path = item.Path.Replace(directory, "");
                }
            }
        }

        List<string> headers = new() { "Path", "FileName", "FrameworkCode", "FrameworkName", "Family", "Language", "Status" };
        if (includeRepos == true)
        {
            headers.Insert(0, "Repo");
        }
        if (includeOrganizations == true)
        {
            headers.Insert(0, "Organization");
        }
        if (string.IsNullOrEmpty(file) == true)
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

            //FileInfo fileInfo = new(_file);
            Console.WriteLine($"Exported results to '{file}'");
            return result;
        }
    }

    public static string? GetFrameworkSummary(string? directory, Target? repo, bool includeTotals, string? file)
    {
        List<Project> projects = GetProjects(directory, repo);
        List<FrameworkSummary> frameworks = Census.AggregateFrameworks(projects, includeTotals);

        if (string.IsNullOrEmpty(file) == true)
        {
            //Create and output the table
            ConsoleTable table = new("Framework", "FrameworkFamily", "Count", "Status");
            foreach (FrameworkSummary item in frameworks)
            {
                table.AddRow(item.Framework, item.FrameworkFamily, item.Count, item.Status);
            }
            string result = table.ToMinimalString();
            Console.WriteLine(result);
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

            //FileInfo fileInfo = new(_file);
            Console.WriteLine($"Exported results to '{file}'");
            return result;
        }
    }
}
