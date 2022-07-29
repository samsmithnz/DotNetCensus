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

    }
}