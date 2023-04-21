using DotNetCensus.Tests.Helpers;

namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
[TestCategory("IntegrationTest")]
public class ConsoleRepoAppTests : RepoBasedTests
{
    [TestMethod]
    public void RunSamplesWithTotalsFromRepoTest()
    {
        //Arrange
        if (GitHubId != null && GitHubSecret != null)
        {
            string[] parameters = new string[] { "-o", "samsmithnz",
                "-r", "dotnetcensus",
                "-u", GitHubId,
                "-p", GitHubSecret,
                "-t" };
            StringWriter sw = new();
            string expected = @"Framework             FrameworkFamily  Count  Status         
-------------------------------------------------------------
.NET 5.0              .NET             1      deprecated     
.NET 6.0              .NET             4      supported      
.NET 6.0-android      .NET             1      supported      
.NET 6.0-ios          .NET             1      supported      
.NET 7.0              .NET             5      supported      
.NET 8.0              .NET             1      in preview     
.NET Core 1.0         .NET Core        1      deprecated     
.NET Core 1.1         .NET Core        1      deprecated     
.NET Core 2.0         .NET Core        1      deprecated     
.NET Core 2.1         .NET Core        1      deprecated     
.NET Core 2.2         .NET Core        1      deprecated     
.NET Core 3.0         .NET Core        2      deprecated     
.NET Core 3.1         .NET Core        3      deprecated     
.NET Framework 1.0    .NET Framework   1      deprecated     
.NET Framework 1.1    .NET Framework   1      deprecated     
.NET Framework 2.0    .NET Framework   1      deprecated     
.NET Framework 3.5    .NET Framework   2      EOL: 9-Jan-2029
.NET Framework 4.0    .NET Framework   1      deprecated     
.NET Framework 4.5    .NET Framework   1      deprecated     
.NET Framework 4.6.1  .NET Framework   1      deprecated     
.NET Framework 4.6.2  .NET Framework   1      supported      
.NET Framework 4.7.1  .NET Framework   1      supported      
.NET Framework 4.7.2  .NET Framework   2      supported      
.NET Standard 2.0     .NET Standard    1      supported      
(Unknown)             (Unknown)        1      unknown        
Visual Basic 6        Visual Basic 6   1      deprecated     
total frameworks                       38                    

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

    [TestMethod]
    public void RunSamplesWithTotalsFromBranchAndRepoTest()
    {
        //Arrange
        if (GitHubId != null && GitHubSecret != null)
        {
            string[] parameters = new string[] { "-o", "samsmithnz",
                "-r", "dotnetcensus",
                "-u", GitHubId,
                "-p", GitHubSecret,
                "-b", GetCurrentBranch(),
                "-t" };
            StringWriter sw = new();
            string expected = @"Framework             FrameworkFamily  Count  Status         
-------------------------------------------------------------
.NET 5.0              .NET             1      deprecated     
.NET 6.0              .NET             4      supported      
.NET 6.0-android      .NET             1      supported      
.NET 6.0-ios          .NET             1      supported      
.NET 7.0              .NET             5      supported      
.NET 8.0              .NET             5      in preview     
.NET Core 1.0         .NET Core        1      deprecated     
.NET Core 1.1         .NET Core        1      deprecated     
.NET Core 2.0         .NET Core        1      deprecated     
.NET Core 2.1         .NET Core        1      deprecated     
.NET Core 2.2         .NET Core        1      deprecated     
.NET Core 3.0         .NET Core        2      deprecated     
.NET Core 3.1         .NET Core        3      deprecated     
.NET Framework 1.0    .NET Framework   1      deprecated     
.NET Framework 1.1    .NET Framework   1      deprecated     
.NET Framework 2.0    .NET Framework   1      deprecated     
.NET Framework 3.5    .NET Framework   2      EOL: 9-Jan-2029
.NET Framework 4.0    .NET Framework   1      deprecated     
.NET Framework 4.5    .NET Framework   1      deprecated     
.NET Framework 4.6.1  .NET Framework   1      deprecated     
.NET Framework 4.6.2  .NET Framework   1      supported      
.NET Framework 4.7.1  .NET Framework   1      supported      
.NET Framework 4.7.2  .NET Framework   3      supported      
.NET Standard 2.0     .NET Standard    3      supported      
(Unknown)             (Unknown)        1      unknown        
Visual Basic 6        Visual Basic 6   1      deprecated     
total frameworks                       45                    

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
