namespace DotNetCensus.Core.Models.GitHub;

public class RepoDirectory
{
    public string? Name { get; set; }
    public string? Path { get; set; }
    public List<RepoDirectory> Directories { get; set; } = new();
    public List<string> Files { get; set; } = new();
}
