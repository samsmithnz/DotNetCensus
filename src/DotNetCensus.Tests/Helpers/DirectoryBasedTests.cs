global using DotNetCensus.Core;
global using DotNetCensus.Core.Models;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;

namespace DotNetCensus.Tests.Helpers;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class DirectoryBasedTests
{
    public string? SamplesPath { get; set; }

    public DirectoryBasedTests()
    {
        DirectoryInfo? currentPath = new(Directory.GetCurrentDirectory());
        if (currentPath != null)
        {
            //Hack: because of a .NET bug, I can't check that Parent is not null without adding a giant if that checks 6 times - and this is just a test... so suppressing the warning
#pragma warning disable CS8604 // Possible null reference argument.
            SamplesPath = Path.Combine(currentPath.Parent?.Parent?.Parent?.Parent?.Parent?.FullName, "samples");
#pragma warning restore CS8604 // Possible null reference argument.
        }

    }
}
