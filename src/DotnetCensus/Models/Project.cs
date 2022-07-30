namespace DotNetCensus.Core.Models
{
    public class Project
    {
        //public Project (string fileName, string path)
        //{
        //    FileName = fileName;
        //    Path = path;
        //}

        public string? FileName { get; set; }
        public string? Path { get; set; }
        //public string? Content { get; set; }
        //public string? Family { get; set; }
        public string? Framework { get; set; }
        public string? Language { get; set; }
        public string? Color { get; set; }

    }
}
