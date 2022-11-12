using DotNetCensus.Tests.Helpers;

namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
[TestCategory("IntegrationTest")]
public class DirectoryAndRepoDataAccessComparisonTests : RepoBasedTests
{

    //Compare the results of a directory scan and repo scan on the same repo
    [TestMethod]
    public void FrameworkSummaryDirectoryVsRepoTest()
    {
        //Arrange
        bool includeTotals = true;
        string? directory = SamplesPath?.Replace("samples", "");
        Repo? repo = null;
        string? file = null;
        bool includeTotals2 = true;
        string? directory2 = null;
        Repo? repo2 = new("samsmithnz", "DotNetCensus")
        {
            User = GitHubId,
            Password = GitHubSecret,
            Branch = GetCurrentBranch()
        };
        string? file2 = null;


        //Act
        string? contents = Main.GetFrameworkSummary(directory, repo, includeTotals, file);
        string? contents2 = Main.GetFrameworkSummary(directory2, repo2, includeTotals2, file2);

        //Asset
        Assert.IsNotNull(contents);
        Assert.IsNotNull(contents2);
        Assert.AreEqual(contents?.Replace("\\", "/"), contents2.Replace("\\", "/"));
    }
}
