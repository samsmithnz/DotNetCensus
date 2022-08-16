using DotNetCensus.Tests.Helpers;

namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class ConsoleRepoDataAccessTests : RepoBasedTests
{  

    [TestMethod]
    public void FrameworkSummaryWithRepoTest()
    {
        //Arrange
        bool includeTotals = false;
        string? directory = null;
        Repo? repo = new("samsmithnz", "DotNetCensus")
        {
            Owner = GitHubId,
            Password = GitHubSecret
        };
        string? file = null;
        if (directory != null || repo != null)
        {
            string expected = @"Framework      FrameworkFamily  Count  Status          
-------------------------------------------------------
.NET Core 3.1  .NET Core        1      EOL: 13-Dec-2022
(Unknown)      (Unknown)        1      unknown         
";

            //Act
            string? contents = Main.GetFrameworkSummary(directory, repo, includeTotals, file);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
        }
    }

    [TestMethod]
    public void FrameworkSummaryWithUserNameAndPasswordRepoTest()
    {
        //Arrange
        bool includeTotals = false;
        string? directory = null;
        Repo? repo = new("samsmithnz", "DotNetCensus")
        {
            User = null,
            Password = null
        };
        string? file = null;
        if (directory != null || repo != null)
        {
            string expected = @"Framework      FrameworkFamily  Count  Status          
-------------------------------------------------------
.NET Core 3.1  .NET Core        1      EOL: 13-Dec-2022
(Unknown)      (Unknown)        1      unknown         
";

            //Act
            string? contents = Main.GetFrameworkSummary(directory, repo, includeTotals, file);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
        }
    }

}
