namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class ConsoleAppTests : BaseTests
{

    [TestMethod]
    public void RunSamplesTest()
    {
        //Arrange
        if (SamplesPath != null)
        {
            string[] parameters = new string[] { "-d", SamplesPath };
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
.NET Standard 2.0     .NET Standard        1
(Unknown)             (Unknown)            1
Visual Basic 6        Visual Basic 6       1

";

            //Act
            Console.SetOut(sw);
            Program.Main(parameters);


            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected, Environment.NewLine + sw.ToString());
        }
    }

    [TestMethod]
    public void RunSamplesWithTotalsTest()
    {
        //Arrange
        if (SamplesPath != null)
        {
            string[] parameters = new string[] { "-d", SamplesPath, "-t" };
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
.NET Standard 2.0     .NET Standard        1
(Unknown)             (Unknown)            1
Visual Basic 6        Visual Basic 6       1
total frameworks                          16

";

            //Act
            Console.SetOut(sw);
            Program.Main(parameters);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected, Environment.NewLine + sw.ToString());
        }
    }

    [TestMethod]
    public void RunSamplesWithInvalidParametersTest()
    {
        //Arrange
        if (SamplesPath != null)
        {
            string[] parameters = new string[] { "-d", SamplesPath, "-z" }; //z is an invalid parameter
            StringWriter sw = new();
            string expected = @"
CommandLine.UnknownOptionError
";

            //Act
            Console.SetOut(sw);
            Program.Main(parameters);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected, Environment.NewLine + sw.ToString());
        }
    }
}
