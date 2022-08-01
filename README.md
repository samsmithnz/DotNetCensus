# DotNetCensus
[![CI/CD](https://github.com/samsmithnz/DotNetCensus/actions/workflows/workflow.yml/badge.svg)](https://github.com/samsmithnz/DotNetCensus/actions/workflows/workflow.yml)

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
Framework             FrameworkFamily  Count
--------------------------------------------
.NET 5.0              .NET                 1
.NET 6.0              .NET                 1
.NET Core 3.1         .NET Core            3
.NET Framework 1.1    .NET Framework       1
.NET Framework 2.0    .NET Framework       1
.NET Framework 4.6.2  .NET Framework       1
.NET Framework 4.7.1  .NET Framework       1
.NET Framework 4.7.2  .NET Framework       2
.NET Standard 2.0     .NET Standard        1
total frameworks                          13
```
