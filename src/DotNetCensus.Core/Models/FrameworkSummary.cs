namespace DotNetCensus.Core.Models
{
    public class FrameworkSummary
    {
        public string Framework { get; set; } = "";
        public string FrameworkFamily { get; set; } = "";
        public int Count { get; set; }
        public string? Color { get; set; }
    }
}
