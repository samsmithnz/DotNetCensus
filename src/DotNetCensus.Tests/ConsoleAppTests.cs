namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
public class ConsoleAppTests : BaseTests
{
    [TestMethod]
    public void RunSamplesWithNoParametersTest()
    {
        //Arrange
        if (SamplesPath != null)
        {
            string[] parameters = new string[] { };
            StringWriter sw = new();
            string expected = @"
Framework  FrameworkFamily  Count  Color
----------------------------------------

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
    public void RunSamplesWithPathTest()
    {
        //Arrange
        if (SamplesPath != null)
        {
            string[] parameters = new string[] { "-d", SamplesPath };
            StringWriter sw = new();
            string expected = @"
Framework             FrameworkFamily  Count  Color 
----------------------------------------------------
.NET 5.0              .NET                 1  red   
.NET 6.0              .NET                 1  blue  
.NET 7.0              .NET                 1  blue  
.NET Core 3.1         .NET Core            3  orange
.NET Framework 1.0    .NET Framework       1  gray  
.NET Framework 1.1    .NET Framework       1  gray  
.NET Framework 2.0    .NET Framework       1  gray  
.NET Framework 4.6.2  .NET Framework       1  gray  
.NET Framework 4.7.1  .NET Framework       1  gray  
.NET Framework 4.7.2  .NET Framework       2  gray  
.NET Standard 2.0     .NET Standard        1  blue  
(Unknown)             (Unknown)            1  gray  
Visual Basic 6        Visual Basic 6       1  gray  

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
Framework             FrameworkFamily  Count  Color 
----------------------------------------------------
.NET 5.0              .NET                 1  red   
.NET 6.0              .NET                 1  blue  
.NET 7.0              .NET                 1  blue  
.NET Core 3.1         .NET Core            3  orange
.NET Framework 1.0    .NET Framework       1  gray  
.NET Framework 1.1    .NET Framework       1  gray  
.NET Framework 2.0    .NET Framework       1  gray  
.NET Framework 4.6.2  .NET Framework       1  gray  
.NET Framework 4.7.1  .NET Framework       1  gray  
.NET Framework 4.7.2  .NET Framework       2  gray  
.NET Standard 2.0     .NET Standard        1  blue  
(Unknown)             (Unknown)            1  gray  
Visual Basic 6        Visual Basic 6       1  gray  
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
        if (SamplesPath != null)
        {
            try
            {
                //Arrange
                string[] parameters = new string[] { "-d", SamplesPath, "-z" }; //z is an invalid parameter
                StringWriter sw = new();

                //Act
                Console.SetOut(sw);
                Program.Main(parameters);
            }
            catch (Exception ex)
            {
                //Asset
                Assert.IsNotNull(ex);
                Assert.AreEqual("One or more errors occurred. (CommandLine.UnknownOptionError)", ex.Message);
            }
        }
    }
}
