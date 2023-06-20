namespace DotNetCensus.Core.Models.GitHub;

public class RepoResponse
{
    //"name": "DotNetCensus",
    //"clone_url": "https://github.com/SamSmithNZ/DotNetCensus.git"

    public RepoResponse()
    {

    }

    public string? name { get; set; }
    public string? clone_url { get; set; }
}
