namespace DotNetCensus.Core.Models
{
    public class Project
    {
        public string FileName { get; set; } = "";
        public string Path { get; set; } = "";
        public string FrameworkCode { get; set; } = "";
        public string FrameworkName { get; set; } = "";
        public string? Family { get; set; }
        public string? Language { get; set; }
        public string? Color { get; set; }
    }
}
