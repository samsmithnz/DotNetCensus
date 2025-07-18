using DotNetCensus.Tests.Helpers;

namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
[TestCategory("IntegrationTest")]
public class RepoDataAccessTests : RepoBasedTests
{

    [TestMethod]
    public void FrameworkSummaryWithRepoTest()
    {
        //Arrange
        bool includeTotals = true;
        string? directory = null;
        Repo? repo = new("samsmithnz", "DotNetCensus")
        {
            User = GitHubId,
            Password = GitHubSecret,
            Branch = GetCurrentBranch()
        };
        string? file = null;
        if (directory != null || repo != null)
        {
            string expected = @"| Framework            | FrameworkFamily | Count | Status           |
|----------------------|-----------------|-------|------------------|
| .NET 10.0            | .NET            | 1     | in preview       |
| .NET 5.0             | .NET            | 2     | deprecated       |
| .NET 6.0             | .NET            | 4     | deprecated       |
| .NET 6.0-android     | .NET            | 1     | deprecated       |
| .NET 6.0-ios         | .NET            | 1     | deprecated       |
| .NET 6.0-maccatalyst | .NET            | 1     | deprecated       |
| .NET 7.0             | .NET            | 2     | deprecated       |
| .NET 8.0             | .NET            | 9     | supported        |
| .NET 9.0             | .NET            | 2     | supported        |
| .NET Core 1.0        | .NET Core       | 1     | deprecated       |
| .NET Core 1.1        | .NET Core       | 1     | deprecated       |
| .NET Core 2.0        | .NET Core       | 1     | deprecated       |
| .NET Core 2.1        | .NET Core       | 1     | deprecated       |
| .NET Core 2.2        | .NET Core       | 1     | deprecated       |
| .NET Core 3.0        | .NET Core       | 2     | deprecated       |
| .NET Core 3.1        | .NET Core       | 3     | deprecated       |
| .NET Framework 1.0   | .NET Framework  | 1     | deprecated       |
| .NET Framework 1.1   | .NET Framework  | 1     | deprecated       |
| .NET Framework 2.0   | .NET Framework  | 1     | deprecated       |
| .NET Framework 3.0   | .NET Framework  | 1     | deprecated       |
| .NET Framework 3.5   | .NET Framework  | 3     | EOL: 9-Jan-2029  |
| .NET Framework 4.0   | .NET Framework  | 2     | deprecated       |
| .NET Framework 4.5   | .NET Framework  | 1     | deprecated       |
| .NET Framework 4.5.1 | .NET Framework  | 1     | deprecated       |
| .NET Framework 4.5.2 | .NET Framework  | 1     | deprecated       |
| .NET Framework 4.6   | .NET Framework  | 1     | deprecated       |
| .NET Framework 4.6.1 | .NET Framework  | 1     | deprecated       |
| .NET Framework 4.6.2 | .NET Framework  | 1     | EOL: 12-Jan-2027 |
| .NET Framework 4.7   | .NET Framework  | 1     | supported        |
| .NET Framework 4.7.1 | .NET Framework  | 1     | supported        |
| .NET Framework 4.7.2 | .NET Framework  | 3     | supported        |
| .NET Framework 4.8   | .NET Framework  | 1     | supported        |
| .NET Framework 4.8.1 | .NET Framework  | 1     | supported        |
| .NET Standard 1.0    | .NET Standard   | 1     | supported        |
| .NET Standard 1.1    | .NET Standard   | 1     | supported        |
| .NET Standard 1.2    | .NET Standard   | 1     | supported        |
| .NET Standard 1.3    | .NET Standard   | 1     | supported        |
| .NET Standard 1.4    | .NET Standard   | 1     | supported        |
| .NET Standard 1.5    | .NET Standard   | 1     | supported        |
| .NET Standard 1.6    | .NET Standard   | 1     | supported        |
| .NET Standard 2.0    | .NET Standard   | 3     | supported        |
| .NET Standard 2.1    | .NET Standard   | 1     | supported        |
| (Unknown)            | (Unknown)       | 2     | unknown          |
| Visual Basic 6       | Visual Basic 6  | 1     | deprecated       |
| total frameworks     |                 | 69    |                  |
";

            //Act
            string? contents = Main.GetFrameworkSummaryAsString(directory, repo, includeTotals, file);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
        }
    }

    [TestMethod]
    public void FrameworkSummaryWithAzurePipelinesToGitHubActionsConverterRepoTest()
    {
        //Arrange
        bool includeTotals = true;
        string? directory = null;
        Repo? repo = new("samsmithnz", "AzurePipelinesToGitHubActionsConverter")
        {
            User = GitHubId,
            Password = GitHubSecret,
            Branch = "main"
        };
        string? file = null;
        if (directory != null || repo != null)
        {
        string expected = @"| Framework         | FrameworkFamily | Count | Status    |
|-------------------|-----------------|-------|-----------|
| .NET 8.0          | .NET            | 1     | supported |
| .NET Standard 2.0 | .NET Standard   | 1     | supported |
| total frameworks  |                 | 2     |           |
";

            //Act
            string? contents = Main.GetFrameworkSummaryAsString(directory, repo, includeTotals, file);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
        }
    }

    [TestMethod]
    public void FrameworkSummaryWithCurrentRepoAndBranchTest()
    {
        //Arrange
        bool includeTotals = true;
        string? directory = null;
        Repo? repo = new("samsmithnz", "DotNetCensus")
        {
            User = GitHubId,
            Password = GitHubSecret,
            Branch = GetCurrentBranch()
        };
        string? file = null;
        if (directory != null || repo != null)
        {
            string expected = @"| Framework            | FrameworkFamily | Count | Status           |
|----------------------|-----------------|-------|------------------|
| .NET 10.0            | .NET            | 1     | in preview       |
| .NET 5.0             | .NET            | 2     | deprecated       |
| .NET 6.0             | .NET            | 4     | deprecated       |
| .NET 6.0-android     | .NET            | 1     | deprecated       |
| .NET 6.0-ios         | .NET            | 1     | deprecated       |
| .NET 6.0-maccatalyst | .NET            | 1     | deprecated       |
| .NET 7.0             | .NET            | 2     | deprecated       |
| .NET 8.0             | .NET            | 9     | supported        |
| .NET 9.0             | .NET            | 2     | supported        |
| .NET Core 1.0        | .NET Core       | 1     | deprecated       |
| .NET Core 1.1        | .NET Core       | 1     | deprecated       |
| .NET Core 2.0        | .NET Core       | 1     | deprecated       |
| .NET Core 2.1        | .NET Core       | 1     | deprecated       |
| .NET Core 2.2        | .NET Core       | 1     | deprecated       |
| .NET Core 3.0        | .NET Core       | 2     | deprecated       |
| .NET Core 3.1        | .NET Core       | 3     | deprecated       |
| .NET Framework 1.0   | .NET Framework  | 1     | deprecated       |
| .NET Framework 1.1   | .NET Framework  | 1     | deprecated       |
| .NET Framework 2.0   | .NET Framework  | 1     | deprecated       |
| .NET Framework 3.0   | .NET Framework  | 1     | deprecated       |
| .NET Framework 3.5   | .NET Framework  | 3     | EOL: 9-Jan-2029  |
| .NET Framework 4.0   | .NET Framework  | 2     | deprecated       |
| .NET Framework 4.5   | .NET Framework  | 1     | deprecated       |
| .NET Framework 4.5.1 | .NET Framework  | 1     | deprecated       |
| .NET Framework 4.5.2 | .NET Framework  | 1     | deprecated       |
| .NET Framework 4.6   | .NET Framework  | 1     | deprecated       |
| .NET Framework 4.6.1 | .NET Framework  | 1     | deprecated       |
| .NET Framework 4.6.2 | .NET Framework  | 1     | EOL: 12-Jan-2027 |
| .NET Framework 4.7   | .NET Framework  | 1     | supported        |
| .NET Framework 4.7.1 | .NET Framework  | 1     | supported        |
| .NET Framework 4.7.2 | .NET Framework  | 3     | supported        |
| .NET Framework 4.8   | .NET Framework  | 1     | supported        |
| .NET Framework 4.8.1 | .NET Framework  | 1     | supported        |
| .NET Standard 1.0    | .NET Standard   | 1     | supported        |
| .NET Standard 1.1    | .NET Standard   | 1     | supported        |
| .NET Standard 1.2    | .NET Standard   | 1     | supported        |
| .NET Standard 1.3    | .NET Standard   | 1     | supported        |
| .NET Standard 1.4    | .NET Standard   | 1     | supported        |
| .NET Standard 1.5    | .NET Standard   | 1     | supported        |
| .NET Standard 1.6    | .NET Standard   | 1     | supported        |
| .NET Standard 2.0    | .NET Standard   | 3     | supported        |
| .NET Standard 2.1    | .NET Standard   | 1     | supported        |
| (Unknown)            | (Unknown)       | 2     | unknown          |
| Visual Basic 6       | Visual Basic 6  | 1     | deprecated       |
| total frameworks     |                 | 69    |                  |
";

            //Act
            string? contents = Main.GetFrameworkSummaryAsString(directory, repo, includeTotals, file);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
        }
    }

    [TestMethod]
    public void InventoryResultsWithRepoTest()
    {
        //Arrange
        string? directory = null;
        Repo? repo = new("samsmithnz", "DotNetCensus")
        {
            User = GitHubId,
            Password = GitHubSecret,
            Branch = GetCurrentBranch()
        };
        string? file = null;
        if (directory != null || repo != null)
        {
            string expected = @"| Path                                                                                                                   | FileName                                                     | FrameworkCode      | FrameworkName        | Family         | Language | Status           |
|------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------|--------------------|----------------------|----------------|----------|------------------|
| /samples/Sample.fsharp.net35/Sample_VS2017_FSharp_ConsoleApp_net35_old_fsharp_core.fsproj                              | Sample_VS2017_FSharp_ConsoleApp_net35_old_fsharp_core.fsproj | net35              | .NET Framework 3.5   | .NET Framework | fsharp   | EOL: 9-Jan-2029  |
| /samples/Sample.GenericProps.File.NoProjectVariable/src/powershell-win-core/powershell-win-core.csproj                 | powershell-win-core.csproj                                   | net8.0             | .NET 8.0             | .NET           | csharp   | supported        |
| /samples/Sample.GenericProps.File/test/xUnit/xUnit.tests.csproj                                                        | xUnit.tests.csproj                                           | net8.0             | .NET 8.0             | .NET           | csharp   | supported        |
| /samples/Sample.Multiple.Directory.Build.Props/src/tools/illink/src/analyzer/analyzer.csproj                           | analyzer.csproj                                              | net8.0             | .NET 8.0             | .NET           | csharp   | supported        |
| /samples/Sample.Multiple.Directory.Build.Props/src/tools/illink/src/ILLink.CodeFix/ILLink.CodeFixProvider.csproj       | ILLink.CodeFixProvider.csproj                                | netstandard2.0     | .NET Standard 2.0    | .NET Standard  | csharp   | supported        |
| /samples/Sample.Multiple.Directory.Build.Props/src/tools/illink/src/ILLink.RoslynAnalyzer/ILLink.RoslynAnalyzer.csproj | ILLink.RoslynAnalyzer.csproj                                 | netstandard2.0     | .NET Standard 2.0    | .NET Standard  | csharp   | supported        |
| /samples/Sample.Multiple.Directory.Build.Props/src/tools/illink/src/ILLink.Tasks/ILLink.Tasks.csproj                   | ILLink.Tasks.csproj                                          | net472             | .NET Framework 4.7.2 | .NET Framework | csharp   | supported        |
| /samples/Sample.Multiple.Directory.Build.Props/src/tools/illink/src/ILLink.Tasks/ILLink.Tasks.csproj                   | ILLink.Tasks.csproj                                          |                    | (Unknown)            | (Unknown)      | csharp   | unknown          |
| /samples/Sample.Multiple.Directory.Build.Props/src/tools/illink/src/ILLink.Tasks/ILLink.Tasks.csproj                   | ILLink.Tasks.csproj                                          | net8.0             | .NET 8.0             | .NET           | csharp   | supported        |
| /samples/Sample.Multiple.Directory.Build.Props/src/tools/illink/src/linker/Mono.Linker.csproj                          | Mono.Linker.csproj                                           | net8.0             | .NET 8.0             | .NET           | csharp   | supported        |
| /samples/Sample.Multiple.Directory.Build.Props/src/tools/illink/src/tlens/tlens.csproj                                 | tlens.csproj                                                 | net8.0             | .NET 8.0             | .NET           | csharp   | supported        |
| /samples/Sample.MultipleTargets.ConsoleApp/Sample.MultipleTargets.ConsoleApp.csproj                                    | Sample.MultipleTargets.ConsoleApp.csproj                     | netcoreapp3.1      | .NET Core 3.1        | .NET Core      | csharp   | deprecated       |
| /samples/Sample.MultipleTargets.ConsoleApp/Sample.MultipleTargets.ConsoleApp.csproj                                    | Sample.MultipleTargets.ConsoleApp.csproj                     | net5.0             | .NET 5.0             | .NET           | csharp   | deprecated       |
| /samples/Sample.MultipleTargets.ConsoleApp/Sample.MultipleTargets.ConsoleApp.csproj                                    | Sample.MultipleTargets.ConsoleApp.csproj                     | net462             | .NET Framework 4.6.2 | .NET Framework | csharp   | EOL: 12-Jan-2027 |
| /samples/Sample.Net10.ConsoleApp/Sample.Net10.ConsoleApp.csproj                                                        | Sample.Net10.ConsoleApp.csproj                               | net10.0            | .NET 10.0            | .NET           | csharp   | in preview       |
| /samples/Sample.Net5.ConsoleApp/Sample.Net5.ConsoleApp.csproj                                                          | Sample.Net5.ConsoleApp.csproj                                | net5.0             | .NET 5.0             | .NET           | csharp   | deprecated       |
| /samples/Sample.Net6.ConsoleApp/Sample.Net6.ConsoleApp.csproj                                                          | Sample.Net6.ConsoleApp.csproj                                | net6.0             | .NET 6.0             | .NET           | csharp   | deprecated       |
| /samples/Sample.Net6.ConsoleApp2/src/Sample.Net6.ConsoleApp.csproj                                                     | Sample.Net6.ConsoleApp.csproj                                | net6.0             | .NET 6.0             | .NET           | csharp   | deprecated       |
| /samples/Sample.NET6.Directory.Build.props/App1/Sample.Net6.ConsoleApp.csproj                                          | Sample.Net6.ConsoleApp.csproj                                | net6.0             | .NET 6.0             | .NET           | csharp   | deprecated       |
| /samples/Sample.Net6.MAUI.Calculator/src/Calculator/Calculator.csproj                                                  | Calculator.csproj                                            | net6.0-ios         | .NET 6.0-ios         | .NET           | csharp   | deprecated       |
| /samples/Sample.Net6.MAUI.Calculator/src/Calculator/Calculator.csproj                                                  | Calculator.csproj                                            | net6.0-maccatalyst | .NET 6.0-maccatalyst | .NET           | csharp   | deprecated       |
| /samples/Sample.Net6.MAUI.Calculator/src/Calculator/Calculator.csproj                                                  | Calculator.csproj                                            | net6.0-android     | .NET 6.0-android     | .NET           | csharp   | deprecated       |
| /samples/Sample.Net6Inception.ConsoleApp/Sample.Net6.ConsoleApp.csproj                                                 | Sample.Net6.ConsoleApp.csproj                                | net6.0             | .NET 6.0             | .NET           | csharp   | deprecated       |
| /samples/Sample.Net7.ConsoleApp/Sample.Net7.ConsoleApp.csproj                                                          | Sample.Net7.ConsoleApp.csproj                                | net7.0             | .NET 7.0             | .NET           | csharp   | deprecated       |
| /samples/Sample.Net7.ConsoleApp2/Sample.Net7.ConsoleApp2.csproj                                                        | Sample.Net7.ConsoleApp2.csproj                               | net7.0             | .NET 7.0             | .NET           | csharp   | deprecated       |
| /samples/Sample.Net8.ConsoleApp/Sample.Net8.ConsoleApp.csproj                                                          | Sample.Net8.ConsoleApp.csproj                                | net8.0             | .NET 8.0             | .NET           | csharp   | supported        |
| /samples/Sample.Net9.ConsoleApp/Sample.Net9.ConsoleApp.csproj                                                          | Sample.Net9.ConsoleApp.csproj                                | net9.0             | .NET 9.0             | .NET           | csharp   | supported        |
| /samples/Sample.NetCore1.0.ConsoleApp/project.json                                                                     | project.json                                                 | netcoreapp1.0      | .NET Core 1.0        | .NET Core      | csharp   | deprecated       |
| /samples/Sample.NetCore1.1.ConsoleApp/project.json                                                                     | project.json                                                 | netcoreapp1.1      | .NET Core 1.1        | .NET Core      | csharp   | deprecated       |
| /samples/Sample.NetCore2.0.ConsoleApp/Sample.NetCore2.0.ConsoleApp.csproj                                              | Sample.NetCore2.0.ConsoleApp.csproj                          | netcoreapp2.0      | .NET Core 2.0        | .NET Core      | csharp   | deprecated       |
| /samples/Sample.NetCore2.1.ConsoleApp/Sample.NetCore2.1.ConsoleApp.csproj                                              | Sample.NetCore2.1.ConsoleApp.csproj                          | netcoreapp2.1      | .NET Core 2.1        | .NET Core      | csharp   | deprecated       |
| /samples/Sample.NetCore2.2.ConsoleApp/Sample.NetCore2.2.ConsoleApp.csproj                                              | Sample.NetCore2.2.ConsoleApp.csproj                          | netcoreapp2.2      | .NET Core 2.2        | .NET Core      | csharp   | deprecated       |
| /samples/Sample.NetCore3.0.ConsoleApp/Sample.NetCore3.0.ConsoleApp.csproj                                              | Sample.NetCore3.0.ConsoleApp.csproj                          | netcoreapp3.0      | .NET Core 3.0        | .NET Core      | csharp   | deprecated       |
| /samples/Sample.NetCore3.0.fsharp.ConsoleApp/hello-world-fsharp.fsproj                                                 | hello-world-fsharp.fsproj                                    | netcoreapp3.0      | .NET Core 3.0        | .NET Core      | fsharp   | deprecated       |
| /samples/Sample.NetCore3.1.ConsoleApp/Sample.NetCore.ConsoleApp.csproj                                                 | Sample.NetCore.ConsoleApp.csproj                             | netcoreapp3.1      | .NET Core 3.1        | .NET Core      | csharp   | deprecated       |
| /samples/Sample.NetFramework.ClientProfile.ConsoleApp/Sample.NetFramework.ClientProfile.ConsoleApp.csproj              | Sample.NetFramework.ClientProfile.ConsoleApp.csproj          | net40-client       | .NET Framework 4.0   | .NET Framework | csharp   | deprecated       |
| /samples/Sample.NetFramework.ConsoleApp/Sample.NetFramework.ConsoleApp.csproj                                          | Sample.NetFramework.ConsoleApp.csproj                        | v4.7.2             | .NET Framework 4.7.2 | .NET Framework | csharp   | supported        |
| /samples/Sample.NetFramework1.0.App/VBProj.vbproj                                                                      | VBProj.vbproj                                                | v1.0               | .NET Framework 1.0   | .NET Framework | vb.net   | deprecated       |
| /samples/Sample.NetFramework1.1.App/VBProj.vbproj                                                                      | VBProj.vbproj                                                | v1.1               | .NET Framework 1.1   | .NET Framework | vb.net   | deprecated       |
| /samples/Sample.NetFramework2.0.App/VBProj.vbproj                                                                      | VBProj.vbproj                                                | v2.0               | .NET Framework 2.0   | .NET Framework | vb.net   | deprecated       |
| /samples/Sample.NetFramework3.0.WebApp/dotnetapp-3.0.csproj                                                            | dotnetapp-3.0.csproj                                         | v3.0               | .NET Framework 3.0   | .NET Framework | csharp   | deprecated       |
| /samples/Sample.NetFramework3.5.WebApp/dotnetapp-3.5.csproj                                                            | dotnetapp-3.5.csproj                                         | v3.5               | .NET Framework 3.5   | .NET Framework | csharp   | EOL: 9-Jan-2029  |
| /samples/Sample.NetFramework3.5.Website/web.config                                                                     | web.config                                                   | v3.5               | .NET Framework 3.5   | .NET Framework | csharp   | EOL: 9-Jan-2029  |
| /samples/Sample.NETFramework4.6.1FSharp.HelloWorld/HelloWorld.fsproj                                                   | HelloWorld.fsproj                                            | net461             | .NET Framework 4.6.1 | .NET Framework | fsharp   | deprecated       |
| /samples/Sample.NetFramework40.WebApp/Sample.csproj                                                                    | Sample.csproj                                                | v4.0               | .NET Framework 4.0   | .NET Framework | csharp   | deprecated       |
| /samples/Sample.NetFramework45.WebApp/Sample.csproj                                                                    | Sample.csproj                                                | v4.5               | .NET Framework 4.5   | .NET Framework | csharp   | deprecated       |
| /samples/Sample.NetFramework451.WebApp/Sample.csproj                                                                   | Sample.csproj                                                | v4.5.1             | .NET Framework 4.5.1 | .NET Framework | csharp   | deprecated       |
| /samples/Sample.NetFramework452.App/ClassLibrary2/ClassLibrary2.csproj                                                 | ClassLibrary2.csproj                                         | v4.5.2             | .NET Framework 4.5.2 | .NET Framework | csharp   | deprecated       |
| /samples/Sample.NetFramework46.WebApp/Sample.csproj                                                                    | Sample.csproj                                                | v4.6               | .NET Framework 4.6   | .NET Framework | csharp   | deprecated       |
| /samples/Sample.NetFramework47.App/ClassLibrary2/ClassLibrary2.csproj                                                  | ClassLibrary2.csproj                                         | v4.7               | .NET Framework 4.7   | .NET Framework | csharp   | supported        |
| /samples/Sample.NetFramework48.App/ClassLibrary2/ClassLibrary2.csproj                                                  | ClassLibrary2.csproj                                         | v4.8               | .NET Framework 4.8   | .NET Framework | csharp   | supported        |
| /samples/Sample.NetFramework481.App/ClassLibrary2/ClassLibrary2.csproj                                                 | ClassLibrary2.csproj                                         | v4.8.1             | .NET Framework 4.8.1 | .NET Framework | csharp   | supported        |
| /samples/Sample.NetFrameworkInvalid.App/VBProj.vbproj                                                                  | VBProj.vbproj                                                |                    | (Unknown)            | (Unknown)      | vb.net   | unknown          |
| /samples/Sample.NetFrameworkVBNet.ConsoleApp/Sample.NetFrameworkVBNet.ConsoleApp.vbproj                                | Sample.NetFrameworkVBNet.ConsoleApp.vbproj                   | netcoreapp3.1      | .NET Core 3.1        | .NET Core      | vb.net   | deprecated       |
| /samples/Sample.NetStandard1.0.Class/Sample.NetStandard.Class.csproj                                                   | Sample.NetStandard.Class.csproj                              | netstandard1.0     | .NET Standard 1.0    | .NET Standard  | csharp   | supported        |
| /samples/Sample.NetStandard1.1.Class/Sample.NetStandard.Class.csproj                                                   | Sample.NetStandard.Class.csproj                              | netstandard1.1     | .NET Standard 1.1    | .NET Standard  | csharp   | supported        |
| /samples/Sample.NetStandard1.2.Class/Sample.NetStandard.Class.csproj                                                   | Sample.NetStandard.Class.csproj                              | netstandard1.2     | .NET Standard 1.2    | .NET Standard  | csharp   | supported        |
| /samples/Sample.NetStandard1.3.Class/Sample.NetStandard.Class.csproj                                                   | Sample.NetStandard.Class.csproj                              | netstandard1.3     | .NET Standard 1.3    | .NET Standard  | csharp   | supported        |
| /samples/Sample.NetStandard1.4.Class/Sample.NetStandard.Class.csproj                                                   | Sample.NetStandard.Class.csproj                              | netstandard1.4     | .NET Standard 1.4    | .NET Standard  | csharp   | supported        |
| /samples/Sample.NetStandard1.5.Class/Sample.NetStandard.Class.csproj                                                   | Sample.NetStandard.Class.csproj                              | netstandard1.5     | .NET Standard 1.5    | .NET Standard  | csharp   | supported        |
| /samples/Sample.NetStandard1.6.Class/Sample.NetStandard.Class.csproj                                                   | Sample.NetStandard.Class.csproj                              | netstandard1.6     | .NET Standard 1.6    | .NET Standard  | csharp   | supported        |
| /samples/Sample.NetStandard2.0.Class/Sample.NetStandard.Class.csproj                                                   | Sample.NetStandard.Class.csproj                              | netstandard2.0     | .NET Standard 2.0    | .NET Standard  | csharp   | supported        |
| /samples/Sample.NetStandard2.1.Class/Sample.NetStandard.Class.csproj                                                   | Sample.NetStandard.Class.csproj                              | netstandard2.1     | .NET Standard 2.1    | .NET Standard  | csharp   | supported        |
| /samples/Sample.SSDT.Database/Sample.SSDT.Database.sqlproj                                                             | Sample.SSDT.Database.sqlproj                                 | v4.7.2             | .NET Framework 4.7.2 | .NET Framework | csharp   | supported        |
| /samples/Sample.Unity2020/Assembly-CSharp.csproj                                                                       | Assembly-CSharp.csproj                                       | v4.7.1             | .NET Framework 4.7.1 | .NET Framework | csharp   | supported        |
| /samples/Sample.VB6.Calculator/Sample.VB6.WinApp.vbp                                                                   | Sample.VB6.WinApp.vbp                                        | vb6                | Visual Basic 6       | Visual Basic 6 | vb6      | deprecated       |
| /src/DotNetCensus.Core/DotNetCensus.Core.csproj                                                                        | DotNetCensus.Core.csproj                                     | net8.0             | .NET 8.0             | .NET           | csharp   | supported        |
| /src/DotNetCensus.Tests/DotNetCensus.Tests.csproj                                                                      | DotNetCensus.Tests.csproj                                    | net9.0             | .NET 9.0             | .NET           | csharp   | supported        |
| /src/DotNetCensus/DotNetCensus.csproj                                                                                  | DotNetCensus.csproj                                          | net8.0             | .NET 8.0             | .NET           | csharp   | supported        |
";

            //Act
            string? contents = Main.GetInventoryResultsAsString(directory, repo, file);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Replace("\\", "/"), contents?.Replace("\\", "/"));
        }
    }

    [TestMethod]
    public void FrameworkSummaryWithPrivateRepoTest()
    {
        //Arrange
        bool includeTotals = true;
        string? directory = null;
        Repo? repo = new("SamSmithNZ-dotcom", "SSNZPOC")
        {
            User = GitHubId,
            Password = GitHubSecret
        };
        string? file = null;
        if (directory != null || repo != null)
        {
            string expected = @"| Framework        | FrameworkFamily | Count | Status     |
|------------------|-----------------|-------|------------|
| .NET 5.0         | .NET            | 4     | deprecated |
| .NET 5.0-windows | .NET            | 2     | deprecated |
| total frameworks |                 | 6     |            |
";

            //Act
            string? contents = Main.GetFrameworkSummaryAsString(directory, repo, includeTotals, file);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.Replace("\\", " /"), contents?.Replace("\\", "/"));
        }
    }


}
