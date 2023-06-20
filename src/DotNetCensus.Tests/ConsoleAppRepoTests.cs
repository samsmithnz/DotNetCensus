using DotNetCensus.Tests.Helpers;

namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
[TestCategory("ConsoleAppFunctionalTest")]
public class ConsoleAppRepoTests : RepoBasedTests
{
    [TestMethod]
    public void RunConsoleAppWithTotalsFromRepoTest()
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
--------------------------------------------------------------
.NET 5.0              .NET             2      deprecated      
.NET 6.0              .NET             6      supported       
.NET 6.0-android      .NET             1      supported       
.NET 6.0-ios          .NET             1      supported       
.NET 6.0-maccatalyst  .NET             1      supported       
.NET 7.0              .NET             5      supported       
.NET 8.0              .NET             7      in preview      
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
.NET Framework 3.0    .NET Framework   1      deprecated      
.NET Framework 3.5    .NET Framework   3      EOL: 9-Jan-2029 
.NET Framework 4.0    .NET Framework   2      deprecated      
.NET Framework 4.5    .NET Framework   1      deprecated      
.NET Framework 4.5.1  .NET Framework   1      deprecated      
.NET Framework 4.5.2  .NET Framework   1      deprecated      
.NET Framework 4.6    .NET Framework   1      deprecated      
.NET Framework 4.6.1  .NET Framework   1      deprecated      
.NET Framework 4.6.2  .NET Framework   1      EOL: 12-Jan-2027
.NET Framework 4.7    .NET Framework   1      supported       
.NET Framework 4.7.1  .NET Framework   1      supported       
.NET Framework 4.7.2  .NET Framework   3      supported       
.NET Framework 4.8    .NET Framework   1      supported       
.NET Framework 4.8.1  .NET Framework   1      supported       
.NET Standard 1.0     .NET Standard    1      supported       
.NET Standard 1.1     .NET Standard    1      supported       
.NET Standard 1.2     .NET Standard    1      supported       
.NET Standard 1.3     .NET Standard    1      supported       
.NET Standard 1.4     .NET Standard    1      supported       
.NET Standard 1.5     .NET Standard    1      supported       
.NET Standard 1.6     .NET Standard    1      supported       
.NET Standard 2.0     .NET Standard    3      supported       
.NET Standard 2.1     .NET Standard    1      supported       
(Unknown)             (Unknown)        2      unknown         
Visual Basic 6        Visual Basic 6   1      deprecated      
total frameworks                       69                     

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
    public void RunConsoleAppWithTotalsFromBranchAndRepoTest()
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
--------------------------------------------------------------
.NET 5.0              .NET             2      deprecated      
.NET 6.0              .NET             6      supported       
.NET 6.0-android      .NET             1      supported       
.NET 6.0-ios          .NET             1      supported       
.NET 6.0-maccatalyst  .NET             1      supported       
.NET 7.0              .NET             5      supported       
.NET 8.0              .NET             7      in preview      
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
.NET Framework 3.0    .NET Framework   1      deprecated      
.NET Framework 3.5    .NET Framework   3      EOL: 9-Jan-2029 
.NET Framework 4.0    .NET Framework   2      deprecated      
.NET Framework 4.5    .NET Framework   1      deprecated      
.NET Framework 4.5.1  .NET Framework   1      deprecated      
.NET Framework 4.5.2  .NET Framework   1      deprecated      
.NET Framework 4.6    .NET Framework   1      deprecated      
.NET Framework 4.6.1  .NET Framework   1      deprecated      
.NET Framework 4.6.2  .NET Framework   1      EOL: 12-Jan-2027
.NET Framework 4.7    .NET Framework   1      supported       
.NET Framework 4.7.1  .NET Framework   1      supported       
.NET Framework 4.7.2  .NET Framework   3      supported       
.NET Framework 4.8    .NET Framework   1      supported       
.NET Framework 4.8.1  .NET Framework   1      supported       
.NET Standard 1.0     .NET Standard    1      supported       
.NET Standard 1.1     .NET Standard    1      supported       
.NET Standard 1.2     .NET Standard    1      supported       
.NET Standard 1.3     .NET Standard    1      supported       
.NET Standard 1.4     .NET Standard    1      supported       
.NET Standard 1.5     .NET Standard    1      supported       
.NET Standard 1.6     .NET Standard    1      supported       
.NET Standard 2.0     .NET Standard    3      supported       
.NET Standard 2.1     .NET Standard    1      supported       
(Unknown)             (Unknown)        2      unknown         
Visual Basic 6        Visual Basic 6   1      deprecated      
total frameworks                       69                     

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
