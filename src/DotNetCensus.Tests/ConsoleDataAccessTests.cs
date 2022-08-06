namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class ConsoleDataAccessTests : BaseTests
{
    [TestMethod]
    public void FrameworkSummaryWithNoParametersTest()
    {
        //Arrange
        bool includeTotals = false;
        string? file = null;
        if (SamplesPath != null)
        {
            string expected = @"Framework  FrameworkFamily  Count  Status
-----------------------------------------
";

            //Act
            string? contents = DataAccess.GetFrameworkSummary("", includeTotals, file);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
        }
    }

    [TestMethod]
    public void FrameworkSummaryWithPathTest()
    {
        //Arrange
        bool includeTotals = false;
        string? file = null;
        if (SamplesPath != null)
        {
            string expected = @"Framework             FrameworkFamily  Count  Status          
--------------------------------------------------------------
.NET 5.0              .NET             1      deprecated      
.NET 6.0              .NET             2      supported       
.NET 6.0-android      .NET             1      supported       
.NET 6.0-ios          .NET             1      supported       
.NET 7.0              .NET             1      supported       
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
.NET Framework 3.5    .NET Framework   1      EOL: 9-Jan-2029 
.NET Framework 4.0    .NET Framework   1      deprecated      
.NET Framework 4.5    .NET Framework   1      deprecated      
.NET Framework 4.6.1  .NET Framework   1      deprecated      
.NET Framework 4.6.2  .NET Framework   1      supported       
.NET Framework 4.7.1  .NET Framework   1      supported       
.NET Framework 4.7.2  .NET Framework   2      supported       
.NET Standard 2.0     .NET Standard    1      supported       
(Unknown)             (Unknown)        2      unknown         
Visual Basic 6        Visual Basic 6   1      deprecated      
";

            //Act
            string? contents = DataAccess.GetFrameworkSummary(SamplesPath, includeTotals, file);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
        }
    }

    [TestMethod]
    public void FrameworkSummaryWithTotalsTest()
    {
        //Arrange
        bool includeTotals = true;
        string? file = null;
        if (SamplesPath != null)
        {
            string expected = @"Framework             FrameworkFamily  Count  Status          
--------------------------------------------------------------
.NET 5.0              .NET             1      deprecated      
.NET 6.0              .NET             2      supported       
.NET 6.0-android      .NET             1      supported       
.NET 6.0-ios          .NET             1      supported       
.NET 7.0              .NET             1      supported       
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
.NET Framework 3.5    .NET Framework   1      EOL: 9-Jan-2029 
.NET Framework 4.0    .NET Framework   1      deprecated      
.NET Framework 4.5    .NET Framework   1      deprecated      
.NET Framework 4.6.1  .NET Framework   1      deprecated      
.NET Framework 4.6.2  .NET Framework   1      supported       
.NET Framework 4.7.1  .NET Framework   1      supported       
.NET Framework 4.7.2  .NET Framework   2      supported       
.NET Standard 2.0     .NET Standard    1      supported       
(Unknown)             (Unknown)        2      unknown         
Visual Basic 6        Visual Basic 6   1      deprecated      
total frameworks                       31                     
";

            //Act
            string? contents = DataAccess.GetFrameworkSummary(SamplesPath, includeTotals, file);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
        }
    }


    [TestMethod]
    public void FrameworkSummaryWithTotalsToFileTest()
    {
        //Arrange
        bool includeTotals = true;
        string? file = "test.txt";
        if (SamplesPath != null)
        {
            string expected = @"Framework,FrameworkFamily,Count,Status
.NET 5.0,.NET,1,deprecated
.NET 6.0,.NET,2,supported
.NET 6.0-android,.NET,1,supported
.NET 6.0-ios,.NET,1,supported
.NET 7.0,.NET,1,supported
.NET Core 1.0,.NET Core,1,deprecated
.NET Core 1.1,.NET Core,1,deprecated
.NET Core 2.0,.NET Core,1,deprecated
.NET Core 2.1,.NET Core,1,deprecated
.NET Core 2.2,.NET Core,1,deprecated
.NET Core 3.0,.NET Core,2,deprecated
.NET Core 3.1,.NET Core,3,EOL: 13-Dec-2022
.NET Framework 1.0,.NET Framework,1,deprecated
.NET Framework 1.1,.NET Framework,1,deprecated
.NET Framework 2.0,.NET Framework,1,deprecated
.NET Framework 3.5,.NET Framework,1,EOL: 9-Jan-2029
.NET Framework 4.0,.NET Framework,1,deprecated
.NET Framework 4.5,.NET Framework,1,deprecated
.NET Framework 4.6.1,.NET Framework,1,deprecated
.NET Framework 4.6.2,.NET Framework,1,supported
.NET Framework 4.7.1,.NET Framework,1,supported
.NET Framework 4.7.2,.NET Framework,2,supported
.NET Standard 2.0,.NET Standard,1,supported
(Unknown),(Unknown),2,unknown
Visual Basic 6,Visual Basic 6,1,deprecated
total frameworks,,31,
";

            //Act
            DataAccess.GetFrameworkSummary(SamplesPath, includeTotals, file);
            string contents = File.ReadAllText(Directory.GetCurrentDirectory() + "/" + file);

            //Asset
            //Assert.AreEqual($"Exported results to 'test.txt'" + Environment.NewLine, result);
            Assert.AreEqual(expected.Replace("\\", "/"), contents.Replace("\\", "/"));
        }
    }

    [TestMethod]
    public void RawResultsTest()
    {
        //Arrange
        string? file = null;
        if (SamplesPath != null)
        {
            string expected = @"FileName                                    Path                                                                             FrameworkCode   FrameworkName         Family          Language  Status          
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Assembly-CSharp.csproj                      /Sample.Unity2020/Assembly-CSharp.csproj                                         v4.7.1          .NET Framework 4.7.1  .NET Framework  csharp    supported       
Calculator.csproj                           /Sample.Net6.MAUI.Calculator/src/Calculator/Calculator.csproj                    net6.0-ios      .NET 6.0-ios          .NET            csharp    supported       
Calculator.csproj                           /Sample.Net6.MAUI.Calculator/src/Calculator/Calculator.csproj                    net6.0-android  .NET 6.0-android      .NET            csharp    supported       
dotnetapp-3.5.csproj                        /Sample.NetFramework3.5.WebApp/dotnetapp-3.5.csproj                              v3.5            .NET Framework 3.5    .NET Framework  csharp    EOL: 9-Jan-2029 
hello-world-fsharp.fsproj                   /Sample.NetCore3.0.fsharp.ConsoleApp/hello-world-fsharp.fsproj                   netcoreapp3.0   .NET Core 3.0         .NET Core       fsharp    deprecated      
HelloWorld.fsproj                           /Sample.NETFramework4.6.1FSharp.HelloWorld/HelloWorld.fsproj                     net461          .NET Framework 4.6.1  .NET Framework  fsharp    deprecated      
project.json                                /Sample.NetCore1.0.ConsoleApp/project.json                                       netcoreapp1.0   .NET Core 1.0         .NET Core       csharp    deprecated      
project.json                                /Sample.NetCore1.1.ConsoleApp/project.json                                       netcoreapp1.1   .NET Core 1.1         .NET Core       csharp    deprecated      
Sample.MultipleTargets.ConsoleApp.csproj    /Sample.MultipleTargets.ConsoleApp/Sample.MultipleTargets.ConsoleApp.csproj      netcoreapp3.1   .NET Core 3.1         .NET Core       csharp    EOL: 13-Dec-2022
Sample.MultipleTargets.ConsoleApp.csproj    /Sample.MultipleTargets.ConsoleApp/Sample.MultipleTargets.ConsoleApp.csproj      net462          .NET Framework 4.6.2  .NET Framework  csharp    supported       
Sample.Net5.ConsoleApp.csproj               /Sample.Net5.ConsoleApp/Sample.Net5.ConsoleApp.csproj                            net5.0          .NET 5.0              .NET            csharp    deprecated      
Sample.Net6.ConsoleApp.csproj               /Sample.Net6.ConsoleApp/Sample.Net6.ConsoleApp.csproj                            net6.0          .NET 6.0              .NET            csharp    supported       
Sample.Net6.ConsoleApp.csproj               /Sample.Net6Inception.ConsoleApp/Sample.Net6.ConsoleApp.csproj                   net6.0          .NET 6.0              .NET            csharp    supported       
Sample.Net7.ConsoleApp.csproj               /Sample.Net7.ConsoleApp/Sample.Net7.ConsoleApp.csproj                            net7.0          .NET 7.0              .NET            csharp    supported       
Sample.NetCore.ConsoleApp.csproj            /Sample.NetCore3.1.ConsoleApp/Sample.NetCore.ConsoleApp.csproj                   netcoreapp3.1   .NET Core 3.1         .NET Core       csharp    EOL: 13-Dec-2022
Sample.NetCore2.0.ConsoleApp.csproj         /Sample.NetCore2.0.ConsoleApp/Sample.NetCore2.0.ConsoleApp.csproj                netcoreapp2.0   .NET Core 2.0         .NET Core       csharp    deprecated      
Sample.NetCore2.1.ConsoleApp.csproj         /Sample.NetCore2.1.ConsoleApp/Sample.NetCore2.1.ConsoleApp.csproj                netcoreapp2.1   .NET Core 2.1         .NET Core       csharp    deprecated      
Sample.NetCore2.2.ConsoleApp.csproj         /Sample.NetCore2.2.ConsoleApp/Sample.NetCore2.2.ConsoleApp.csproj                netcoreapp2.2   .NET Core 2.2         .NET Core       csharp    deprecated      
Sample.NetCore3.0.ConsoleApp.csproj         /Sample.NetCore3.0.ConsoleApp/Sample.NetCore3.0.ConsoleApp.csproj                netcoreapp3.0   .NET Core 3.0         .NET Core       csharp    deprecated      
Sample.NetFramework.ConsoleApp.csproj       /Sample.NetFramework.ConsoleApp/Sample.NetFramework.ConsoleApp.csproj            v4.7.2          .NET Framework 4.7.2  .NET Framework  csharp    supported       
Sample.NetFrameworkVBNet.ConsoleApp.vbproj  /Sample.NetFrameworkVBNet.ConsoleApp/Sample.NetFrameworkVBNet.ConsoleApp.vbproj  netcoreapp3.1   .NET Core 3.1         .NET Core       vb.net    EOL: 13-Dec-2022
Sample.NetStandard.Class.csproj             /Sample.NetStandard.Class/Sample.NetStandard.Class.csproj                        netstandard2.0  .NET Standard 2.0     .NET Standard   csharp    supported       
Sample.SSDT.Database.sqlproj                /Sample.SSDT.Database/Sample.SSDT.Database.sqlproj                               v4.7.2          .NET Framework 4.7.2  .NET Framework  csharp    supported       
Sample.VB6.WinApp.vbp                       /Sample.VB6.Calculator/Sample.VB6.WinApp.vbp                                     vb6             Visual Basic 6        Visual Basic 6  vb6       deprecated      
VBProj.vbproj                               /Sample.NetFramework1.0.App/VBProj.vbproj                                        v1.0            .NET Framework 1.0    .NET Framework  vb.net    deprecated      
VBProj.vbproj                               /Sample.NetFramework1.1.App/VBProj.vbproj                                        v1.1            .NET Framework 1.1    .NET Framework  vb.net    deprecated      
VBProj.vbproj                               /Sample.NetFramework2.0.App/VBProj.vbproj                                        v2.0            .NET Framework 2.0    .NET Framework  vb.net    deprecated      
VBProj.vbproj                               /Sample.NetFrameworkInvalid.App/VBProj.vbproj                                                    (Unknown)             (Unknown)       vb.net    unknown         
web.config                                  /Sample.NetFramework3.5.Website/web.config                                                       (Unknown)             (Unknown)       csharp    unknown         
WorldBankSample.csproj                      /Sample.NetFramework40.WebApp/WorldBankSample.csproj                             v4.0            .NET Framework 4.0    .NET Framework  csharp    deprecated      
WorldBankSample.csproj                      /Sample.NetFramework45.WebApp/WorldBankSample.csproj                             v4.5            .NET Framework 4.5    .NET Framework  csharp    deprecated      
";

            //Act
            string? contents = DataAccess.GetRawResults(SamplesPath, file);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
        }
    }

    [TestMethod]
    public void RawResultsWebConfigCountTest()
    {
        //Arrange
        string? file = null;
        int webConfigCount = 0;
        if (SamplesPath != null)
        {
            //Act
            string? contents = DataAccess.GetRawResults(SamplesPath, file);
            if (contents != null)
            {
                string[] lines = contents.Split(Environment.NewLine);
                for (int i = 0; i < lines.Length - 1; i++)
                {
                    if (lines[i].Contains("web.config"))
                    {
                        //Asset
                        webConfigCount++;
                    }
                }
            }

            //Asset
            Assert.IsNotNull(contents);
            Assert.AreEqual(1, webConfigCount);
        }
    }

    [TestMethod]
    public void RawResultsToFileTest()
    {
        //Arrange
        string? file = "test2.txt";
        if (SamplesPath != null)
        {
            string expected = @"FileName,Path,FrameworkCode,FrameworkName,Family,Language,Status
Assembly-CSharp.csproj,/Sample.Unity2020/Assembly-CSharp.csproj,v4.7.1,.NET Framework 4.7.1,.NET Framework,csharp,supported
Calculator.csproj,/Sample.Net6.MAUI.Calculator/src/Calculator/Calculator.csproj,net6.0-ios,.NET 6.0-ios,.NET,csharp,supported
Calculator.csproj,/Sample.Net6.MAUI.Calculator/src/Calculator/Calculator.csproj,net6.0-android,.NET 6.0-android,.NET,csharp,supported
dotnetapp-3.5.csproj,/Sample.NetFramework3.5.WebApp/dotnetapp-3.5.csproj,v3.5,.NET Framework 3.5,.NET Framework,csharp,EOL: 9-Jan-2029
hello-world-fsharp.fsproj,/Sample.NetCore3.0.fsharp.ConsoleApp/hello-world-fsharp.fsproj,netcoreapp3.0,.NET Core 3.0,.NET Core,fsharp,deprecated
HelloWorld.fsproj,/Sample.NETFramework4.6.1FSharp.HelloWorld/HelloWorld.fsproj,net461,.NET Framework 4.6.1,.NET Framework,fsharp,deprecated
project.json,/Sample.NetCore1.0.ConsoleApp/project.json,netcoreapp1.0,.NET Core 1.0,.NET Core,csharp,deprecated
project.json,/Sample.NetCore1.1.ConsoleApp/project.json,netcoreapp1.1,.NET Core 1.1,.NET Core,csharp,deprecated
Sample.MultipleTargets.ConsoleApp.csproj,/Sample.MultipleTargets.ConsoleApp/Sample.MultipleTargets.ConsoleApp.csproj,netcoreapp3.1,.NET Core 3.1,.NET Core,csharp,EOL: 13-Dec-2022
Sample.MultipleTargets.ConsoleApp.csproj,/Sample.MultipleTargets.ConsoleApp/Sample.MultipleTargets.ConsoleApp.csproj,net462,.NET Framework 4.6.2,.NET Framework,csharp,supported
Sample.Net5.ConsoleApp.csproj,/Sample.Net5.ConsoleApp/Sample.Net5.ConsoleApp.csproj,net5.0,.NET 5.0,.NET,csharp,deprecated
Sample.Net6.ConsoleApp.csproj,/Sample.Net6.ConsoleApp/Sample.Net6.ConsoleApp.csproj,net6.0,.NET 6.0,.NET,csharp,supported
Sample.Net6.ConsoleApp.csproj,/Sample.Net6Inception.ConsoleApp/Sample.Net6.ConsoleApp.csproj,net6.0,.NET 6.0,.NET,csharp,supported
Sample.Net7.ConsoleApp.csproj,/Sample.Net7.ConsoleApp/Sample.Net7.ConsoleApp.csproj,net7.0,.NET 7.0,.NET,csharp,supported
Sample.NetCore.ConsoleApp.csproj,/Sample.NetCore3.1.ConsoleApp/Sample.NetCore.ConsoleApp.csproj,netcoreapp3.1,.NET Core 3.1,.NET Core,csharp,EOL: 13-Dec-2022
Sample.NetCore2.0.ConsoleApp.csproj,/Sample.NetCore2.0.ConsoleApp/Sample.NetCore2.0.ConsoleApp.csproj,netcoreapp2.0,.NET Core 2.0,.NET Core,csharp,deprecated
Sample.NetCore2.1.ConsoleApp.csproj,/Sample.NetCore2.1.ConsoleApp/Sample.NetCore2.1.ConsoleApp.csproj,netcoreapp2.1,.NET Core 2.1,.NET Core,csharp,deprecated
Sample.NetCore2.2.ConsoleApp.csproj,/Sample.NetCore2.2.ConsoleApp/Sample.NetCore2.2.ConsoleApp.csproj,netcoreapp2.2,.NET Core 2.2,.NET Core,csharp,deprecated
Sample.NetCore3.0.ConsoleApp.csproj,/Sample.NetCore3.0.ConsoleApp/Sample.NetCore3.0.ConsoleApp.csproj,netcoreapp3.0,.NET Core 3.0,.NET Core,csharp,deprecated
Sample.NetFramework.ConsoleApp.csproj,/Sample.NetFramework.ConsoleApp/Sample.NetFramework.ConsoleApp.csproj,v4.7.2,.NET Framework 4.7.2,.NET Framework,csharp,supported
Sample.NetFrameworkVBNet.ConsoleApp.vbproj,/Sample.NetFrameworkVBNet.ConsoleApp/Sample.NetFrameworkVBNet.ConsoleApp.vbproj,netcoreapp3.1,.NET Core 3.1,.NET Core,vb.net,EOL: 13-Dec-2022
Sample.NetStandard.Class.csproj,/Sample.NetStandard.Class/Sample.NetStandard.Class.csproj,netstandard2.0,.NET Standard 2.0,.NET Standard,csharp,supported
Sample.SSDT.Database.sqlproj,/Sample.SSDT.Database/Sample.SSDT.Database.sqlproj,v4.7.2,.NET Framework 4.7.2,.NET Framework,csharp,supported
Sample.VB6.WinApp.vbp,/Sample.VB6.Calculator/Sample.VB6.WinApp.vbp,vb6,Visual Basic 6,Visual Basic 6,vb6,deprecated
VBProj.vbproj,/Sample.NetFramework1.0.App/VBProj.vbproj,v1.0,.NET Framework 1.0,.NET Framework,vb.net,deprecated
VBProj.vbproj,/Sample.NetFramework1.1.App/VBProj.vbproj,v1.1,.NET Framework 1.1,.NET Framework,vb.net,deprecated
VBProj.vbproj,/Sample.NetFramework2.0.App/VBProj.vbproj,v2.0,.NET Framework 2.0,.NET Framework,vb.net,deprecated
VBProj.vbproj,/Sample.NetFrameworkInvalid.App/VBProj.vbproj,,(Unknown),(Unknown),vb.net,unknown
web.config,/Sample.NetFramework3.5.Website/web.config,,(Unknown),(Unknown),csharp,unknown
WorldBankSample.csproj,/Sample.NetFramework40.WebApp/WorldBankSample.csproj,v4.0,.NET Framework 4.0,.NET Framework,csharp,deprecated
WorldBankSample.csproj,/Sample.NetFramework45.WebApp/WorldBankSample.csproj,v4.5,.NET Framework 4.5,.NET Framework,csharp,deprecated
";

            //Act
            DataAccess.GetRawResults(SamplesPath, file);
            string contents = File.ReadAllText(Directory.GetCurrentDirectory() + "/" + file);

            //Asset
            //Assert.AreEqual($"Exported results to 'test2.txt'" + Environment.NewLine, result);
            Assert.AreEqual(expected.Replace("\\", "/"), contents.Replace("\\", "/"));
        }
    }


}
