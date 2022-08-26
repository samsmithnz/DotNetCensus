namespace DotNetCensus.Core.Models.GitHub
{
    public class RateLimitResources
    {
        //{
        //"resources": {
        //  "core": {
        //    "limit": 5000,
        //    "remaining": 4999,
        //    "reset": 1372700873,
        //    "used": 1
        //  },
        public RateLimitCore core { get; set; }
    }
}
