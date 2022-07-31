namespace DotNetCensus.Tests;

[TestClass]
public class ConsoleAppTests : BaseTests
{

    [TestMethod]
    public void RunSamplesTest()
    {
        //Arrange
        StringWriter sw = new();
        string expected = @"
Framework             FrameworkFamily  Count
--------------------------------------------
.NET 5.0              .NET                 1
.NET 6.0              .NET                 1
.NET 7.0              .NET                 1
.NET Core 3.1         .NET Core            3
.NET Framework 1.0    .NET Framework       1
.NET Framework 1.1    .NET Framework       1
.NET Framework 2.0    .NET Framework       1
.NET Framework 4.6.2  .NET Framework       1
.NET Framework 4.7.1  .NET Framework       1
.NET Framework 4.7.2  .NET Framework       2
.NET Standard         .NET Standard        1
(Unknown)             (Unknown)            1
Visual Basic 6        Visual Basic 6       1

";

        //Act
        if (SamplesPath != null)
        {
            Console.SetOut(sw);
            Program.Main(new string[] { "-d", SamplesPath });
        }

        //Asset
        Assert.IsNotNull(expected);
        Assert.AreEqual(expected, Environment.NewLine + sw.ToString());
    }

    [TestMethod]
    public void RunSamplesWithTotalsTest()
    {
        //Arrange
        bool includeTotal = true;
        StringWriter sw = new();
        string expected = @"
Framework             FrameworkFamily  Count
--------------------------------------------
.NET 5.0              .NET                 1
.NET 6.0              .NET                 1
.NET 7.0              .NET                 1
.NET Core 3.1         .NET Core            3
.NET Framework 1.0    .NET Framework       1
.NET Framework 1.1    .NET Framework       1
.NET Framework 2.0    .NET Framework       1
.NET Framework 4.6.2  .NET Framework       1
.NET Framework 4.7.1  .NET Framework       1
.NET Framework 4.7.2  .NET Framework       2
.NET Standard         .NET Standard        1
(Unknown)             (Unknown)            1
Visual Basic 6        Visual Basic 6       1
total frameworks                          16

";
        //        string expected = @"
        //Framework         FrameworkFamily  Count
        //----------------------------------------
        //net6.0            .NET                 1
        //total frameworks                       1

        //";
        //Act
        if (SamplesPath != null)
        {
            Console.SetOut(sw);
            Program.Main(new string[] { "-d", SamplesPath, "-i", includeTotal.ToString() });
        }

        //Asset
        Assert.IsNotNull(expected);
        Assert.AreEqual(expected, Environment.NewLine + sw.ToString());
    }

}
