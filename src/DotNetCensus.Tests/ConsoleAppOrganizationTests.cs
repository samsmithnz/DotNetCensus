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
            TextWriter originalOut = Console.Out;  // Save the original output
            string expected = @"| Framework            | FrameworkFamily | Count | Status    |
|----------------------|-----------------|-------|-----------|
| .NET 9.0             | .NET            | 11    | supported |
| .NET 9.0-windows     | .NET            | 2     | supported |
| .NET Framework 4.7.2 | .NET Framework  | 1     | supported |
| total frameworks     |                 | 14    |           |

";

            //Act
            try
            {
                Console.SetOut(sw);
                Program.Main(parameters);
                string result = sw.ToString();

                //Asset
                Assert.IsNotNull(result);
                result = TextHelper.CleanTimingFromResult(result);
                Assert.AreEqual(expected, result);
            }
            finally
            {
                Console.SetOut(originalOut);  // Always restore the original output
                sw.Close();
            }
        }
    }


}
