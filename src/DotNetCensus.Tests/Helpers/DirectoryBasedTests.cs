using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Helpers;
using System.Diagnostics;

namespace DotNetCensus.Tests.Helpers;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class DirectoryBasedTests
{
    public string? SamplesPath { get; set; }
    //public string? ConsoleExePath { get; set; }

    public DirectoryBasedTests()
    {
        DirectoryInfo? currentPath = new(Directory.GetCurrentDirectory());
        if (currentPath != null)
        {
            //Hack: because of a .NET bug, I can't check that Parent is not null without adding a giant if that checks 6 times - and this is just a test... so suppressing the warning
#pragma warning disable CS8604 // Possible null reference argument.
            SamplesPath = Path.Combine(currentPath.Parent?.Parent?.Parent?.Parent?.Parent?.FullName, "samples");
            //ConsoleExePath = Path.Combine(currentPath.Parent?.Parent?.Parent?.Parent?.Parent?.FullName, "src/DotNetCensus/bin/Debug/net7.0/dotnet-census.exe");
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }

    //protected Process? StartApplication(string arguments)
    //{
    //    ProcessStartInfo processStartInfo = new()
    //    {
    //        FileName = ConsoleExePath,
    //        Arguments = arguments,
    //        WindowStyle = ProcessWindowStyle.Hidden,
    //        CreateNoWindow = true,
    //        UseShellExecute = false,
    //        RedirectStandardInput = true,
    //        RedirectStandardOutput = true
    //    };

    //    return Process.Start(processStartInfo);
    //}

    //protected Task<string?> WaitForResponse(Process process)
    //{
    //    return Task.Run(() =>
    //    {
    //        string? output = process.StandardOutput.ReadLine();
    //        return output;
    //    });
    //}
}
