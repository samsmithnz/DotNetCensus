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
        Assert.AreEqual(14, results.Count);
        Project? project1 = results.FirstOrDefault(d => d.Framework == "netcoreapp3.1");
        Assert.IsNotNull(project1);
        Assert.AreEqual(project1.Framework, "netcoreapp3.1");
        Assert.AreEqual(project1.Language, "csharp");
        Assert.IsTrue(project1.Path?.Length > 0);
        Assert.IsTrue(project1.FileName?.Length > 0);
        Assert.IsTrue(project1.Color?.Length > 0);
    }

    [TestMethod]
    public void AggregateSampleFrameworksTest()
    {
        //Arrange
        List<FrameworkSummary>? results = null;

        //Act
        if (SamplesPath != null)
        {
            List<Project> projects = DotNetProjectScanning.SearchDirectory(SamplesPath);
            results = Census.AggregateFrameworks(projects, true);
        }

        //Asset
        Assert.IsNotNull(results);
        Assert.AreEqual(13, results.Count);
        //Project project1 = results.FirstOrDefault(d => d.Framework == "netcoreapp3.1");
        //Assert.IsNotNull(project1);
        //Assert.AreEqual(project1.Framework, "netcoreapp3.1");
        //Assert.AreEqual(project1.Language, "csharp");
        //Assert.IsTrue(project1.Path?.Length > 0);
        //Assert.IsTrue(project1.FileName?.Length > 0);
        //Assert.IsTrue(project1.Color?.Length > 0);
    }

    [TestMethod]
    public void AggregateSampleLanguagesTest()
    {
        //Arrange
        List<LanguageSummary>? results = null;

        //Act
        if (SamplesPath != null)
        {
            List<Project> projects = DotNetProjectScanning.SearchDirectory(SamplesPath);
            results = Census.AggregateLanguages(projects, true);
        }

        //Asset
        Assert.IsNotNull(results);
        Assert.AreEqual(4, results.Count);
        //Project project1 = results.FirstOrDefault(d => d.Framework == "netcoreapp3.1");
        //Assert.IsNotNull(project1);
        //Assert.AreEqual(project1.Framework, "netcoreapp3.1");
        //Assert.AreEqual(project1.Language, "csharp");
        //Assert.IsTrue(project1.Path?.Length > 0);
        //Assert.IsTrue(project1.FileName?.Length > 0);
        //Assert.IsTrue(project1.Color?.Length > 0);
    }
}