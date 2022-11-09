using DotNetCensus.Tests.Helpers;

namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
[TestCategory("IntegrationTest")]
public class ConsoleDirectoryAppTests : DirectoryBasedTests
{

    [TestMethod]
    public void RunSamplesWithInventoryResultsToFileTest()
    {
        //Arrange
        if (SamplesPath != null)
        {
            string file = "test2.txt";
            string[] parameters = new string[] { "-d", SamplesPath, "-i", "-f", file };
            StringWriter sw = new();
            string expected = @"Path,FileName,FrameworkCode,FrameworkName,Family,Language,Status
/Sample.MultipleTargets.ConsoleApp/Sample.MultipleTargets.ConsoleApp.csproj,Sample.MultipleTargets.ConsoleApp.csproj,netcoreapp3.1,.NET Core 3.1,.NET Core,csharp,EOL: 13-Dec-2022
/Sample.MultipleTargets.ConsoleApp/Sample.MultipleTargets.ConsoleApp.csproj,Sample.MultipleTargets.ConsoleApp.csproj,net462,.NET Framework 4.6.2,.NET Framework,csharp,supported
/Sample.Net5.ConsoleApp/Sample.Net5.ConsoleApp.csproj,Sample.Net5.ConsoleApp.csproj,net5.0,.NET 5.0,.NET,csharp,deprecated
/Sample.Net6.ConsoleApp/Sample.Net6.ConsoleApp.csproj,Sample.Net6.ConsoleApp.csproj,net6.0,.NET 6.0,.NET,csharp,supported
/Sample.Net6.ConsoleApp2/src/Sample.Net6.ConsoleApp.csproj,Sample.Net6.ConsoleApp.csproj,net6.0,.NET 6.0,.NET,csharp,supported
/Sample.NET6.Directory.Build.props/App1/Sample.Net6.ConsoleApp.csproj,Sample.Net6.ConsoleApp.csproj,net6.0,.NET 6.0,.NET,csharp,supported
/Sample.Net6.MAUI.Calculator/src/Calculator/Calculator.csproj,Calculator.csproj,net6.0-ios,.NET 6.0-ios,.NET,csharp,supported
/Sample.Net6.MAUI.Calculator/src/Calculator/Calculator.csproj,Calculator.csproj,net6.0-android,.NET 6.0-android,.NET,csharp,supported
/Sample.Net6Inception.ConsoleApp/Sample.Net6.ConsoleApp.csproj,Sample.Net6.ConsoleApp.csproj,net6.0,.NET 6.0,.NET,csharp,supported
/Sample.Net7.ConsoleApp/Sample.Net7.ConsoleApp.csproj,Sample.Net7.ConsoleApp.csproj,net7.0,.NET 7.0,.NET,csharp,in preview
/Sample.Net8.ConsoleApp/Sample.Net8.ConsoleApp.csproj,Sample.Net8.ConsoleApp.csproj,net8.0,.NET 8.0,.NET,csharp,in preview
/Sample.NetCore1.0.ConsoleApp/project.json,project.json,netcoreapp1.0,.NET Core 1.0,.NET Core,csharp,deprecated
/Sample.NetCore1.1.ConsoleApp/project.json,project.json,netcoreapp1.1,.NET Core 1.1,.NET Core,csharp,deprecated
/Sample.NetCore2.0.ConsoleApp/Sample.NetCore2.0.ConsoleApp.csproj,Sample.NetCore2.0.ConsoleApp.csproj,netcoreapp2.0,.NET Core 2.0,.NET Core,csharp,deprecated
/Sample.NetCore2.1.ConsoleApp/Sample.NetCore2.1.ConsoleApp.csproj,Sample.NetCore2.1.ConsoleApp.csproj,netcoreapp2.1,.NET Core 2.1,.NET Core,csharp,deprecated
/Sample.NetCore2.2.ConsoleApp/Sample.NetCore2.2.ConsoleApp.csproj,Sample.NetCore2.2.ConsoleApp.csproj,netcoreapp2.2,.NET Core 2.2,.NET Core,csharp,deprecated
/Sample.NetCore3.0.ConsoleApp/Sample.NetCore3.0.ConsoleApp.csproj,Sample.NetCore3.0.ConsoleApp.csproj,netcoreapp3.0,.NET Core 3.0,.NET Core,csharp,deprecated
/Sample.NetCore3.0.fsharp.ConsoleApp/hello-world-fsharp.fsproj,hello-world-fsharp.fsproj,netcoreapp3.0,.NET Core 3.0,.NET Core,fsharp,deprecated
/Sample.NetCore3.1.ConsoleApp/Sample.NetCore.ConsoleApp.csproj,Sample.NetCore.ConsoleApp.csproj,netcoreapp3.1,.NET Core 3.1,.NET Core,csharp,EOL: 13-Dec-2022
/Sample.NetFramework.ConsoleApp/Sample.NetFramework.ConsoleApp.csproj,Sample.NetFramework.ConsoleApp.csproj,v4.7.2,.NET Framework 4.7.2,.NET Framework,csharp,supported
/Sample.NetFramework1.0.App/VBProj.vbproj,VBProj.vbproj,v1.0,.NET Framework 1.0,.NET Framework,vb.net,deprecated
/Sample.NetFramework1.1.App/VBProj.vbproj,VBProj.vbproj,v1.1,.NET Framework 1.1,.NET Framework,vb.net,deprecated
/Sample.NetFramework2.0.App/VBProj.vbproj,VBProj.vbproj,v2.0,.NET Framework 2.0,.NET Framework,vb.net,deprecated
/Sample.NetFramework3.5.WebApp/dotnetapp-3.5.csproj,dotnetapp-3.5.csproj,v3.5,.NET Framework 3.5,.NET Framework,csharp,EOL: 9-Jan-2029
/Sample.NetFramework3.5.Website/web.config,web.config,v3.5,.NET Framework 3.5,.NET Framework,csharp,EOL: 9-Jan-2029
/Sample.NETFramework4.6.1FSharp.HelloWorld/HelloWorld.fsproj,HelloWorld.fsproj,net461,.NET Framework 4.6.1,.NET Framework,fsharp,deprecated
/Sample.NetFramework40.WebApp/WorldBankSample.csproj,WorldBankSample.csproj,v4.0,.NET Framework 4.0,.NET Framework,csharp,deprecated
/Sample.NetFramework45.WebApp/WorldBankSample.csproj,WorldBankSample.csproj,v4.5,.NET Framework 4.5,.NET Framework,csharp,deprecated
/Sample.NetFrameworkInvalid.App/VBProj.vbproj,VBProj.vbproj,,(Unknown),(Unknown),vb.net,unknown
/Sample.NetFrameworkVBNet.ConsoleApp/Sample.NetFrameworkVBNet.ConsoleApp.vbproj,Sample.NetFrameworkVBNet.ConsoleApp.vbproj,netcoreapp3.1,.NET Core 3.1,.NET Core,vb.net,EOL: 13-Dec-2022
/Sample.NetStandard.Class/Sample.NetStandard.Class.csproj,Sample.NetStandard.Class.csproj,netstandard2.0,.NET Standard 2.0,.NET Standard,csharp,supported
/Sample.SSDT.Database/Sample.SSDT.Database.sqlproj,Sample.SSDT.Database.sqlproj,v4.7.2,.NET Framework 4.7.2,.NET Framework,csharp,supported
/Sample.Unity2020/Assembly-CSharp.csproj,Assembly-CSharp.csproj,v4.7.1,.NET Framework 4.7.1,.NET Framework,csharp,supported
/Sample.VB6.Calculator/Sample.VB6.WinApp.vbp,Sample.VB6.WinApp.vbp,vb6,Visual Basic 6,Visual Basic 6,vb6,deprecated
";

            //Act
            Console.SetOut(sw);
            Program.Main(parameters);
            string result = sw.ToString();
            sw.Close();
            string contents = File.ReadAllText(Directory.GetCurrentDirectory() + "/" + file);

            //Asset
            Assert.AreEqual($"Exported results to 'test2.txt'" + Environment.NewLine, result);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));

        }
    }


    [TestMethod]
    public void RunSamplesWithTotalsToFileTest()
    {
        //Arrange
        if (SamplesPath != null)
        {
            string file = "test.txt";
            string[] parameters = new string[] { "-d", SamplesPath, "-t", "-f", file };
            StringWriter sw = new();
            string expected = @"Framework,FrameworkFamily,Count,Status
.NET 5.0,.NET,1,deprecated
.NET 6.0,.NET,4,supported
.NET 6.0-android,.NET,1,supported
.NET 6.0-ios,.NET,1,supported
.NET 7.0,.NET,1,supported
.NET 8.0,.NET,1,in preview
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
.NET Framework 3.5,.NET Framework,2,EOL: 9-Jan-2029
.NET Framework 4.0,.NET Framework,1,deprecated
.NET Framework 4.5,.NET Framework,1,deprecated
.NET Framework 4.6.1,.NET Framework,1,deprecated
.NET Framework 4.6.2,.NET Framework,1,supported
.NET Framework 4.7.1,.NET Framework,1,supported
.NET Framework 4.7.2,.NET Framework,2,supported
.NET Standard 2.0,.NET Standard,1,supported
(Unknown),(Unknown),1,unknown
Visual Basic 6,Visual Basic 6,1,deprecated
total frameworks,,34,
";

            //Act
            Console.SetOut(sw);
            Program.Main(parameters);
            string result = sw.ToString();
            sw.Close();
            string contents = File.ReadAllText(Directory.GetCurrentDirectory() + "/" + file);

            //Asset
            Assert.AreEqual($"Exported results to 'test.txt'" + Environment.NewLine, result);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
        }
    }

}
