# DotNetCensus
[![CI/CD](https://github.com/samsmithnz/DotNetCensus/actions/workflows/workflow.yml/badge.svg)](https://github.com/samsmithnz/DotNetCensus/actions/workflows/workflow.yml)
[![Latest NuGet package](https://img.shields.io/nuget/v/dotnet-census)](https://www.nuget.org/packages/dotnet-census/)
![Current Release](https://img.shields.io/github/release/samsmithnz/DotNetCensus/all.svg)

**A tool to conduct a census, to count different .NET versions, on a folder and/or repo.** 

Ever wanted to understand what your .NET portfolio looks like? Perhaps to understand what frameworks are expired, the amount of technical debt, or just to have more visibility into your portfolio. DotNet Census is here to help.

Currently supports:
- .NET Framework
- .NET Standard
- .NET Core 
- .NET 5/6/7/etc
- VB6 (!!!)

- Currently only works on folders. In the future will support GitHub repo scanning too.

## To use

1. First install:
`dotnet tool install -g dotnet-census`

2. Then run the command in the directory you need to count versions:
`dotnet census`

To target a specific directory from anywhere, use the `-d` argument to specify a target directory:
`dotnet census -d c:\users\me\desktop\repos`

To add totals to the results, use the `-t` argument:
`dotnet census -t`

This is a sample of the results: 
```
Framework             FrameworkFamily  Count  Status
--------------------------------------------------------------
.NET 5.0              .NET             1      deprecated
.NET 6.0              .NET             4      supported
.NET 7.0              .NET             1      supported
.NET Core 1.0         .NET Core        1      deprecated
.NET Core 1.1         .NET Core        1      deprecated
.NET Core 2.0         .NET Core        1      deprecated
.NET Core 2.1         .NET Core        1      deprecated
.NET Core 3.0         .NET Core        1      deprecated
.NET Core 3.1         .NET Core        3      EOL: 13-Dec-2022
.NET Framework 1.0    .NET Framework   1      deprecated
.NET Framework 1.1    .NET Framework   1      deprecated
.NET Framework 2.0    .NET Framework   1      deprecated
.NET Framework 4.6.2  .NET Framework   1      supported
.NET Framework 4.7.1  .NET Framework   1      supported
.NET Framework 4.7.2  .NET Framework   2      supported
.NET Standard 2.0     .NET Standard    1      supported
(Unknown)             (Unknown)        1      unknown
Visual Basic 6        Visual Basic 6   1      deprecated
total frameworks                       24                 
```

To get raw results, use the `-r` argument:
`dotnet census -r`

A sample of raw results:
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
