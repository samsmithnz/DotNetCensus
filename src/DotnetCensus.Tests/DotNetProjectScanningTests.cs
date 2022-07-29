using DotNetCensus.Core.Models;

namespace DotNetCensus.Tests;

[TestClass]
public class DotNetProjectScanningTests
{
    [TestMethod]
    public void CountSampleFrameworksTest()
    {
        //Arrange
        DirectoryInfo? currentWorkingPath = new(Directory.GetCurrentDirectory());
        string samplesPath = Path.Combine(currentWorkingPath.Parent.Parent.Parent.Parent.Parent.FullName, "samples"); ;

        //Act
        List<Project> results = DotNetProjectScanning.SearchFolder(samplesPath);

        //Asset
        Assert.IsNotNull(results);
        Assert.AreEqual(9, results.Count);
        Project project1 = results[0];
        Assert.IsNotNull(project1);
        Assert.AreEqual(project1.Framework, "netcoreapp3.1");
        Assert.AreEqual(project1.Language, "csharp");
        Assert.IsTrue(project1.Path.Length > 0);
        Assert.IsTrue(project1.FileName.Length > 0);
        Assert.IsTrue(project1.Color.Length > 0);
    }
}