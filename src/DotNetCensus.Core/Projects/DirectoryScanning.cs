using DotNetCensus.Core.Models;
using System.Diagnostics;

namespace DotNetCensus.Core.Projects
{
    public static class DirectoryScanning
    {
        //Recursively search directories until a project file is found
        public static List<Project> SearchDirectory(string directory, FileInfo? directoryBuildPropFile = null)
        {
            List<Project> projects = new();
            bool foundProjectFile = false;

            //Get all files for the current directory, looking for projects.
            foreach (FileInfo fileInfo in new DirectoryInfo(directory).GetFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                if (ProjectClassification.IsProjectFile(fileInfo.Name) == true)
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
                    List<Project> directoryProjects = ProjectFileProcessing.SearchSecondaryProjects(fileInfo, fileInfo.FullName, null);
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
                    //Avoid the .git and .vs directories, they tend to be large, slow down the scan, and we don't need the data within them.
                    if (subDirectory.Name != ".devcontainer" && 
                        subDirectory.Name != ".git" && 
                        subDirectory.Name != ".github" && 
                        subDirectory.Name != ".vs" && 
                        subDirectory.Name != ".vscode")
                    {
                        projects.AddRange(SearchDirectory(subDirectory.FullName, newDirectoryBuildPropFile));
                    }
                }
            }

            return projects;
        }

    }
}
