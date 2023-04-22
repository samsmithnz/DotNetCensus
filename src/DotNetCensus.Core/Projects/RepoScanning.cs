using DotNetCensus.Core.APIs;
using DotNetCensus.Core.Models;
using DotNetCensus.Core.Models.GitHub;

namespace DotNetCensus.Core.Projects
{
    public static class RepoScanning
    {
        public async static Task<List<Project>> SearchRepo(string? clientId, string? clientSecret,
            string owner, string repository, string branch = "main")
        {
            //Get all files for the current repo, looking for projects
            List<Project> repoProjects = await GitHubAPI.GetRepoFiles(clientId, clientSecret,
                   owner, repository, branch);

            //Recreate the folder structure with the primary directory, sub-directories, and any files. 
            RepoDirectory baseDir = CreateRepoDirectoryStructure(repoProjects);

            //Recursively search directories until a project file is found
            List<Project> projects = new();
            if (baseDir != null && baseDir.Path != null)
            {
                projects = await SearchRepoDirectory(baseDir, baseDir.Path,
                    clientId, clientSecret,
                    owner, repository, branch);
            }

            return projects;
        }

        private async static Task<List<Project>> SearchRepoDirectory(RepoDirectory baseDir, string fullPath,
            string? clientId, string? clientSecret,
            string owner, string repository, string branch,
            string? directoryBuildPropFileContent = null,
            int currentRecursionLevel = 1)
        {
            List<Project> projects = new();
            bool foundProjectFile = false;
            System.Diagnostics.Debug.WriteLine("Processing " + baseDir.Name + " at " + fullPath);

            string? newDirectoryBuildPropFileContent = null;
            if (directoryBuildPropFileContent != null)
            {
                newDirectoryBuildPropFileContent = directoryBuildPropFileContent;
            }

            //Now that the files are arranged in a directory/tree-like structure, start the simulated search
            if (baseDir.Files.Count > 0)
            {
                foreach (string file in baseDir.Files)
                {
                    if (ProjectClassification.IsProjectFile(file))
                    {
                        FileInfo fileInfo = new(file);
                        string filePath = (fullPath + "/" + file).Replace("//", "/");
                        FileDetails? fileDetails = await GitHubAPI.GetRepoFileContents(clientId, clientSecret,
                               owner, repository, filePath, branch);
                        List<Project> directoryProjects = ProjectFileProcessing.SearchProjectFile(fileInfo, filePath, fileDetails?.content, newDirectoryBuildPropFileContent);
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
                    foreach (string file in baseDir.Files)
                    {
                        FileInfo fileInfo = new(file);
                        if (ProjectClassification.IsProjectFile(file, false))
                        {
                            foundProjectFile = true;
                            string filePath = (fullPath + "/" + file).Replace("//", "/");
                            FileDetails? fileDetails = await GitHubAPI.GetRepoFileContents(clientId, clientSecret,
                                   owner, repository, filePath, branch);
                            if (fileDetails != null)
                            {
                                List<Project> directoryProjects = ProjectFileProcessing.SearchSecondaryProjects(fileInfo, filePath, fileDetails?.content);
                                if (directoryProjects.Count > 0)
                                {
                                    projects.AddRange(directoryProjects);
                                    foundProjectFile = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            //If we still didn't find a project, then look deeper in the sub-directories.
            if (!foundProjectFile)
            {
                //Check for a Directory.Build.props file first
                foreach (string file in baseDir.Files)
                {
                    if (file == "Directory.Build.props")
                    {
                        string filePath = (fullPath + "/" + file).Replace("//", "/");
                        FileDetails? fileDetails = await GitHubAPI.GetRepoFileContents(clientId, clientSecret,
                               owner, repository, filePath, branch);
                        if (fileDetails != null)
                        {
                            if (newDirectoryBuildPropFileContent == null)
                            {
                                newDirectoryBuildPropFileContent = "";
                            }
                            newDirectoryBuildPropFileContent += fileDetails.content;
                        }
                        break;
                    }
                }
                HashSet<string> foldersDone = new();
                foreach (RepoDirectory subDirectory in baseDir.Directories)
                {
                    string filePath = (fullPath + "/" + subDirectory.Name).Replace("//", "/");
                    List<Project> projects2 = await SearchRepoDirectory(subDirectory, filePath,
                        clientId, clientSecret,
                        owner, repository, branch,
                        newDirectoryBuildPropFileContent,
                        currentRecursionLevel + 1);
                    if (subDirectory != null && subDirectory.Name != null &&
                        projects2.Count > 0 &&
                        !foldersDone.Contains(subDirectory.Name))
                    {
                        projects.AddRange(projects2);
                        foldersDone.Add(subDirectory.Name);
                        //break;
                    }
                }
            }

            return projects;
        }

        public static RepoDirectory CreateRepoDirectoryStructure(List<Project> projects)
        {
            //Create a base repo directory
            RepoDirectory baseDir = new()
            {
                Name = "",
                Path = "/"
            };

            //Loop through every project.
            foreach (Project project in projects)
            {
                //Look at each level of the path
                string[] dirs = (project.Path + project.FileName).Split('/');
                //Drop any empty items from the array
                dirs = CleanArrayOfEmptyValues(dirs);
                Queue<string> dirQueue = new(dirs);

                if (dirQueue.Count > 1)
                {
                    ParseDirectorys(baseDir, dirQueue);
                }
                else if (dirQueue.Count == 1)
                {
                    baseDir.Files.Add(dirQueue.Dequeue());
                }
            }
            return baseDir;
        }

        private static void ParseDirectorys(RepoDirectory? baseDir, Queue<string> dirQueue)
        {
            string name = dirQueue.Dequeue();

            if (baseDir != null)
            {
                //Add any directories missing
                if (!baseDir.Directories.Any(r => r.Name == name))
                {
                    baseDir.Directories.Add(new()
                    {
                        Name = name,
                        Path = baseDir.Path + name + "/"
                    });
                }

                //If there are still items to process, recursively add sub directories
                if (dirQueue.Count > 1 && baseDir != null && baseDir.Directories != null)
                {
                    ParseDirectorys(baseDir.Directories.Find(r => r.Name == name), dirQueue);
                }
                else if (dirQueue.Count == 1 &&
                    baseDir != null &&
                    baseDir.Directories != null)
                {
                    //Add files in the correct directory position
                    baseDir?.Directories?.Find(r => r.Name == name)?.Files.Add(dirQueue.Dequeue());
                }
            }
        }

        //Clean a string array of any empty/"" or null values
        private static string[] CleanArrayOfEmptyValues(string[] array)
        {
            List<string> items = new(array);
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrEmpty(items[i]))
                {
                    items.RemoveAt(i);
                }
            }
            return items.ToArray();
        }

    }
}
