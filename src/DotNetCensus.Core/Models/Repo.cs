namespace DotNetCensus.Core.Models
{
    public class Repo
    {
        public Repo(string owner, string repo)
        {
            Owner = owner;
            Repository = repo;
        }

        public string Owner { get; set; }
        public string Repository { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
        public string? Branch { get; set; }
    }
}
