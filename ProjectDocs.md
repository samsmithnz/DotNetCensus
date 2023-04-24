# Project docs for each version of .NET

## .NET Core, .NET 5+, and .NET Standard based projects uses `TargetFramework`
``` *.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
  </PropertyGroup>
</Project>
```


## .NET Framework based projects use `TargetFrameworkVersion`
``` *.csproj
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <TargetFrameworkVersion>v4.5.2</TargetFrameworkVersion>
  </PropertyGroup>
```

## When targeting multiple frameworks with .NET Core 2 -> 3.1, .NET 5+, .NET Standard 1+, and .NET Framework: projects use `TargetFrameworks` with a ; separator
``` *.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFrameworks>netstandard1.4;net472;net5.0</TargetFrameworks>
  </PropertyGroup>
</Project>
```
