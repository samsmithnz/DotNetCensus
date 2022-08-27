namespace DotNetCensus.Core.Models.GitHub
{
    public class RepoResponse
    {
        //"name": "Hello-World",
        //"archived": false,
        //"disabled": false,
        //"visibility": "public",

        public string? name { get; set; }
        public bool? archived { get; set; }
        public bool? disabled { get; set; }
        public string? visibility { get; set; }
        public string? default_branch { get; set; }
    }
}
