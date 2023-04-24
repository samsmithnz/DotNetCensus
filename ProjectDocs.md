# Project docs for each version of .NET

## .NET Core 2 -> 3.1, .NET 5+, .NET Standard 1+, uses TargetFramework
``` *.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
  </PropertyGroup>
</Project>
```

## .NET Framework 1->4.8 (excluding .NET 3.5 web projects), uses TargetFrameworkVersion
``` *.csproj
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <TargetFrameworkVersion>v4.5.2</TargetFrameworkVersion>
  </PropertyGroup>
```