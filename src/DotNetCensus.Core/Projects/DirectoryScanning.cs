using DotNetCensus.Core.Models;

namespace DotNetCensus.Core.Projects
{
    public static class DirectoryScanning
    {
        //Recursively search directories until a project file is found
        public static List<Project> SearchDirectory(string directory, string? propFileContent = null)
        {
            List<Project> projects = new();
            bool foundProjectFile = false;
            string? currentPropFileContent = propFileContent;

            //Get all files for the current directory, looking for projects.
            foreach (FileInfo fileInfo in new DirectoryInfo(directory).GetFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                if (ProjectClassification.IsProjectFile(fileInfo.Name))
                {
                    List<Project> directoryProjects = ProjectFileProcessing.SearchProjectFile(fileInfo, fileInfo.FullName, null, currentPropFileContent);
                    if (directoryProjects.Count > 0)
                    {
                        projects.AddRange(directoryProjects);
                        foundProjectFile = true;
                    }
                }
            }

            //If we didn't find projects in the initial pass, do a secondary pass looking for more obscurce and older projects
            if (!foundProjectFile)
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
            if (!foundProjectFile)
            {
                //Check for .props files 
                List<FileInfo> propFiles = new DirectoryInfo(directory).GetFiles("*.props", SearchOption.TopDirectoryOnly).ToList();
                if (propFiles.Count > 0)
                {
                    foreach (FileInfo fileInfo in propFiles)
                    {
                        //If there is a directory file being passed in - convert it to content
                        if (fileInfo != null)
                        {
                            if (propFileContent == null)
                            {
                                propFileContent = "";
                            }
                            propFileContent += File.ReadAllText(fileInfo.FullName);
                        }
                    }
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
                        projects.AddRange(SearchDirectory(subDirectory.FullName, propFileContent));
                    }
                }
            }

            return projects;
        }

    }
}
