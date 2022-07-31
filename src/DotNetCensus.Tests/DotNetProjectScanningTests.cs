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
    public void AggregateSampleFrameworksWithTotalTest()
    {
        //Arrange
        List<FrameworkSummary>? results = null;
        bool includeTotal = true;

        //Act
        if (SamplesPath != null)
        {
            List<Project> projects = DotNetProjectScanning.SearchDirectory(SamplesPath);
            results = Census.AggregateFrameworks(projects, includeTotal);
        }

        //Asset
        Assert.IsNotNull(results);
        Assert.AreEqual(13, results.Count);
        Assert.AreEqual(1, results[0].Count);
        Assert.AreEqual("(Unknown framework)", results[0].Framework);
        Assert.AreEqual(1, results[^2].Count);
        Assert.AreEqual("vb6", results[^2].Framework);
        Assert.AreEqual(14, results[^1].Count);
        Assert.AreEqual("total frameworks", results[^1].Framework);
    }

    [TestMethod]
    public void AggregateSampleFrameworksNoTotalTest()
    {
        //Arrange
        List<FrameworkSummary>? results = null;
        bool includeTotal = false;

        //Act
        if (SamplesPath != null)
        {
            List<Project> projects = DotNetProjectScanning.SearchDirectory(SamplesPath);
            results = Census.AggregateFrameworks(projects, includeTotal);
        }

        //Asset
        Assert.IsNotNull(results);
        Assert.AreEqual(12, results.Count);
        Assert.AreEqual(1, results[0].Count);
        Assert.AreEqual("(Unknown framework)", results[0].Framework);
        Assert.AreEqual(1, results[^1].Count);
        Assert.AreEqual("vb6", results[^1].Framework);
    }

    [TestMethod]
    public void AggregateSampleLanguagesWithTotalTest()
    {
        //Arrange
        List<LanguageSummary>? results = null;
        bool includeTotal = true;

        //Act
        if (SamplesPath != null)
        {
            List<Project> projects = DotNetProjectScanning.SearchDirectory(SamplesPath);
            results = Census.AggregateLanguages(projects, includeTotal);
        }

        //Asset
        Assert.IsNotNull(results);
        Assert.AreEqual(4, results.Count);
        Assert.AreEqual(8,results[0].Count);
        Assert.AreEqual("csharp", results[0].Language);
        Assert.AreEqual(5, results[1].Count);
        Assert.AreEqual("vb.net", results[1].Language);
        Assert.AreEqual(1, results[2].Count);
        Assert.AreEqual("vb6", results[2].Language);
        Assert.AreEqual(14, results[3].Count);
        Assert.AreEqual("total languages", results[3].Language);
    }

    [TestMethod]
    public void AggregateSampleLanguagesNoTotalTest()
    {
        //Arrange
        List<LanguageSummary>? results = null;
        bool includeTotal = false;

        //Act
        if (SamplesPath != null)
        {
            List<Project> projects = DotNetProjectScanning.SearchDirectory(SamplesPath);
            results = Census.AggregateLanguages(projects, includeTotal);
        }

        //Asset
        Assert.IsNotNull(results);
        Assert.AreEqual(3, results.Count);
        Assert.AreEqual(8,results[0].Count);
        Assert.AreEqual("csharp", results[0].Language);
        Assert.AreEqual(5, results[1].Count);
        Assert.AreEqual("vb.net", results[1].Language);
        Assert.AreEqual(1, results[2].Count);
        Assert.AreEqual("vb6", results[2].Language);
    }
}