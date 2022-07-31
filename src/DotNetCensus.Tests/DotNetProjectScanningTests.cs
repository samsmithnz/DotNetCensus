namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
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
        Assert.AreEqual(17, results.Count);
        Project? project1 = results.FirstOrDefault(d => d.Framework == "netcoreapp3.1");
        Assert.IsNotNull(project1);
        Assert.AreEqual(project1.Framework, "netcoreapp3.1");
        Assert.AreEqual(project1.Language, "csharp");
        Assert.IsTrue(project1.Path?.Length > 0);
        Assert.IsTrue(project1.FileName?.Length > 0);
        Assert.IsTrue(project1.Color?.Length > 0);
    }
    
    [TestMethod]
    public void FriendlyNameTest()
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
        Assert.AreEqual(17, results.Count);
        Project? project1 = results.FirstOrDefault(d => d.Framework == "netcoreapp3.1");
        Assert.IsNotNull(project1);
        Assert.AreEqual(project1.Framework, "netcoreapp3.1");
        Assert.AreEqual(project1.Language, "csharp");
        Assert.AreEqual(project1.Family, ".NET Core");
        Assert.AreEqual(project1.FriendlyName, ".NET Core 3.1");
    }


}