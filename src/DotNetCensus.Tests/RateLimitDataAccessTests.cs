using DotNetCensus.Core.APIs;
using DotNetCensus.Tests.Helpers;

namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
[TestCategory("IntegrationTest")]
public class RateLimitDataAccessTests : RepoBasedTests
{

    [TestMethod]
    public async Task FrameworkSummaryWithGitHubRepoTest()
    {
        if (GitHubId != null || GitHubSecret != null)
        {
            //Arrange

            //Act
            int? rateLimitRemaining = await GitHubAPI.GetRateLimit(GitHubId, GitHubSecret);

            //Asset
            Assert.IsTrue(rateLimitRemaining > 0);
        }
    }
    
}
