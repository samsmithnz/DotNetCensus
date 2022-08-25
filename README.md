# DotNetCensus
[![CI/CD](https://github.com/samsmithnz/DotNetCensus/actions/workflows/workflow.yml/badge.svg)](https://github.com/samsmithnz/DotNetCensus/actions/workflows/workflow.yml)
[![Latest NuGet package](https://img.shields.io/nuget/v/dotnet-census)](https://www.nuget.org/packages/dotnet-census/)
![Current Release](https://img.shields.io/github/release/samsmithnz/DotNetCensus/all.svg)

**A dotnet [tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) to conduct a .NET census - count all of the different .NET versions - on a target directory.** 

Ever wanted to understand what your .NET portfolio looks like? Do you know what projects are using .NET versions that expired and the amount of technical debt you are managing? Or maybe just looking to have more visibility into your .NET portfolio? DotNet Census is here to help.

DotNet Census currently supports these .NET versions:
- .NET 5/6/7/etc
- .NET Core  (including .NET Core 1 and 1.1 project.json formats)
- .NET Framework (including .NET Framework 3.5 website formats)
- .NET Standard

Additionally, this tool supports:
- The main .NET languages, including C#, VB.NET, F#, and VB6
- [Directory.Build.props variables](https://github.com/samsmithnz/DotNetCensus/issues/44) in project files

## To use

1. First install:
`dotnet tool install -g dotnet-census`
2. Then run the command in the directory you need to count versions:
`dotnet census`
3. If you have an old version, you can update with `dotnet tool update -g dotnet-census`, and uninstall with `dotnet tool uninstall -g dotnet-census`

## Console arguments
Several options are supported:

`dotnet census [-d|--directory <DIRECTORY>] [-r|--repo <REPO>] [-u|--user <USERNAME>] [-p|--password <PASSWORD>] [-f|--file <FILE>] [-t|--total] [-i|--inventory]`

- `-d|--directory`: target directory to scan for .NET versions
- `-r|--repo`: target GitHub repo to scan for .NET versions
- `-u|--user`: target GitHub username to scan for .NET versions
- `-p|--password`: target GitHub PAT Token to scan for .NET versions
- `-f|--file`: file path to output and save CSV data. 
- `-t|--total`: Add totals to results. Ignored when used with `-i|--inventory`
- `-i|--inventory`: output inventory of all data (instead of the default aggregated summary)

### To target a specific directory from anywhere, use the `-d` argument to specify a target directory:
`dotnet census -d c:\users\me\desktop\repos`

### To add totals to the results, use the `-t` argument:
`dotnet census -t`

This is a sample of the results: 
```
Framework             FrameworkFamily  Count  Status          
--------------------------------------------------------------
.NET 5.0              .NET             1      deprecated      
.NET 6.0              .NET             3      supported       
.NET 6.0-android      .NET             1      supported       
.NET 6.0-ios          .NET             1      supported       
.NET 7.0              .NET             1      in preview      
.NET Core 1.0         .NET Core        1      deprecated      
.NET Core 1.1         .NET Core        1      deprecated      
.NET Core 2.0         .NET Core        1      deprecated      
.NET Core 2.1         .NET Core        1      deprecated      
.NET Core 2.2         .NET Core        1      deprecated      
.NET Core 3.0         .NET Core        2      deprecated      
.NET Core 3.1         .NET Core        3      EOL: 13-Dec-2022
.NET Framework 1.0    .NET Framework   1      deprecated      
.NET Framework 1.1    .NET Framework   1      deprecated      
.NET Framework 2.0    .NET Framework   1      deprecated      
.NET Framework 3.5    .NET Framework   2      EOL: 9-Jan-2029 
.NET Framework 4.0    .NET Framework   1      deprecated      
.NET Framework 4.5    .NET Framework   1      deprecated      
.NET Framework 4.6.1  .NET Framework   1      deprecated      
.NET Framework 4.6.2  .NET Framework   1      supported       
.NET Framework 4.7.1  .NET Framework   1      supported       
.NET Framework 4.7.2  .NET Framework   2      supported       
.NET Standard 2.0     .NET Standard    1      supported       
(Unknown)             (Unknown)        1      unknown         
Visual Basic 6        Visual Basic 6   1      deprecated      
total frameworks                       32                     
```

### To get an inventory of results, use the `-i` argument:
`dotnet census -i`

A sample of inventory results:
```
FileName                                    Path                                                                                     FrameworkCode   FrameworkName         Family          Language  Status
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Assembly-CSharp.csproj                      \samples\Sample.Unity2020\Assembly-CSharp.csproj                                         v4.7.1          .NET Framework 4.7.1  .NET Framework  csharp    supported
DotNetCensus.Core.csproj                    \src\DotNetCensus.Core\DotNetCensus.Core.csproj                                          net6.0          .NET 6.0              .NET            csharp    supported
DotNetCensus.csproj                         \src\DotNetCensus\DotNetCensus.csproj                                                    net6.0          .NET 6.0              .NET            csharp    supported
DotNetCensus.Tests.csproj                   \src\DotNetCensus.Tests\DotNetCensus.Tests.csproj                                        net6.0          .NET 6.0              .NET            csharp    supported
project.json                                \samples\Sample.NetCore1.0.ConsoleApp\project.json                                       netcoreapp1.0   .NET Core 1.0         .NET Core       csharp    deprecated
project.json                                \samples\Sample.NetCore1.1.ConsoleApp\project.json                                       netcoreapp1.1   .NET Core 1.1         .NET Core       csharp    deprecated
Sample.MultipleTargets.ConsoleApp.csproj    \samples\Sample.MultipleTargets.ConsoleApp\Sample.MultipleTargets.ConsoleApp.csproj      netcoreapp3.1   .NET Core 3.1         .NET Core       csharp    EOL: 13-Dec-2022
Sample.MultipleTargets.ConsoleApp.csproj    \samples\Sample.MultipleTargets.ConsoleApp\Sample.MultipleTargets.ConsoleApp.csproj      net462          .NET Framework 4.6.2  .NET Framework  csharp    supported
Sample.Net5.ConsoleApp.csproj               \samples\Sample.Net5.ConsoleApp\Sample.Net5.ConsoleApp.csproj                            net5.0          .NET 5.0              .NET            csharp    deprecated
Sample.Net6.ConsoleApp.csproj               \samples\Sample.Net6.ConsoleApp\Sample.Net6.ConsoleApp.csproj                            net6.0          .NET 6.0              .NET            csharp    supported
Sample.Net7.ConsoleApp.csproj               \samples\Sample.Net7.ConsoleApp\Sample.Net7.ConsoleApp.csproj                            net7.0          .NET 7.0              .NET            csharp    supported
Sample.NetCore.ConsoleApp.csproj            \samples\Sample.NetCore3.1.ConsoleApp\Sample.NetCore.ConsoleApp.csproj                   netcoreapp3.1   .NET Core 3.1         .NET Core       csharp    EOL: 13-Dec-2022
Sample.NetCore2.0.ConsoleApp.csproj         \samples\Sample.NetCore2.0.ConsoleApp\Sample.NetCore2.0.ConsoleApp.csproj                netcoreapp2.0   .NET Core 2.0         .NET Core       csharp    deprecated
Sample.NetCore2.1.ConsoleApp.csproj         \samples\Sample.NetCore2.1.ConsoleApp\Sample.NetCore2.1.ConsoleApp.csproj                netcoreapp2.1   .NET Core 2.1         .NET Core       csharp    deprecated
Sample.NetCore3.0.ConsoleApp.csproj         \samples\Sample.NetCore3.0.ConsoleApp\Sample.NetCore3.0.ConsoleApp.csproj                netcoreapp3.0   .NET Core 3.0         .NET Core       csharp    deprecated
Sample.NetFramework.ConsoleApp.csproj       \samples\Sample.NetFramework.ConsoleApp\Sample.NetFramework.ConsoleApp.csproj            v4.7.2          .NET Framework 4.7.2  .NET Framework  csharp    supported
Sample.NetFrameworkVBNet.ConsoleApp.vbproj  \samples\Sample.NetFrameworkVBNet.ConsoleApp\Sample.NetFrameworkVBNet.ConsoleApp.vbproj  netcoreapp3.1   .NET Core 3.1         .NET Core       vb.net    EOL: 13-Dec-2022
Sample.NetStandard.Class.csproj             \samples\Sample.NetStandard.Class\Sample.NetStandard.Class.csproj                        netstandard2.0  .NET Standard 2.0     .NET Standard   csharp    supported
Sample.SSDT.Database.sqlproj                \samples\Sample.SSDT.Database\Sample.SSDT.Database.sqlproj                               v4.7.2          .NET Framework 4.7.2  .NET Framework  csharp    supported
Sample.VB6.WinApp.vbp                       \samples\Sample.VB6.Calculator\Sample.VB6.WinApp.vbp                                     vb6             Visual Basic 6        Visual Basic 6  vb6       deprecated
VBProj.vbproj                               \samples\Sample.NetFramework1.0.App\VBProj.vbproj                                        v1.0            .NET Framework 1.0    .NET Framework  vb.net    deprecated
VBProj.vbproj                               \samples\Sample.NetFramework1.1.App\VBProj.vbproj                                        v1.1            .NET Framework 1.1    .NET Framework  vb.net    deprecated
VBProj.vbproj                               \samples\Sample.NetFramework2.0.App\VBProj.vbproj                                        v2.0            .NET Framework 2.0    .NET Framework  vb.net    deprecated
VBProj.vbproj                               \samples\Sample.NetFrameworkInvalid.App\VBProj.vbproj                                                    (Unknown)             (Unknown)       vb.net    unknown
```

### To download results to a csv file, add the `-f` argument with a file name:
`dotnet census -f c:\temp\results.csv`

### To download results from a GitHub Repo, 
- Add the `-o` argument with the GitHub owner or organiation
- Add the `-r` argument with the GitHub repo name
- Add the `-u` argument with the GitHub user
- Add the `-p` argument with the GitHub PAT Token password

For example, to download results from this repository:

`dotnet census -o samsmithnz -r dotnetcensus -u samsmithnz -p <PAT_TOKEN>`

### To download results from a GitHub Organization, omit the `-r` argument
- Add the `-o` argument with the GitHub owner or organiation
- Add the `-u` argument with the GitHub user
- Add the `-p` argument with the GitHub PAT Token password

Note: For large organizations, this will time out. Open to solutions on how to handle this better.
For example, to download results from this organization:

`dotnet census -o samsmithnz -u samsmithnz -p <PAT_TOKEN>`
## What's next?
- Add ability to scan target other types of Git Repos (Azure DevOps), and experiment with scanning organizations

## Contributions
If you have a sample that you think should have been picked up, please create an issue or a PR! I'm happy to consider anything, and I know this isn't perfect!  
