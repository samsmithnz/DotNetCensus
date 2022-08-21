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
                    owner, repository);
            }

            return projects;
        }

        private async static Task<List<Project>> SearchRepoDirectory(RepoDirectory baseDir, string fullPath,
            string? clientId, string? clientSecret,
            string owner, string repository,
            string? directoryBuildPropFileContent = null)
        {
            List<Project> projects = new();
            bool foundProjectFile = false;
            System.Diagnostics.Debug.WriteLine("Processing " + baseDir.Name + " at " + fullPath);

            //Now that the files are arranged in a directory/tree-like structure, start the simulated search
            foreach (string file in baseDir.Files)
            {
                if (ProjectClassification.IsProjectFile(file) == true)
                {
                    FileInfo fileInfo = new(file);
                    string filePath = (fullPath + "/" + file).Replace("//", "/");
                    FileDetails? fileDetails = await GitHubAPI.GetRepoFileContents(clientId, clientSecret,
                           owner, repository, filePath);
                    List<Project> directoryProjects = ProjectFileProcessing.SearchProjectFile(fileInfo, filePath, fileDetails?.content, null, directoryBuildPropFileContent);
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
                foreach (string file in baseDir.Files)
                {
                    FileInfo fileInfo = new(file);
                    if (ProjectClassification.IsProjectFile(file, false) == true)
                    {
                        foundProjectFile = true;
                        string filePath = (fullPath + "/" + file).Replace("//", "/");
                        FileDetails? fileDetails = await GitHubAPI.GetRepoFileContents(clientId, clientSecret,
                               owner, repository, filePath);
                        if (fileDetails != null)
                        {
                            List<Project> directoryProjects = ProjectFileProcessing.SearchSecondaryProjects(fileInfo, filePath, fileDetails?.content);
                            if (directoryProjects.Count > 0)
                            {
                                projects.AddRange(directoryProjects);
                                foundProjectFile = true;
                            }
                        }
                    }
                }
            }

            //If we still didn't find a project, then look deeper in the sub-directories.
            if (foundProjectFile == false)
            {
                //Check for a Directory.Build.props file first
                string? newDirectoryBuildPropFile = null;
                foreach (string file in baseDir.Files)
                {
                    if (file == "Directory.Build.props")
                    {
                        string filePath = (fullPath + "/" + file).Replace("//", "/");
                        FileDetails? fileDetails = await GitHubAPI.GetRepoFileContents(clientId, clientSecret,
                               owner, repository, filePath);
                        if (fileDetails != null)
                        {
                            newDirectoryBuildPropFile = fileDetails.content;
                        }
                        //newDirectoryBuildPropFile = directoryBuildPropFile;
                        break;
                    }
                }
                //List<FileInfo> directoryBuildPropFiles = new DirectoryInfo(directory).GetFiles("Directory.Build.props", SearchOption.TopDirectoryOnly).ToList();
                //if (directoryBuildPropFile != null)
                //{
                //    newDirectoryBuildPropFile = directoryBuildPropFile;
                //}
                //else if (directoryBuildPropFiles.Count > 0)
                //{
                //    newDirectoryBuildPropFile = directoryBuildPropFiles[0];
                //}
                foreach (RepoDirectory subDirectory in baseDir.Directories)
                {
                    string filePath = (fullPath + "/" + subDirectory.Name).Replace("//", "/");
                    projects.AddRange(await SearchRepoDirectory(subDirectory, filePath,
                        clientId, clientSecret,
                        owner, repository,
                        newDirectoryBuildPropFile));
                }
            }

            return projects;
        }

        public static RepoDirectory CreateRepoDirectoryStructure(List<Project> projects)
        {
            RepoDirectory baseDir = new();
            foreach (Project project in projects)
            {
                if (project.Path.Contains("Sample.NetCore1.1.ConsoleApp"))
                {
                    int i = 0;
                }
                string[] dirs = (project.Path + project.FileName).Split('/');
                //Drop any empty items from the array
                dirs = CleanArrayOfEmptyValues(dirs);
                Queue<string> dirQueue = new(dirs);

                //Create the root directory
                if (string.IsNullOrEmpty(baseDir.Name) == true)
                {
                    baseDir.Name = "";
                    baseDir.Path = "/";
                }
                //If there is only one item left, it's a file!
                if (dirQueue.Count == 1)
                {
                    baseDir.Files.Add(dirQueue.Dequeue());
                }
                else
                {
                    //Check if directory already exists in list - we don't want to add the same folder twice
                    if (baseDir.Directories.Any(item => item.Name == dirQueue.Peek()) == false)
                    {
                        baseDir.Directories.Add(CreateRepoDirectoryStructure(dirQueue));
                    }
                    else
                    {
                        //Merge the duplicate directory with the existing directory
                        RepoDirectory? baseDir2 = baseDir.Directories.Find(x => x.Name == dirQueue.Peek());
                        //process the duplicate directory from the queue
                        RepoDirectory repoDirectory = CreateRepoDirectoryStructure(dirQueue);
                        if (baseDir2 != null)
                        {
                            baseDir2.Directories.AddRange(repoDirectory.Directories);
                            baseDir2.Files.AddRange(repoDirectory.Files);
                        }
                    }
                }
            }
            return baseDir;
        }

        private static string[] CleanArrayOfEmptyValues(string[] array)
        {
            List<string> items = new(array);
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrEmpty(items[i]) == true)
                {
                    items.RemoveAt(i);
                }
            }
            return items.ToArray();
        }

        private static RepoDirectory CreateRepoDirectoryStructure(Queue<string> dirQueue)
        {
            RepoDirectory repoDirectory = new()
            {
                Name = dirQueue.Dequeue()
            };
            //If there is just one item in the queue, then it's a file
            if (dirQueue.Count == 1)
            {
                repoDirectory.Files.Add(dirQueue.Dequeue());
            }
            else
            {
                repoDirectory.Directories.Add(CreateRepoDirectoryStructure(dirQueue));
            }
            return repoDirectory;

            //    if (i == 0)
            //    {
            //        baseDir.Name = dirs[i];
            //        baseDir.Path = dirs[i];
            //        if (i == dirs.Length - 1)
            //        {
            //            baseDir.Files.Add(dirs[i]);
            //        }
            //    }
            //    else if (i < dirs.Length - 1)
            //    {
            //        baseDir.Directories.AddRange(ProcessDirectory(dirs));
            //        //if (baseDir.Directories.FirstOrDefault(d => d.Name == dirs[i]) == null)
            //        //{
            //        //    RepoDirectory subDir = new()
            //        //    {
            //        //        Name = dirs[i],
            //        //        Path = baseDir.Path + "/" + dirs[i]
            //        //    };
            //        //    if (i + 1 == dirs.Length - 1)
            //        //    {
            //        //        subDir.Files.Add(dirs[i + 1]);
            //        //    }
            //        //    baseDir.Directories.Add(subDir);
            //        //}
            //    }

            //return new List<RepoDirectory>();
        }

    }
}
