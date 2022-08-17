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
            User = GitHubId,
            Password = GitHubSecret
        };
        string? file = null;
        if (directory != null || repo != null)
        {
            string expected = @"Framework             FrameworkFamily  Count  Status          
--------------------------------------------------------------
.NET 5.0              .NET             1      deprecated      
.NET 6.0              .NET             7      supported       
.NET 6.0-android      .NET             1      supported       
.NET 6.0-ios          .NET             1      supported       
.NET 7.0              .NET             1      in preview      
.NET Core 2.0         .NET Core        1      deprecated      
.NET Core 2.1         .NET Core        1      deprecated      
.NET Core 2.2         .NET Core        1      deprecated      
.NET Core 3.0         .NET Core        2      deprecated      
.NET Core 3.1         .NET Core        3      EOL: 13-Dec-2022
.NET Framework 1.0    .NET Framework   1      deprecated      
.NET Framework 1.1    .NET Framework   1      deprecated      
.NET Framework 2.0    .NET Framework   1      deprecated      
.NET Framework 3.5    .NET Framework   1      EOL: 9-Jan-2029 
.NET Framework 4.0    .NET Framework   2      deprecated      
.NET Framework 4.5    .NET Framework   2      deprecated      
.NET Framework 4.6.1  .NET Framework   1      deprecated      
.NET Framework 4.6.2  .NET Framework   1      supported       
.NET Framework 4.7.1  .NET Framework   1      supported       
.NET Framework 4.7.2  .NET Framework   2      supported       
.NET Standard 2.0     .NET Standard    1      supported       
(Unknown)             (Unknown)        2      unknown         
Visual Basic 6        Visual Basic 6   1      deprecated      
";

            //Act
            string? contents = Main.GetFrameworkSummary(directory, repo, includeTotals, file);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
        }
    }

    //    [TestMethod]
    //    public void FrameworkSummaryWithUserNameAndPasswordRepoTest()
    //    {
    //        //Arrange
    //        bool includeTotals = false;
    //        string? directory = null;
    //        Repo? repo = new("samsmithnz", "DotNetCensus")
    //        {
    //            User = null,
    //            Password = null
    //        };
    //        string? file = null;
    //        if (directory != null || repo != null)
    //        {
    //            string expected = @"Framework      FrameworkFamily  Count  Status          
    //-------------------------------------------------------
    //.NET Core 3.1  .NET Core        1      EOL: 13-Dec-2022
    //(Unknown)      (Unknown)        1      unknown         
    //";

    //            //Act
    //            string? contents = Main.GetFrameworkSummary(directory, repo, includeTotals, file);

    //            //Asset
    //            Assert.IsNotNull(expected);
    //            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
    //        }
    //    }

}
