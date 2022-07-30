namespace DotNetCensus.Tests;

[TestClass]
public class DotNetProjectScanningTests : BaseTests
{
    [TestMethod]
    public void CountSampleFrameworksTest()
    {
        //Arrange
        List<Project>? results = null;

        //Act
        if (SamplesPath != null)
        {
            results = DotNetProjectScanning.SearchDirectory(SamplesPath);
        }

        //Asset
        Assert.IsNotNull(results);
        Assert.AreEqual(9, results.Count);
        Project project1 = results[0];
        Assert.IsNotNull(project1);
        Assert.AreEqual(project1.Framework, "netcoreapp3.1");
        Assert.AreEqual(project1.Language, "csharp");
        Assert.IsTrue(project1.Path.Length > 0);
        Assert.IsTrue(project1.FileName.Length > 0);
        Assert.IsTrue(project1.Color?.Length > 0);
    }
}