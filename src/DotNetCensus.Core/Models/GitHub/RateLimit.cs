namespace DotNetCensus.Core.Models.GitHub
{
    public class RateLimit
    {
        //{
        //"resources": {
        //  "core": {
        //    "limit": 5000,
        //    "remaining": 4999,
        //    "reset": 1372700873,
        //    "used": 1
        //  },
        public RateLimitResources resources { get; set; }
    }
}
