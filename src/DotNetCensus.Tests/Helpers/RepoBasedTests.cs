global using DotNetCensus.Core;
global using DotNetCensus.Core.Models;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;

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
}
