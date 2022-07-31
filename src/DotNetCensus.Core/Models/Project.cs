namespace DotNetCensus.Core.Models
{
    public class Project
    {
        //public Project(string fileName, string path, string framework)
        //{
        //    FileName = fileName;
        //    Path = path;
        //    Framework = framework;
        //}

        public string FileName { get; set; } = "";
        public string Path { get; set; } = "";
        public string Framework { get; set; } = "";
        public string? Language { get; set; }
        public string? Family { get; set; }
        public string? Color { get; set; }
        
        //public string? Content { get; set; }

    }
}
