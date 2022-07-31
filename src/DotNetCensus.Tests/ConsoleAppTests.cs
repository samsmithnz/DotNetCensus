namespace DotNetCensus.Tests;

[TestClass]
public class ConsoleAppTests : BaseTests
{

    [TestMethod]
    public void RunSamplesTest()
    {
        //Arrange
        StringWriter sw = new();
        string expected = @"(Unknown framework): 1
net462: 1
net5.0: 1
netcoreapp3.1: 3
netstandard2.0: 1
Unity3d v2020.3: 1
v1.0: 1
v1.1: 1
v2.0: 1
v4.7.1: 1
v4.7.2: 2
vb6: 1
";

        //Act
        if (SamplesPath != null)
        {
            Console.SetOut(sw);
            Program.Main(new string[] { "-d", SamplesPath });
        }

        //Asset
        Assert.IsNotNull(expected);
        Assert.AreEqual(expected, sw.ToString());
    }

    [TestMethod]
    public void RunSamplesWithTotalsTest()
    {
        //Arrange
        bool includeTotal = true;
        StringWriter sw = new();
        string expected = @"(Unknown framework): 1
net462: 1
net5.0: 1
netcoreapp3.1: 3
netstandard2.0: 1
Unity3d v2020.3: 1
v1.0: 1
v1.1: 1
v2.0: 1
v4.7.1: 1
v4.7.2: 2
vb6: 1
total frameworks: 15
";

        //Act
        if (SamplesPath != null)
        {
            Console.SetOut(sw);
            Program.Main(new string[] { "-d", SamplesPath, "-i", includeTotal.ToString() });
        }

        //Asset
        Assert.IsNotNull(expected);
        Assert.AreEqual(expected, sw.ToString());
    }

}
