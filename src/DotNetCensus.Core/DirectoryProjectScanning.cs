using DotNetCensus.Core.APIs;
using DotNetCensus.Core.Models;
using DotNetCensus.Core.Models.GitHub;
using System.Text.Json;

namespace DotNetCensus.Core
{
    public static class DirectoryProjectScanning
    {
        //Searches sub directories until we find a project file
        public static List<Project> SearchDirectory(string directory, FileInfo? directoryBuildPropFile = null)
        {
            List<Project> projects = new();
            bool foundProjectFile = false;

            //Get all files for the current directory, looking for projects.
            foreach (FileInfo fileInfo in new DirectoryInfo(directory).GetFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                if (Classification.IsProjectFile(fileInfo.Name) == true)
                {
                    List<Project> directoryProjects = ProjectFileProcessing.SearchProjectFile(fileInfo, fileInfo.FullName, null, directoryBuildPropFile);
                    if (directoryProjects.Count > 0)
                    {
                        projects.AddRange(directoryProjects);
                        foundProjectFile = true;
                    }
                }
            }

            //If we didn't find projects in the initial pass, do a secondary pass looking for more obscurce and older projects
            if (foundProjectFile == false)
            {
                foreach (FileInfo fileInfo in new DirectoryInfo(directory).GetFiles("*.*", SearchOption.TopDirectoryOnly))
                {
                    List<Project> directoryProjects = ProjectFileProcessing.SearchSecondaryProjects(fileInfo);
                    if (directoryProjects.Count > 0)
                    {
                        projects.AddRange(directoryProjects);
                        foundProjectFile = true;
                    }
                }
            }

            //If we still didn't find a project, then look deeper in the sub-directories.
            if (foundProjectFile == false)
            {
                //Check for a Directory.Build.props file first
                FileInfo? newDirectoryBuildPropFile = null;
                List<FileInfo> directoryBuildPropFiles = new DirectoryInfo(directory).GetFiles("Directory.Build.props", SearchOption.TopDirectoryOnly).ToList();
                if (directoryBuildPropFile != null)
                {
                    newDirectoryBuildPropFile = directoryBuildPropFile;
                }
                else if (directoryBuildPropFiles.Count > 0)
                {
                    newDirectoryBuildPropFile = directoryBuildPropFiles[0];
                }
                foreach (DirectoryInfo subDirectory in new DirectoryInfo(directory).GetDirectories())
                {
                    //Prevent blocking when debugging in Visual Studio.
                    if (subDirectory.Name != ".vs")
                    {
                        projects.AddRange(SearchDirectory(subDirectory.FullName, newDirectoryBuildPropFile));
                    }
                }
            }

            return projects;
        }

    }
}
