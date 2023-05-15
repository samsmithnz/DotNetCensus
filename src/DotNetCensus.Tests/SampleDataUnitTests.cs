using DotNetCensus.Core.Projects;
using DotNetCensus.Tests.Helpers;

namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
[TestCategory("UnitTest")]
public class SampleDataUnitTests : DirectoryBasedTests
{

    [TestMethod]
    public void AggregateFrameworksTest()
    {
        //Arrange
        List<Project> projects = GenerateSampleData();
        bool includeTotal = true;

        //Act
        List<FrameworkSummary> results = Census.AggregateFrameworks(projects, includeTotal);

        //Asset
        Assert.AreEqual(9, results.Count);
        //Assert.AreEqual("", results.Find(i => i.Framework == "netstandard2.0"));
        Assert.AreEqual(2, results.Find(i => i.Framework == ".NET Standard 2.0")?.Count);
        Assert.AreEqual(2, results.Find(i => i.FrameworkFamily == ".NET Standard")?.Count);
        Assert.AreEqual(1, results.Find(i => i.Framework == ".NET Core 3.1")?.Count);
        Assert.AreEqual(1, results.Find(i => i.FrameworkFamily == ".NET Core")?.Count);
        Assert.AreEqual(2, results.Find(i => i.Framework == ".NET Framework 4.5")?.Count);
        Assert.AreEqual(1, results.Find(i => i.FrameworkFamily == ".NET Framework")?.Count);
        Assert.AreEqual(11, results[^1].Count);
    }

    [TestMethod]
    public void AggregateLanguagesTest()
    {
        //Arrange
        List<Project> projects = GenerateSampleData();
        bool includeTotal = true;

        //Act
        List<LanguageSummary> results = Census.AggregateLanguages(projects, includeTotal);

        //Asset
        Assert.AreEqual(7, results.Count);
        Assert.AreEqual(6, results.Find(i => i.Language == "csharp")?.Count);
        Assert.AreEqual(1, results.Find(i => i.Language == "vb.net")?.Count);
        Assert.AreEqual(1, results.Find(i => i.Language == "fsharp")?.Count);
        Assert.AreEqual(11, results[^1].Count);
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
            List<Project> projects = DirectoryScanning.SearchDirectory(SamplesPath);
            results = Census.AggregateFrameworks(projects, includeTotal);
        }

        //Asset
        Assert.IsNotNull(results);
        Assert.AreEqual(38, results.Count);
        Assert.AreEqual(2, results[0].Count);
        Assert.AreEqual(".NET 5.0", results[0].Framework);
        Assert.AreEqual(1, results[^2].Count);
        Assert.AreEqual("Visual Basic 6", results[^2].Framework);
        Assert.AreEqual(51, results[^1].Count);
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
            List<Project> projects = DirectoryScanning.SearchDirectory(SamplesPath);
            results = Census.AggregateFrameworks(projects, includeTotal);
        }

        //Asset
        Assert.IsNotNull(results);
        Assert.AreEqual(37, results.Count);
        Assert.AreEqual(2, results[0].Count);
        Assert.AreEqual(".NET 5.0", results[0].Framework);
        Assert.AreEqual(1, results[^1].Count);
        Assert.AreEqual("Visual Basic 6", results[^1].Framework);
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
            List<Project> projects = DirectoryScanning.SearchDirectory(SamplesPath);
            results = Census.AggregateLanguages(projects, includeTotal);
        }

        //Asset
        Assert.IsNotNull(results);
        Assert.AreEqual(4, results.Count);
        Assert.AreEqual(50, results[0].Count);
        Assert.AreEqual("csharp", results[0].Language);
        Assert.AreEqual(3, results[1].Count);
        Assert.AreEqual("fsharp", results[1].Language);
        Assert.AreEqual(5, results[2].Count);
        Assert.AreEqual("vb.net", results[2].Language);
        Assert.AreEqual(1, results[3].Count);
        Assert.AreEqual("vb6", results[3].Language);
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
            List<Project> projects = DirectoryScanning.SearchDirectory(SamplesPath);
            results = Census.AggregateLanguages(projects, includeTotal);
        }

        //Asset
        Assert.IsNotNull(results);
        Assert.AreEqual(5, results.Count);
        Assert.AreEqual(42, results[0].Count);
        Assert.AreEqual("csharp", results[0].Language);
        Assert.AreEqual(3, results[1].Count);
        Assert.AreEqual("fsharp", results[1].Language);
        Assert.AreEqual(5, results[2].Count);
        Assert.AreEqual("vb.net", results[2].Language);
        Assert.AreEqual(1, results[3].Count);
        Assert.AreEqual("vb6", results[3].Language);
        Assert.AreEqual(51, results[4].Count);
        Assert.AreEqual("total languages", results[4].Language);
    }

    private static List<Project> GenerateSampleData()
    {
        List<Project> projects = new()
            {
                new Project
                {
                    FrameworkCode = "netstandard2.0",
                    Family = ProjectClassification.GetFrameworkFamily("netstandard2.0"),
                    FrameworkName = ProjectClassification.GetFriendlyName("netstandard2.0", ProjectClassification.GetFrameworkFamily("netstandard2.0")),
                    Language = "csharp",
                    Path = @"c:\Project1"
                },
                new Project
                {
                    FrameworkCode = "netstandard2.0",
                    Family = ProjectClassification.GetFrameworkFamily("netstandard2.0"),
                    FrameworkName = ProjectClassification.GetFriendlyName("netstandard2.0", ProjectClassification.GetFrameworkFamily("netstandard2.0")),
                    Language = "csharp",
                    Path = @"c:\Project2"
                },
                new Project
                {
                    FrameworkCode = "netcoreapp3.1",
                    Family = ProjectClassification.GetFrameworkFamily("netcoreapp3.1"),
                    FrameworkName = ProjectClassification.GetFriendlyName("netcoreapp3.1", ProjectClassification.GetFrameworkFamily("netcoreapp3.1")),
                    Language = "csharp",
                    Path = @"c:\Project3"
                },
                new Project
                {
                    FrameworkCode = "net6.0",
                    Family = ProjectClassification.GetFrameworkFamily("net6.0"),
                    FrameworkName = ProjectClassification.GetFriendlyName("net6.0", ProjectClassification.GetFrameworkFamily("net6.0")),
                    Language = "csharp",
                    Path = @"c:\Project3"
                },
                new Project
                {
                    FrameworkCode = "net45",
                    Family = ProjectClassification.GetFrameworkFamily("net45"),
                    FrameworkName = ProjectClassification.GetFriendlyName("net45", ProjectClassification.GetFrameworkFamily("net45")),
                    Language = "vb.net",
                    Path = @"c:\Project45"
                },
                new Project
                {
                    FrameworkCode = "vb6",
                    Family = ProjectClassification.GetFrameworkFamily("vb6"),
                    FrameworkName = ProjectClassification.GetFriendlyName("vb6", ProjectClassification.GetFrameworkFamily("vb6")),
                    Language = "vb6",
                    Path = @"c:\Projectvb6"
                },
                new Project
                {
                    FrameworkCode = "v2.2",
                    Family = ProjectClassification.GetFrameworkFamily("v2.2"),
                    FrameworkName = ProjectClassification.GetFriendlyName("v2.2", ProjectClassification.GetFrameworkFamily("v2.2")),
                    Language = "csharp",
                    Path = @"c:\Projectv2"
                },
                new Project
                {
                    FrameworkCode = "v3.5",
                    Family = ProjectClassification.GetFrameworkFamily("v3.5"),
                    FrameworkName = ProjectClassification.GetFriendlyName("v3.5", ProjectClassification.GetFrameworkFamily("v3.5")),
                    Language = "csharp",
                    Path = @"c:\Projectv35"
                },
                new Project
                {
                    FrameworkCode = "v4.5",
                    Family = ProjectClassification.GetFrameworkFamily("v4.5"),
                    FrameworkName = ProjectClassification.GetFriendlyName("v4.5", ProjectClassification.GetFrameworkFamily("v4.5")),
                    Language = "fsharp",
                    Path = @"c:\Projectv45_FS"
                },
                new Project
                {
                    FrameworkCode = "",
                    Family = ProjectClassification.GetFrameworkFamily(""),
                    FrameworkName = ProjectClassification.GetFriendlyName("", ProjectClassification.GetFrameworkFamily("")),
                    Language = "",
                    Path = @"c:\ProjectNull"
                },
                new Project
                {
                    FrameworkCode = "unknown framework",
                    Family = ProjectClassification.GetFrameworkFamily("unknown framework"),
                    FrameworkName = ProjectClassification.GetFriendlyName("unknown framework", ProjectClassification.GetFrameworkFamily("unknown framework")),
                    Language = "unknown language",
                    Path = @"c:\ProjectUnknown"
                }
            };
        return projects;
    }
}
