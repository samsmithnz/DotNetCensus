# Project docs for each version of .NET

## .NET Core, .NET 5+, and .NET Standard 
Projects use `TargetFramework` to identify version
``` *.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
  </PropertyGroup>
</Project>
```


## .NET Framework 
Projects use `TargetFrameworkVersion` to identify version
``` *.csproj
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <TargetFrameworkVersion>v4.5.2</TargetFrameworkVersion>
  </PropertyGroup>
```

## Multiple frameworks 
With .NET Core 2 -> 3.1, .NET 5+, .NET Standard 1+, and .NET Framework: projects use `TargetFrameworks` with a ; separator to identify version
``` *.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFrameworks>netstandard1.4;net472;net5.0</TargetFrameworks>
  </PropertyGroup>
</Project>
```

Current benchmarks: (total unknown / total)
- aspnetcore: 566/735
- fsharp: 16/2482
- Mono: 753/1072
- PowerShell: 0/38
- roslyn: 77/324
- Runner: 0/9
- SDK: 181/ 463
- StackExchange.Redis: 0/1
- winforms: 42/49
- wpf: 0/51 
