using ConsoleTables;
using DotNetCensus.Core;
using DotNetCensus.Core.Models;

namespace DotNetCensus
{
    public static class DataAccess
    {

        private static List<Project> GetProjects(string directory)
        {
            //Run the calculations to get and aggregate the results
            List<Project> projects = ProjectScanning.SearchDirectory(directory);
            //Need to sort so that Linux + Windows results are the same
            List<Project> sortedProjects = projects.OrderBy(o => o.FileName).ThenBy(o => o.Path).ToList();
            return sortedProjects;
        }

        public static void GetRawResults(string directory, string? outputFile)
        {
            List<Project> projects = GetProjects(directory);

            //If it's a raw output, remove the full path from each project
            foreach (Project item in projects)
            {
                item.Path = item.Path.Replace(directory, "");
            }

            if (string.IsNullOrEmpty(outputFile) == true)
            {
                //Create and output the table
                string text = ConsoleTable
                          .From<Project>(projects)
                          .Configure(o => o.NumberAlignment = Alignment.Right)
                          .ToString();
                Console.WriteLine(text);
                //                    .Write(Format.Minimal);
            }
            else
            {
                //Create a CSV file
                StreamWriter sw = File.CreateText(outputFile);
                sw.WriteLine("FileName,Path,FrameworkCode,FrameworkName,Family,Language,Status");
                foreach (Project item in projects)
                {
                    sw.WriteLine(item.FileName + "," +
                        item.Path + "," +
                        item.FrameworkCode + "," +
                        item.FrameworkName + "," +
                        item.Family + "," +
                        item.Language + "," +
                        item.Status);
                }
                sw.Close();

                //FileInfo fileInfo = new(_outputFile);
                Console.WriteLine($"Exported results to '{outputFile}'");
            }
        }

        public static void GetFrameworkSummary(string directory, bool includeTotals, string? outputFile)
        {
            List<Project> projects = GetProjects(directory);
            List<FrameworkSummary> results = Census.AggregateFrameworks(projects, includeTotals);

            if (string.IsNullOrEmpty(outputFile) == true)
            {
                //Create and output the table
                ConsoleTable
                    .From<FrameworkSummary>(results)
                    .Configure(o => o.NumberAlignment = Alignment.Right)
                    .Write(Format.Minimal);
            }
            else
            {
                //Create a CSV file
                StreamWriter sw = File.CreateText(outputFile);
                sw.WriteLine("Framework,FrameworkFamily,Count,Status");
                foreach (FrameworkSummary item in results)
                {
                    sw.WriteLine(item.Framework + "," +
                        item.FrameworkFamily + "," +
                        item.Count + "," +
                        item.Status);
                }
                sw.Close();

                //FileInfo fileInfo = new(_outputFile);
                Console.WriteLine($"Exported results to '{outputFile}'");
            }
        }
    }
}
