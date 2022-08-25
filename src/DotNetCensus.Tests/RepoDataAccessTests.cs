using DotNetCensus.Tests.Helpers;

namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
[TestCategory("UnitTest")]
public class RepoDataAccessTests : RepoBasedTests
{

    [TestMethod]
    public void FrameworkSummaryWithGitHubRepoTest()
    {
        //Arrange
        bool includeTotals = true;
        string? directory = null;
        Target? repo = new("samsmithnz", "DotNetCensus")
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
.NET Core 1.0         .NET Core        1      deprecated      
.NET Core 1.1         .NET Core        1      deprecated      
.NET Core 2.0         .NET Core        1      deprecated      
.NET Core 2.1         .NET Core        1      deprecated      
.NET Core 2.2         .NET Core        1      deprecated      
.NET Core 3.0         .NET Core        2      deprecated      
.NET Core 3.1         .NET Core        3      EOL: 13-Dec-2022
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
total frameworks                       36                     
";

            //Act
            string? contents = Main.GetFrameworkSummary(directory, repo, includeTotals, file);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
        }
    }

    [TestMethod]
    public void InventoryResultsWithGitHubRepoTest()
    {
        //Arrange
        string? directory = null;
        Target? repo = new("samsmithnz", "DotNetCensus")
        {
            User = GitHubId,
            Password = GitHubSecret
        };
        string? file = null;
        if (directory != null || repo != null)
        {
            string expected = @"Path                                                                                     FileName                                    FrameworkCode   FrameworkName         Family          Language  Status          
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/samples/Sample.MultipleTargets.ConsoleApp/Sample.MultipleTargets.ConsoleApp.csproj      Sample.MultipleTargets.ConsoleApp.csproj    netcoreapp3.1   .NET Core 3.1         .NET Core       csharp    EOL: 13-Dec-2022
/samples/Sample.MultipleTargets.ConsoleApp/Sample.MultipleTargets.ConsoleApp.csproj      Sample.MultipleTargets.ConsoleApp.csproj    net462          .NET Framework 4.6.2  .NET Framework  csharp    supported       
/samples/Sample.Net5.ConsoleApp/Sample.Net5.ConsoleApp.csproj                            Sample.Net5.ConsoleApp.csproj               net5.0          .NET 5.0              .NET            csharp    deprecated      
/samples/Sample.Net6.ConsoleApp/Sample.Net6.ConsoleApp.csproj                            Sample.Net6.ConsoleApp.csproj               net6.0          .NET 6.0              .NET            csharp    supported       
/samples/Sample.Net6.ConsoleApp2/src/Sample.Net6.ConsoleApp.csproj                       Sample.Net6.ConsoleApp.csproj               net6.0          .NET 6.0              .NET            csharp    supported       
/samples/Sample.NET6.Directory.Build.props/App1/Sample.Net6.ConsoleApp.csproj            Sample.Net6.ConsoleApp.csproj               net6.0          .NET 6.0              .NET            csharp    supported       
/samples/Sample.Net6.MAUI.Calculator/src/Calculator/Calculator.csproj                    Calculator.csproj                           net6.0-ios      .NET 6.0-ios          .NET            csharp    supported       
/samples/Sample.Net6.MAUI.Calculator/src/Calculator/Calculator.csproj                    Calculator.csproj                           net6.0-android  .NET 6.0-android      .NET            csharp    supported       
/samples/Sample.Net6Inception.ConsoleApp/Sample.Net6.ConsoleApp.csproj                   Sample.Net6.ConsoleApp.csproj               net6.0          .NET 6.0              .NET            csharp    supported       
/samples/Sample.Net7.ConsoleApp/Sample.Net7.ConsoleApp.csproj                            Sample.Net7.ConsoleApp.csproj               net7.0          .NET 7.0              .NET            csharp    in preview      
/samples/Sample.NetCore1.0.ConsoleApp/project.json                                       project.json                                netcoreapp1.0   .NET Core 1.0         .NET Core       csharp    deprecated      
/samples/Sample.NetCore1.1.ConsoleApp/project.json                                       project.json                                netcoreapp1.1   .NET Core 1.1         .NET Core       csharp    deprecated      
/samples/Sample.NetCore2.0.ConsoleApp/Sample.NetCore2.0.ConsoleApp.csproj                Sample.NetCore2.0.ConsoleApp.csproj         netcoreapp2.0   .NET Core 2.0         .NET Core       csharp    deprecated      
/samples/Sample.NetCore2.1.ConsoleApp/Sample.NetCore2.1.ConsoleApp.csproj                Sample.NetCore2.1.ConsoleApp.csproj         netcoreapp2.1   .NET Core 2.1         .NET Core       csharp    deprecated      
/samples/Sample.NetCore2.2.ConsoleApp/Sample.NetCore2.2.ConsoleApp.csproj                Sample.NetCore2.2.ConsoleApp.csproj         netcoreapp2.2   .NET Core 2.2         .NET Core       csharp    deprecated      
/samples/Sample.NetCore3.0.ConsoleApp/Sample.NetCore3.0.ConsoleApp.csproj                Sample.NetCore3.0.ConsoleApp.csproj         netcoreapp3.0   .NET Core 3.0         .NET Core       csharp    deprecated      
/samples/Sample.NetCore3.0.fsharp.ConsoleApp/hello-world-fsharp.fsproj                   hello-world-fsharp.fsproj                   netcoreapp3.0   .NET Core 3.0         .NET Core       fsharp    deprecated      
/samples/Sample.NetCore3.1.ConsoleApp/Sample.NetCore.ConsoleApp.csproj                   Sample.NetCore.ConsoleApp.csproj            netcoreapp3.1   .NET Core 3.1         .NET Core       csharp    EOL: 13-Dec-2022
/samples/Sample.NetFramework.ConsoleApp/Sample.NetFramework.ConsoleApp.csproj            Sample.NetFramework.ConsoleApp.csproj       v4.7.2          .NET Framework 4.7.2  .NET Framework  csharp    supported       
/samples/Sample.NetFramework1.0.App/VBProj.vbproj                                        VBProj.vbproj                               v1.0            .NET Framework 1.0    .NET Framework  vb.net    deprecated      
/samples/Sample.NetFramework1.1.App/VBProj.vbproj                                        VBProj.vbproj                               v1.1            .NET Framework 1.1    .NET Framework  vb.net    deprecated      
/samples/Sample.NetFramework2.0.App/VBProj.vbproj                                        VBProj.vbproj                               v2.0            .NET Framework 2.0    .NET Framework  vb.net    deprecated      
/samples/Sample.NetFramework3.5.WebApp/dotnetapp-3.5.csproj                              dotnetapp-3.5.csproj                        v3.5            .NET Framework 3.5    .NET Framework  csharp    EOL: 9-Jan-2029 
/samples/Sample.NetFramework3.5.Website/web.config                                       web.config                                  v3.5            .NET Framework 3.5    .NET Framework  csharp    EOL: 9-Jan-2029 
/samples/Sample.NETFramework4.6.1FSharp.HelloWorld/HelloWorld.fsproj                     HelloWorld.fsproj                           net461          .NET Framework 4.6.1  .NET Framework  fsharp    deprecated      
/samples/Sample.NetFramework40.WebApp/WorldBankSample.csproj                             WorldBankSample.csproj                      v4.0            .NET Framework 4.0    .NET Framework  csharp    deprecated      
/samples/Sample.NetFramework45.WebApp/WorldBankSample.csproj                             WorldBankSample.csproj                      v4.5            .NET Framework 4.5    .NET Framework  csharp    deprecated      
/samples/Sample.NetFrameworkInvalid.App/VBProj.vbproj                                    VBProj.vbproj                                               (Unknown)             (Unknown)       vb.net    unknown         
/samples/Sample.NetFrameworkVBNet.ConsoleApp/Sample.NetFrameworkVBNet.ConsoleApp.vbproj  Sample.NetFrameworkVBNet.ConsoleApp.vbproj  netcoreapp3.1   .NET Core 3.1         .NET Core       vb.net    EOL: 13-Dec-2022
/samples/Sample.NetStandard.Class/Sample.NetStandard.Class.csproj                        Sample.NetStandard.Class.csproj             netstandard2.0  .NET Standard 2.0     .NET Standard   csharp    supported       
/samples/Sample.SSDT.Database/Sample.SSDT.Database.sqlproj                               Sample.SSDT.Database.sqlproj                v4.7.2          .NET Framework 4.7.2  .NET Framework  csharp    supported       
/samples/Sample.Unity2020/Assembly-CSharp.csproj                                         Assembly-CSharp.csproj                      v4.7.1          .NET Framework 4.7.1  .NET Framework  csharp    supported       
/samples/Sample.VB6.Calculator/Sample.VB6.WinApp.vbp                                     Sample.VB6.WinApp.vbp                       vb6             Visual Basic 6        Visual Basic 6  vb6       deprecated      
/src/DotNetCensus.Core/DotNetCensus.Core.csproj                                          DotNetCensus.Core.csproj                    net6.0          .NET 6.0              .NET            csharp    supported       
/src/DotNetCensus.Tests/DotNetCensus.Tests.csproj                                        DotNetCensus.Tests.csproj                   net6.0          .NET 6.0              .NET            csharp    supported       
/src/DotNetCensus/DotNetCensus.csproj                                                    DotNetCensus.csproj                         net6.0          .NET 6.0              .NET            csharp    supported       
";

            //Act
            string? contents = Main.GetInventoryResults(directory, repo, file);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
        }
    }

    [TestMethod]
    public void FrameworkSummaryWithGitHubOrganizationTest()
    {
        //Arrange
        bool includeTotals = true;
        string? directory = null;
        Target? repo = new("SamSmithNZ-dotcom")
        {
            User = GitHubId,
            Password = GitHubSecret
        };
        string? file = null;
        if (directory != null || repo != null)
        {
            string expected = @"Path                                                                                            FileName                                      FrameworkCode   FrameworkName         Family          Language  Status    
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/MandMCounter/MandMCounter.Core/MandMCounter.Core.csproj                                        MandMCounter.Core.csproj                      net6.0          .NET 6.0              .NET            csharp    supported 
/MandMCounter/MandMCounter.Service/MandMCounter.Service.csproj                                  MandMCounter.Service.csproj                   net6.0          .NET 6.0              .NET            csharp    supported 
/MandMCounter/MandMCounter.Tests/MandMCounter.Tests.csproj                                      MandMCounter.Tests.csproj                     net6.0          .NET 6.0              .NET            csharp    supported 
/SamSmithNZ/SamSmithNZ.Database/SamSmithNZ.Database.sqlproj                                     SamSmithNZ.Database.sqlproj                   v4.7.2          .NET Framework 4.7.2  .NET Framework  csharp    supported 
/SamSmithNZ/SamSmithNZ.FFLSetlistScraper.WinForms/SamSmithNZ.FFLSetlistScraper.WinForms.csproj  SamSmithNZ.FFLSetlistScraper.WinForms.csproj  net6.0-windows  .NET 6.0-windows      .NET            csharp    supported 
/SamSmithNZ/SamSmithNZ.FunctionalTests/SamSmithNZ.FunctionalTests.csproj                        SamSmithNZ.FunctionalTests.csproj             net6.0          .NET 6.0              .NET            csharp    supported 
/SamSmithNZ/SamSmithNZ.Service/SamSmithNZ.Service.csproj                                        SamSmithNZ.Service.csproj                     net6.0          .NET 6.0              .NET            csharp    supported 
/SamSmithNZ/SamSmithNZ.Tests/SamSmithNZ.Tests.csproj                                            SamSmithNZ.Tests.csproj                       net6.0          .NET 6.0              .NET            csharp    supported 
/SamSmithNZ/SamSmithNZ.Web/SamSmithNZ.Web.csproj                                                SamSmithNZ.Web.csproj                         net6.0          .NET 6.0              .NET            csharp    supported 
/SamSmithNZ/SamSmithNZ.WorldCupGoals.WPF/SamSmithNZ.WorldCupGoals.WPF.csproj                    SamSmithNZ.WorldCupGoals.WPF.csproj           net6.0-windows  .NET 6.0-windows      .NET            csharp    supported 
/SSNZ/SSNZ.FunctionalTests/SSNZ.FunctionalTests.csproj                                          SSNZ.FunctionalTests.csproj                   net5.0          .NET 5.0              .NET            csharp    deprecated
/SSNZ/SSNZ.Tests/SSNZ.Tests.csproj                                                              SSNZ.Tests.csproj                             net5.0          .NET 5.0              .NET            csharp    deprecated
/SSNZ/SSNZ.Web/SSNZ.Web.csproj                                                                  SSNZ.Web.csproj                               net5.0          .NET 5.0              .NET            csharp    deprecated
/SSNZ/SSNZ.WebAPI/SSNZ.WebAPI.csproj                                                            SSNZ.WebAPI.csproj                            net5.0          .NET 5.0              .NET            csharp    deprecated
/SSNZ/SSNZ.WinForms/SSNZ.WinForms.csproj                                                        SSNZ.WinForms.csproj                          net5.0-windows  .NET 5.0-windows      .NET            csharp    deprecated
/SSNZ/SSNZ.WPF/SSNZ.WPF.csproj                                                                  SSNZ.WPF.csproj                               net5.0-windows  .NET 5.0-windows      .NET            csharp    deprecated
";

            //Act
            string? contents = Main.GetFrameworkSummary(directory, repo, includeTotals, file);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
        }
    }

    [TestMethod]
    public void InventoryResultsWithGitHubOrganizationTest()
    {
        //Arrange
        string? directory = null;
        Target? repo = new("SamSmithNZ-dotcom")
        {
            User = GitHubId,
            Password = GitHubSecret
        };
        string? file = null;
        if (directory != null || repo != null)
        {
            string expected = @"Path                                                                                     FileName                                    FrameworkCode   FrameworkName         Family          Language  Status          
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/samples/Sample.MultipleTargets.ConsoleApp/Sample.MultipleTargets.ConsoleApp.csproj      Sample.MultipleTargets.ConsoleApp.csproj    netcoreapp3.1   .NET Core 3.1         .NET Core       csharp    EOL: 13-Dec-2022
/samples/Sample.MultipleTargets.ConsoleApp/Sample.MultipleTargets.ConsoleApp.csproj      Sample.MultipleTargets.ConsoleApp.csproj    net462          .NET Framework 4.6.2  .NET Framework  csharp    supported       
/samples/Sample.Net5.ConsoleApp/Sample.Net5.ConsoleApp.csproj                            Sample.Net5.ConsoleApp.csproj               net5.0          .NET 5.0              .NET            csharp    deprecated      
/samples/Sample.Net6.ConsoleApp/Sample.Net6.ConsoleApp.csproj                            Sample.Net6.ConsoleApp.csproj               net6.0          .NET 6.0              .NET            csharp    supported       
/samples/Sample.Net6.ConsoleApp2/src/Sample.Net6.ConsoleApp.csproj                       Sample.Net6.ConsoleApp.csproj               net6.0          .NET 6.0              .NET            csharp    supported       
/samples/Sample.NET6.Directory.Build.props/App1/Sample.Net6.ConsoleApp.csproj            Sample.Net6.ConsoleApp.csproj               net6.0          .NET 6.0              .NET            csharp    supported       
/samples/Sample.Net6.MAUI.Calculator/src/Calculator/Calculator.csproj                    Calculator.csproj                           net6.0-ios      .NET 6.0-ios          .NET            csharp    supported       
/samples/Sample.Net6.MAUI.Calculator/src/Calculator/Calculator.csproj                    Calculator.csproj                           net6.0-android  .NET 6.0-android      .NET            csharp    supported       
/samples/Sample.Net6Inception.ConsoleApp/Sample.Net6.ConsoleApp.csproj                   Sample.Net6.ConsoleApp.csproj               net6.0          .NET 6.0              .NET            csharp    supported       
/samples/Sample.Net7.ConsoleApp/Sample.Net7.ConsoleApp.csproj                            Sample.Net7.ConsoleApp.csproj               net7.0          .NET 7.0              .NET            csharp    in preview      
/samples/Sample.NetCore1.0.ConsoleApp/project.json                                       project.json                                netcoreapp1.0   .NET Core 1.0         .NET Core       csharp    deprecated      
/samples/Sample.NetCore1.1.ConsoleApp/project.json                                       project.json                                netcoreapp1.1   .NET Core 1.1         .NET Core       csharp    deprecated      
/samples/Sample.NetCore2.0.ConsoleApp/Sample.NetCore2.0.ConsoleApp.csproj                Sample.NetCore2.0.ConsoleApp.csproj         netcoreapp2.0   .NET Core 2.0         .NET Core       csharp    deprecated      
/samples/Sample.NetCore2.1.ConsoleApp/Sample.NetCore2.1.ConsoleApp.csproj                Sample.NetCore2.1.ConsoleApp.csproj         netcoreapp2.1   .NET Core 2.1         .NET Core       csharp    deprecated      
/samples/Sample.NetCore2.2.ConsoleApp/Sample.NetCore2.2.ConsoleApp.csproj                Sample.NetCore2.2.ConsoleApp.csproj         netcoreapp2.2   .NET Core 2.2         .NET Core       csharp    deprecated      
/samples/Sample.NetCore3.0.ConsoleApp/Sample.NetCore3.0.ConsoleApp.csproj                Sample.NetCore3.0.ConsoleApp.csproj         netcoreapp3.0   .NET Core 3.0         .NET Core       csharp    deprecated      
/samples/Sample.NetCore3.0.fsharp.ConsoleApp/hello-world-fsharp.fsproj                   hello-world-fsharp.fsproj                   netcoreapp3.0   .NET Core 3.0         .NET Core       fsharp    deprecated      
/samples/Sample.NetCore3.1.ConsoleApp/Sample.NetCore.ConsoleApp.csproj                   Sample.NetCore.ConsoleApp.csproj            netcoreapp3.1   .NET Core 3.1         .NET Core       csharp    EOL: 13-Dec-2022
/samples/Sample.NetFramework.ConsoleApp/Sample.NetFramework.ConsoleApp.csproj            Sample.NetFramework.ConsoleApp.csproj       v4.7.2          .NET Framework 4.7.2  .NET Framework  csharp    supported       
/samples/Sample.NetFramework1.0.App/VBProj.vbproj                                        VBProj.vbproj                               v1.0            .NET Framework 1.0    .NET Framework  vb.net    deprecated      
/samples/Sample.NetFramework1.1.App/VBProj.vbproj                                        VBProj.vbproj                               v1.1            .NET Framework 1.1    .NET Framework  vb.net    deprecated      
/samples/Sample.NetFramework2.0.App/VBProj.vbproj                                        VBProj.vbproj                               v2.0            .NET Framework 2.0    .NET Framework  vb.net    deprecated      
/samples/Sample.NetFramework3.5.WebApp/dotnetapp-3.5.csproj                              dotnetapp-3.5.csproj                        v3.5            .NET Framework 3.5    .NET Framework  csharp    EOL: 9-Jan-2029 
/samples/Sample.NetFramework3.5.Website/web.config                                       web.config                                  v3.5            .NET Framework 3.5    .NET Framework  csharp    EOL: 9-Jan-2029 
/samples/Sample.NETFramework4.6.1FSharp.HelloWorld/HelloWorld.fsproj                     HelloWorld.fsproj                           net461          .NET Framework 4.6.1  .NET Framework  fsharp    deprecated      
/samples/Sample.NetFramework40.WebApp/WorldBankSample.csproj                             WorldBankSample.csproj                      v4.0            .NET Framework 4.0    .NET Framework  csharp    deprecated      
/samples/Sample.NetFramework45.WebApp/WorldBankSample.csproj                             WorldBankSample.csproj                      v4.5            .NET Framework 4.5    .NET Framework  csharp    deprecated      
/samples/Sample.NetFrameworkInvalid.App/VBProj.vbproj                                    VBProj.vbproj                                               (Unknown)             (Unknown)       vb.net    unknown         
/samples/Sample.NetFrameworkVBNet.ConsoleApp/Sample.NetFrameworkVBNet.ConsoleApp.vbproj  Sample.NetFrameworkVBNet.ConsoleApp.vbproj  netcoreapp3.1   .NET Core 3.1         .NET Core       vb.net    EOL: 13-Dec-2022
/samples/Sample.NetStandard.Class/Sample.NetStandard.Class.csproj                        Sample.NetStandard.Class.csproj             netstandard2.0  .NET Standard 2.0     .NET Standard   csharp    supported       
/samples/Sample.SSDT.Database/Sample.SSDT.Database.sqlproj                               Sample.SSDT.Database.sqlproj                v4.7.2          .NET Framework 4.7.2  .NET Framework  csharp    supported       
/samples/Sample.Unity2020/Assembly-CSharp.csproj                                         Assembly-CSharp.csproj                      v4.7.1          .NET Framework 4.7.1  .NET Framework  csharp    supported       
/samples/Sample.VB6.Calculator/Sample.VB6.WinApp.vbp                                     Sample.VB6.WinApp.vbp                       vb6             Visual Basic 6        Visual Basic 6  vb6       deprecated      
/src/DotNetCensus.Core/DotNetCensus.Core.csproj                                          DotNetCensus.Core.csproj                    net6.0          .NET 6.0              .NET            csharp    supported       
/src/DotNetCensus.Tests/DotNetCensus.Tests.csproj                                        DotNetCensus.Tests.csproj                   net6.0          .NET 6.0              .NET            csharp    supported       
/src/DotNetCensus/DotNetCensus.csproj                                                    DotNetCensus.csproj                         net6.0          .NET 6.0              .NET            csharp    supported       
";

            //Act
            string? contents = Main.GetInventoryResults(directory, repo, file);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
        }
    }

}
