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
Framework  FrameworkFamily  Count  Status
-----------------------------------------

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
Framework             FrameworkFamily  Count  Status          
--------------------------------------------------------------
.NET 5.0              .NET                 1  deprecated      
.NET 6.0              .NET                 1  supported       
.NET 7.0              .NET                 1  supported       
.NET Core 1.0         .NET Core            1  deprecated      
.NET Core 1.1         .NET Core            1  deprecated      
.NET Core 2.0         .NET Core            1  deprecated      
.NET Core 2.1         .NET Core            1  deprecated      
.NET Core 3.0         .NET Core            1  deprecated      
.NET Core 3.1         .NET Core            3  EOL: 13-Dec-2022
.NET Framework 1.0    .NET Framework       1  deprecated      
.NET Framework 1.1    .NET Framework       1  deprecated      
.NET Framework 2.0    .NET Framework       1  deprecated      
.NET Framework 4.6.2  .NET Framework       1  supported       
.NET Framework 4.7.1  .NET Framework       1  supported       
.NET Framework 4.7.2  .NET Framework       2  supported       
.NET Standard 2.0     .NET Standard        1  supported       
(Unknown)             (Unknown)            1  unknown         
Visual Basic 6        Visual Basic 6       1  deprecated      

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
Framework             FrameworkFamily  Count  Status          
--------------------------------------------------------------
.NET 5.0              .NET                 1  deprecated      
.NET 6.0              .NET                 1  supported       
.NET 7.0              .NET                 1  supported       
.NET Core 1.0         .NET Core            1  deprecated      
.NET Core 1.1         .NET Core            1  deprecated      
.NET Core 2.0         .NET Core            1  deprecated      
.NET Core 2.1         .NET Core            1  deprecated      
.NET Core 3.0         .NET Core            1  deprecated      
.NET Core 3.1         .NET Core            3  EOL: 13-Dec-2022
.NET Framework 1.0    .NET Framework       1  deprecated      
.NET Framework 1.1    .NET Framework       1  deprecated      
.NET Framework 2.0    .NET Framework       1  deprecated      
.NET Framework 4.6.2  .NET Framework       1  supported       
.NET Framework 4.7.1  .NET Framework       1  supported       
.NET Framework 4.7.2  .NET Framework       2  supported       
.NET Standard 2.0     .NET Standard        1  supported       
(Unknown)             (Unknown)            1  unknown         
Visual Basic 6        Visual Basic 6       1  deprecated      
total frameworks                          21                  

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
    public void RunSamplesWithRawResultsTest()
    {
        //Arrange
        if (SamplesPath != null)
        {
            string[] parameters = new string[] { "-d", SamplesPath, "-r" };
            StringWriter sw = new();
            string expected = @"
FileName                                    Path                                                                                                                             FrameworkCode   FrameworkName         Family          Language  Color           
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Sample.MultipleTargets.ConsoleApp.csproj    C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.MultipleTargets.ConsoleApp\Sample.MultipleTargets.ConsoleApp.csproj      netcoreapp3.1   .NET Core 3.1         .NET Core       csharp    EOL: 13-Dec-2022
Sample.MultipleTargets.ConsoleApp.csproj    C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.MultipleTargets.ConsoleApp\Sample.MultipleTargets.ConsoleApp.csproj      net462          .NET Framework 4.6.2  .NET Framework  csharp    supported       
Sample.Net5.ConsoleApp.csproj               C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.Net5.ConsoleApp\Sample.Net5.ConsoleApp.csproj                            net5.0          .NET 5.0              .NET            csharp    deprecated      
Sample.Net6.ConsoleApp.csproj               C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.Net6.ConsoleApp\Sample.Net6.ConsoleApp.csproj                            net6.0          .NET 6.0              .NET            csharp    supported       
Sample.Net7.ConsoleApp.csproj               C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.Net7.ConsoleApp\Sample.Net7.ConsoleApp.csproj                            net7.0          .NET 7.0              .NET            csharp    supported       
project.json                                C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.NetCore1.0.ConsoleApp\project.json                                       netcoreapp1.0   .NET Core 1.0         .NET Core       csharp    deprecated      
project.json                                C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.NetCore1.1.ConsoleApp\project.json                                       netcoreapp1.1   .NET Core 1.1         .NET Core       csharp    deprecated      
Sample.NetCore2.0.ConsoleApp.csproj         C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.NetCore2.0.ConsoleApp\Sample.NetCore2.0.ConsoleApp.csproj                netcoreapp2.0   .NET Core 2.0         .NET Core       csharp    deprecated      
Sample.NetCore2.1.ConsoleApp.csproj         C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.NetCore2.1.ConsoleApp\Sample.NetCore2.1.ConsoleApp.csproj                netcoreapp2.1   .NET Core 2.1         .NET Core       csharp    deprecated      
Sample.NetCore3.0.ConsoleApp.csproj         C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.NetCore3.0.ConsoleApp\Sample.NetCore3.0.ConsoleApp.csproj                netcoreapp3.0   .NET Core 3.0         .NET Core       csharp    deprecated      
Sample.NetCore.ConsoleApp.csproj            C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.NetCore3.1.ConsoleApp\Sample.NetCore.ConsoleApp.csproj                   netcoreapp3.1   .NET Core 3.1         .NET Core       csharp    EOL: 13-Dec-2022
Sample.NetFramework.ConsoleApp.csproj       C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.NetFramework.ConsoleApp\Sample.NetFramework.ConsoleApp.csproj            v4.7.2          .NET Framework 4.7.2  .NET Framework  csharp    supported       
VBProj.vbproj                               C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.NetFramework1.0.App\VBProj.vbproj                                        v1.0            .NET Framework 1.0    .NET Framework  vb.net    deprecated      
VBProj.vbproj                               C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.NetFramework1.1.App\VBProj.vbproj                                        v1.1            .NET Framework 1.1    .NET Framework  vb.net    deprecated      
VBProj.vbproj                               C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.NetFramework2.0.App\VBProj.vbproj                                        v2.0            .NET Framework 2.0    .NET Framework  vb.net    deprecated      
VBProj.vbproj                               C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.NetFrameworkInvalid.App\VBProj.vbproj                                                    (Unknown)             (Unknown)       vb.net    unknown         
Sample.NetFrameworkVBNet.ConsoleApp.vbproj  C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.NetFrameworkVBNet.ConsoleApp\Sample.NetFrameworkVBNet.ConsoleApp.vbproj  netcoreapp3.1   .NET Core 3.1         .NET Core       vb.net    EOL: 13-Dec-2022
Sample.NetStandard.Class.csproj             C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.NetStandard.Class\Sample.NetStandard.Class.csproj                        netstandard2.0  .NET Standard 2.0     .NET Standard   csharp    supported       
Sample.SSDT.Database.sqlproj                C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.SSDT.Database\Sample.SSDT.Database.sqlproj                               v4.7.2          .NET Framework 4.7.2  .NET Framework  csharp    supported       
Assembly-CSharp.csproj                      C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.Unity2020\Assembly-CSharp.csproj                                         v4.7.1          .NET Framework 4.7.1  .NET Framework  csharp    supported       
Sample.VB6.WinApp.vbp                       C:\Users\samsm\source\repos\DotNetCensus\samples\Sample.VB6.Calculator\Sample.VB6.WinApp.vbp                                     vb6             Visual Basic 6        Visual Basic 6  vb6       deprecated      

";
            //generate character n times
            string dashes = new string('-', SamplesPath.Length);
            expected = expected.Replace(SamplesPath, "samples");
            expected = expected.Replace(dashes, "");

            //Act
            Console.SetOut(sw);
            Program.Main(parameters);

            //Asset
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected, Environment.NewLine + sw.ToString().Replace(SamplesPath, "samples").Replace(dashes, ""));
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
