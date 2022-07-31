namespace DotNetCensus.Tests;

[TestClass]
public class CensorTests : BaseTests
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
        //Assert.AreEqual(10, results.Count);
        Assert.AreEqual("", results.Find(i => i.Framework == "netstandard2.0"));
        Assert.AreEqual(2, results.Find(i => i.Framework == "netstandard2.0")?.Count);
        Assert.AreEqual(2, results.Find(i => i.FrameworkFamily == ".NET Standard")?.Count);
        Assert.AreEqual(1, results.Find(i => i.Framework == "netcoreapp3.1")?.Count);
        Assert.AreEqual(1, results.Find(i => i.FrameworkFamily == ".NET Core")?.Count);
        Assert.AreEqual(1, results.Find(i => i.Framework == "net45")?.Count);
        Assert.AreEqual(1, results.Find(i => i.FrameworkFamily == ".NET Framework")?.Count);
        Assert.AreEqual(10, results[^1].Count);
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
        Assert.AreEqual(6, results.Count);
        Assert.AreEqual(6, results.Find(i => i.Language == "csharp")?.Count);
        Assert.AreEqual(1, results.Find(i => i.Language == "vbDotNet")?.Count);
        Assert.AreEqual(10, results[^1].Count);
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
        Assert.AreEqual(14, results.Count);
        Assert.AreEqual(1, results[0].Count);
        Assert.AreEqual("(Unknown framework)", results[0].Framework);
        Assert.AreEqual(1, results[^2].Count);
        Assert.AreEqual("vb6", results[^2].Framework);
        Assert.AreEqual(16, results[^1].Count);
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
        Assert.AreEqual(13, results.Count);
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
        Assert.AreEqual(10, results[0].Count);
        Assert.AreEqual("csharp", results[0].Language);
        Assert.AreEqual(5, results[1].Count);
        Assert.AreEqual("vb.net", results[1].Language);
        Assert.AreEqual(1, results[2].Count);
        Assert.AreEqual("vb6", results[2].Language);
        Assert.AreEqual(16, results[3].Count);
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
        Assert.AreEqual(10, results[0].Count);
        Assert.AreEqual("csharp", results[0].Language);
        Assert.AreEqual(5, results[1].Count);
        Assert.AreEqual("vb.net", results[1].Language);
        Assert.AreEqual(1, results[2].Count);
        Assert.AreEqual("vb6", results[2].Language);
    }

    private static List<Project> GenerateSampleData()
    {
        List<Project> projects = new()
            {
                new Project
                {
                    FrameworkCode = "netstandard2.0",
                    Family = DotNetProjectScanning.GetFrameworkFamily("netstandard2.0"),
                    FrameworkName = DotNetProjectScanning.GetFriendlyName("netstandard2.0", DotNetProjectScanning.GetFrameworkFamily("netstandard2.0")),
                    Language = "csharp",
                    Path = @"c:\Project1"
                },
                new Project
                {
                    FrameworkCode = "netstandard2.0",
                    Family = DotNetProjectScanning.GetFrameworkFamily("netstandard2.0"),
                    FrameworkName = DotNetProjectScanning.GetFriendlyName("netstandard2.0", DotNetProjectScanning.GetFrameworkFamily("netstandard2.0")),
                    Language = "csharp",
                    Path = @"c:\Project2"
                },
                new Project
                {
                    FrameworkCode = "netcoreapp3.1",
                    Family = DotNetProjectScanning.GetFrameworkFamily("netcoreapp3.1"),
                    FrameworkName = DotNetProjectScanning.GetFriendlyName("netcoreapp3.1", DotNetProjectScanning.GetFrameworkFamily("netcoreapp3.1")),
                    Language = "csharp",
                    Path = @"c:\Project3"
                },
                new Project
                {
                    FrameworkCode = "net6.0",
                    Family = DotNetProjectScanning.GetFrameworkFamily("net6.0"),
                    FrameworkName = DotNetProjectScanning.GetFriendlyName("net6.0", DotNetProjectScanning.GetFrameworkFamily("net6.0")),
                    Language = "csharp",
                    Path = @"c:\Project3"
                },
                new Project
                {
                    FrameworkCode = "net45",
                    Family = DotNetProjectScanning.GetFrameworkFamily("net45"),
                    FrameworkName = DotNetProjectScanning.GetFriendlyName("net45", DotNetProjectScanning.GetFrameworkFamily("net45")),
                    Language = "vbDotNet",
                    Path = @"c:\Project45"
                },
                new Project
                {
                    FrameworkCode = "vb6",
                    Family = DotNetProjectScanning.GetFrameworkFamily("vb6"),
                    FrameworkName = DotNetProjectScanning.GetFriendlyName("vb6", DotNetProjectScanning.GetFrameworkFamily("vb6")),
                    Language = "vb6",
                    Path = @"c:\Projectvb6"
                },
                new Project
                {
                    FrameworkCode = "v2.2",
                    Family = DotNetProjectScanning.GetFrameworkFamily("v2.2"),
                    FrameworkName = DotNetProjectScanning.GetFriendlyName("v2.2", DotNetProjectScanning.GetFrameworkFamily("v2.2")),
                    Language = "csharp",
                    Path = @"c:\Projectv2"
                },
                new Project
                {
                    FrameworkCode = "v3.5",
                    Family = DotNetProjectScanning.GetFrameworkFamily("v3.5"),
                    FrameworkName = DotNetProjectScanning.GetFriendlyName("v3.5", DotNetProjectScanning.GetFrameworkFamily("v3.5")),
                    Language = "csharp",
                    Path = @"c:\Projectv35"
                },
                new Project
                {
                    FrameworkCode = "",
                    Family = DotNetProjectScanning.GetFrameworkFamily(""),
                    FrameworkName = DotNetProjectScanning.GetFriendlyName("", DotNetProjectScanning.GetFrameworkFamily("")),
                    Language = "",
                    Path = @"c:\ProjectNull"
                },
                new Project
                {
                    FrameworkCode = "unknown framework",
                    Family = DotNetProjectScanning.GetFrameworkFamily("unknown framework"),
                    FrameworkName = DotNetProjectScanning.GetFriendlyName("unknown framework", DotNetProjectScanning.GetFrameworkFamily("unknown framework")),
                    Language = "unknown language",
                    Path = @"c:\ProjectUnknown"
                }
            };
        return projects;
    }
}
