using DotNetCensus.Core.Models.GitHub;
using DotNetCensus.Tests.Helpers;

namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class RepoDirectoryTests : RepoBasedTests
{

    [TestMethod]
    public void RepoDirectoryConstructionBaseDirTest()
    {
        //Arrange
        List<Project> projects = new() {
            new Project()
            {
                FileName = "Sample.NetCore.ConsoleApp.csproj",
                Path = "/Sample.NetCore.ConsoleApp.csproj"
            }
        };

        //Act
        RepoDirectory dir = ProjectScanning.CreateRepoDirectoryStructure(projects);

        //Asset
        Assert.IsNotNull(dir);
        Assert.AreEqual("Root", dir.Name);
        Assert.AreEqual(0, dir.Directories.Count);
        Assert.AreEqual(1, dir.Files.Count);
        Assert.AreEqual("Sample.NetCore.ConsoleApp.csproj", dir.Files[0]);
    }

    [TestMethod]
    public void RepoDirectoryConstructionSingleDirTest()
    {
        //Arrange
        List<Project> projects = new() {
            new Project()
            {
                FileName = "Sample.NetCore.ConsoleApp.csproj",
                Path = "samples/Sample.NetCore.ConsoleApp.csproj"
            }
        };

        //Act
        RepoDirectory dir = ProjectScanning.CreateRepoDirectoryStructure(projects);

        //Asset
        Assert.IsNotNull(dir);
        Assert.AreEqual("Root", dir.Name);
        Assert.AreEqual(1, dir.Directories.Count);
        Assert.AreEqual(0, dir.Files.Count);
        Assert.AreEqual("samples", dir.Directories[0].Name);
        Assert.AreEqual(1, dir.Directories[0].Files.Count);
        Assert.AreEqual("Sample.NetCore.ConsoleApp.csproj", dir.Directories[0].Files[0]);
    }

    [TestMethod]
    public void RepoDirectoryConstructionMultipleFilesTest()
    {
        //Arrange
        List<Project> projects = new() {

            new Project()
            {
                FileName = "Sample.NetCore.ConsoleApp1.csproj",
                Path = "samples/Sample.NetCore.ConsoleApp1.csproj"
            },
            new Project()
            {
                FileName = "Sample.NetCore.ConsoleApp2.csproj",
                Path = "samples/abc/Sample.NetCore.ConsoleApp2.csproj"
            },
            new Project()
            {
                FileName = "Sample.NetCore.ConsoleApp3.csproj",
                Path = "samples/def/Sample.NetCore.ConsoleApp3.csproj"
            }
        };

        //Act
        RepoDirectory dir = ProjectScanning.CreateRepoDirectoryStructure(projects);

        //Asset
        Assert.IsNotNull(dir);
        Assert.AreEqual("Root", dir.Name);
        Assert.AreEqual(1, dir.Directories.Count);
        Assert.AreEqual(0, dir.Files.Count);
        //Assert.AreEqual("abc", dir.Directories[0].Name);
        //Assert.AreEqual("def", dir.Directories[1].Name);
        //Assert.AreEqual(1, dir.Directories[0].Files.Count);
        //Assert.AreEqual("Sample.NetCore.ConsoleApp2.csproj", dir.Directories[0].Files[0]);
    }

    [TestMethod]
    public void RepoDirectoryConstructionDeepDirTest()
    {
        //Arrange
        List<Project> projects = new() {
            new Project()
            {
                FileName = "Sample.NetCore.ConsoleApp.csproj",
                Path = "samples/abc/def/ghi/Sample.NetCore.ConsoleApp.csproj"
            }
        };

        //Act
        RepoDirectory dir = ProjectScanning.CreateRepoDirectoryStructure(projects);

        //Asset
        Assert.IsNotNull(dir);
        Assert.AreEqual("Root", dir.Name);
        Assert.AreEqual(1, dir.Directories.Count);
        Assert.AreEqual(0, dir.Files.Count);
        //RepoDirectory abc = dir.Directories[0];
        //Assert.AreEqual("abc", abc.Name);
        //Assert.AreEqual(0, abc.Files.Count);
        //Assert.AreEqual(1, abc.Directories.Count);
        //RepoDirectory def = abc.Directories[0];
        //Assert.AreEqual("def", def.Name);
        //Assert.AreEqual(0, def.Files.Count);
        //Assert.AreEqual(1, def.Directories.Count);
        //RepoDirectory ghi = def.Directories[0];
        //Assert.AreEqual("ghi", ghi.Name);
        //Assert.AreEqual(0, ghi.Files.Count);
        //Assert.AreEqual(1, ghi.Directories.Count);
        //Assert.AreEqual("Sample.NetCore.ConsoleApp.csproj", ghi.Directories[0].Files[0]);
    }

}
