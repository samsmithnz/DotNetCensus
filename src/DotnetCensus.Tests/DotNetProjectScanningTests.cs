namespace DotNetCensus.Tests;

[TestClass]
public class DotNetProjectScanningTests
{
    [TestMethod]
    public void TestMethod1()
    {
        //Arrange
        DirectoryInfo currentDir = new(Directory.GetCurrentDirectory());
        string samplesPath =  Path.Combine(currentDir.Parent.Parent.Parent.Parent.Parent.FullName, "samples"); ;

        ////Act
        //List<FrameworkSummary> results = repoScanner.AggregateFrameworks(projects, includeTotal);

        ////Asset
        Assert.AreEqual(@"C:\Users\samsm\source\repos\DotnetCensus\samples", samplesPath);
        //Assert.AreEqual(10, results.Count);
        //Assert.AreEqual(2, results.Find(i => i.Framework == "netstandard2.0").Count);
        //Assert.AreEqual(2, results.Find(i => i.FrameworkFamily == ".NET Standard").Count);
        //Assert.AreEqual(1, results.Find(i => i.Framework == "netcoreapp3.1").Count);
        //Assert.AreEqual(1, results.Find(i => i.FrameworkFamily == ".NET Core").Count);
        //Assert.AreEqual(1, results.Find(i => i.Framework == "net45").Count);
        //Assert.AreEqual(1, results.Find(i => i.FrameworkFamily == ".NET Framework").Count);
        //Assert.AreEqual(10, results[^1].Count);
    }
}