global using DotNetCensus.Core;
global using DotNetCensus.Core.Models;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace DotNetCensus.Tests.Helpers;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class RepoBasedTests : DirectoryBasedTests
{
    public string? GitHubId { get; set; }
    public string? GitHubSecret { get; set; }

    public RepoBasedTests()
    {
        //Load the appsettings.json configuration file
        IConfigurationBuilder? builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false)
             .AddUserSecrets<RepoBasedTests>(true);
        IConfigurationRoot configuration = builder.Build();

        GitHubId = configuration["AppSettings:GitHubClientId"];
        GitHubSecret = configuration["AppSettings:GitHubClientSecret"];
    }

    //With help from https://stackoverflow.com/a/48458952/337421
    public string GetCurrentBranch()
    {
        string branchName = "";
        ProcessStartInfo startInfo = new("git")
        {
            UseShellExecute = false,
            WorkingDirectory = Directory.GetCurrentDirectory(),
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            Arguments = "branch --show-current"
        };
        Process process = new()
        {
            StartInfo = startInfo
        };
        process.Start();

        if (process != null && process.StandardOutput != null)
        {
            string? result = process.StandardOutput.ReadLine();
            if (string.IsNullOrEmpty(result) == true)
            {
                branchName = "main";
            }
            else
            {
                branchName = result;
            }
        }

        return branchName;
    }
}
