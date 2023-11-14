using DotNetCensus.Tests.Helpers;

namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
[TestCategory("ConsoleAppFunctionalTest")]
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
                "-u", "",//GitHubId,
                "-p", "",//GitHubSecret,
                "-d", Path.GetTempPath(),
                "-t" };
            StringWriter sw = new();
            string expected = @"| Framework            | FrameworkFamily | Count | Status           |
|----------------------|-----------------|-------|------------------|
| .NET 7.0             | .NET            | 8     | EOL: 14-May-2024 |
| .NET 7.0-windows     | .NET            | 2     | EOL: 14-May-2024 |
| .NET Framework 4.7.2 | .NET Framework  | 1     | supported        |
| total frameworks     |                 | 11    |                  |

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
