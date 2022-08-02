namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class ProjectScanningTests : BaseTests
{
    [TestMethod]
    public void CountSampleFrameworksTest()
    {
        //Arrange
        List<Project>? results = null;

        //Act
        if (SamplesPath != null)
        {
            results = ProjectScanning.SearchDirectory(SamplesPath);
        }

        //Asset
        Assert.IsNotNull(results);
        Assert.AreEqual(21, results.Count);
        Project? project1 = results.FirstOrDefault(d => d.FrameworkCode == "netcoreapp3.1" && d.Language == "csharp");
        Assert.IsNotNull(project1);
        Assert.AreEqual("netcoreapp3.1", project1.FrameworkCode);
        Assert.AreEqual("csharp", project1.Language);
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
            results = ProjectScanning.SearchDirectory(SamplesPath);
        }

        //Asset
        Assert.IsNotNull(results);
        Assert.AreEqual(21, results.Count);
        Project? project1 = results.FirstOrDefault(d => d.FrameworkCode == "netcoreapp3.1");
        Assert.IsNotNull(project1);
        Assert.AreEqual("netcoreapp3.1", project1.FrameworkCode);
        Assert.AreEqual(".NET Core", project1.Family);
        Assert.AreEqual(".NET Core 3.1", project1.FrameworkName);

        Project? project2 = results.FirstOrDefault(d => d.FrameworkCode == "net462");
        Assert.IsNotNull(project2);
        Assert.AreEqual("net462", project2.FrameworkCode);
        Assert.AreEqual(".NET Framework", project2.Family);
        Assert.AreEqual(".NET Framework 4.6.2", project2.FrameworkName);

        Project? project3 = results.FirstOrDefault(d => d.FrameworkCode == "v4.7.2");
        Assert.IsNotNull(project3);
        Assert.AreEqual("v4.7.2", project3.FrameworkCode);
        Assert.AreEqual(".NET Framework", project3.Family);
        Assert.AreEqual(".NET Framework 4.7.2", project3.FrameworkName);

        Project? project4 = results.FirstOrDefault(d => d.FrameworkCode == "net5.0");
        Assert.IsNotNull(project4);
        Assert.AreEqual("net5.0", project4.FrameworkCode);
        Assert.AreEqual(".NET", project4.Family);
        Assert.AreEqual(".NET 5.0", project4.FrameworkName);
    }


}