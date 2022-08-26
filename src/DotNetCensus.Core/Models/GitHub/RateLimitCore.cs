namespace DotNetCensus.Core.Models.GitHub
{
    public class RateLimitCore
    {
        //    "limit": 5000,
        //    "remaining": 4999,
        //    "reset": 1372700873,
        //    "used": 1
        public int limit { get; set; }
        public int remaining { get; set; }
        public int reset { get; set; }
        public int used { get; set; }
    }
}
