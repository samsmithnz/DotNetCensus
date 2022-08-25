using DotNetCensus.Tests.Helpers;

namespace DotNetCensus.Tests;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[TestClass]
[TestCategory("IntegrationTest")]
public class ConsoleAppTests : DirectoryBasedTests
{
    [TestMethod]
    public void RunSamplesWithNoParametersTest()
    {
        //Arrange
        string[] parameters = Array.Empty<string>();
        StringWriter sw = new();
        string expected = @"
Framework  FrameworkFamily  Count  Status
-----------------------------------------

";

        //Act
        Console.SetOut(sw);
        Program.Main(parameters);
        string result = Environment.NewLine + sw.ToString();
        sw.Close();

        //Asset
        Assert.IsNotNull(expected);
        Assert.AreEqual(expected, result);
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
