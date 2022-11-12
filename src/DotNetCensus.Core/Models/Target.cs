namespace DotNetCensus.Core.Models
{
    public class Target
    {
        public Target(string owner, string repo)
        {
            Owner = owner;
            Repository = repo;
        }
        
        public Target(string owner)
        {
            Owner = owner;
        }

        public string Owner { get; set; }
        public string? Repository { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
    }
}
