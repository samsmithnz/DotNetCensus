namespace DotNetCensus.Tests;

[TestClass]
public class ConsoleAppTests : BaseTests
{

    [TestMethod]
    public void RunSamplesTest()
    {
        //Arrange
        List<FrameworkSummary>? results = null;
        bool includeTotal = true;
        string? expected = null;
        StringWriter sw = new();

        //Act
        if (SamplesPath != null)
        {

            Console.SetOut(sw);

            Program.Main(new string[] { SamplesPath });
            expected = "Hello World!" + Environment.NewLine;

            //Program.Main(new string[] { SamplesPath });
            //List<Project> projects = DotNetProjectScanning.SearchDirectory(SamplesPath);
            //results = Census.AggregateFrameworks(projects, includeTotal);
        }

        //Asset
        Assert.IsNotNull(expected);
        Assert.AreEqual(expected, sw.ToString());
        //Assert.IsNotNull(results);
        //Assert.AreEqual(13, results.Count);
        //Assert.AreEqual(2, results.Find(i => i.Framework == "netstandard2.0")?.Count);
        //Assert.AreEqual(2, results.Find(i => i.FrameworkFamily == ".NET Standard")?.Count);
        //Assert.AreEqual(1, results.Find(i => i.Framework == "netcoreapp3.1")?.Count);
        //Assert.AreEqual(1, results.Find(i => i.FrameworkFamily == ".NET Core")?.Count);
        //Assert.AreEqual(1, results.Find(i => i.Framework == "net45")?.Count);
        //Assert.AreEqual(1, results.Find(i => i.FrameworkFamily == ".NET Framework")?.Count);
        //Assert.AreEqual(10, results[^1].Count);
    }

}
