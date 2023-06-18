using DotNetCensus.Tests.Helpers;

namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
[TestCategory("IntegrationTest")]
public class ConsoleAppOrganizationTests : RepoBasedTests
{
    [TestMethod]
    public void RunConsoleAppOrganizationTest()
    {
        //Arrange
        if (GitHubId != null && GitHubSecret != null)
        {
            string[] parameters = new string[] { 
                "-o", "SamSmithNZ-dotcom",
                "-u", GitHubId,
                "-p", GitHubSecret,
                "-d", "c:\\temp",
                "-t" };
            StringWriter sw = new();
            string expected = @"Framework         FrameworkFamily  Count  Status
------------------------------------------------
total frameworks                   0            

";

            //Act
            Console.SetOut(sw);
            Program.Main(parameters);
            string result = sw.ToString();
            sw.Close();

            //Asset
            Assert.IsNotNull(result);
            result = TextHelper.CleanTimingFromResult(result);
            Assert.AreEqual(expected, result);
        }
    }


}