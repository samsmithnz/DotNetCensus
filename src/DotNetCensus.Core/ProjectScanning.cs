using DotNetCensus.Core.APIs;
using DotNetCensus.Core.Models;
using DotNetCensus.Core.Models.GitHub;
using System.Text.Json;

namespace DotNetCensus.Core
{
    public static class ProjectScanning
    {
        public static List<Project> SearchDirectory(string directory, FileInfo? directoryBuildPropFile = null)
        {
            List<Project> projects = new();
            bool foundProjectFile = false;

            //Get all files for the current directory, looking for projects.
            foreach (FileInfo fileInfo in new DirectoryInfo(directory).GetFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                if (Classification.IsProjectFile(fileInfo.Name) == true)
                {
                    List<Project> directoryProjects = SearchProjectFile(fileInfo, null, directoryBuildPropFile);
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
                    List<Project> directoryProjects = SearchSecondaryProjects(fileInfo);
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
                    projects.AddRange(SearchDirectory(subDirectory.FullName, newDirectoryBuildPropFile));
                }
            }

            return projects;
        }

        public async static Task<List<Project>> SearchRepo(string? clientId, string? clientSecret,
            string owner, string repository, string branch = "main")
        {
            List<Project> projects = new();
            bool foundProjectFile = false;

            //Get all files for the current repo, looking for projects
            List<Project> repoProjects = await GitHubAPI.GetRepoFiles(clientId, clientSecret,
                   owner, repository, branch);

            foreach (Project project in repoProjects)
            {
                FileInfo fileInfo = new(project.FileName);
                System.Diagnostics.Debug.WriteLine(fileInfo.FullName);
                if (Classification.IsProjectFile(fileInfo.Name) == true)
                {
                    foundProjectFile = true;
                    FileDetails? fileDetails = await GitHubAPI.GetRepoFileContents(clientId, clientSecret,
                           owner, repository, project.Path);
                    if (fileDetails != null)
                    {
                        List<Project> directoryProjects = SearchProjectFile(fileInfo, fileDetails.content, null);
                        if (directoryProjects.Count > 0)
                        {
                            projects.AddRange(directoryProjects);
                            foundProjectFile = true;
                        }
                    }
                }
            }

            return projects;
        }

        private static List<Project> SearchProjectFile(FileInfo fileInfo, string? content, FileInfo? directoryBuildPropFile = null)
        {
            string fileName = fileInfo.Name;
            string filePath = fileInfo.FullName;
            if (content == null)
            {
                content = File.ReadAllText(filePath);
            }
            List<Project> projects = new();
            switch (fileInfo.Extension.ToLower())
            {
                case ".csproj":
                case ".sqlproj":
                    projects.AddRange(ProcessProjectFile(fileName, filePath, "csharp", content, directoryBuildPropFile));
                    break;
                case ".vbproj":
                    projects.AddRange(ProcessProjectFile(fileName, filePath, "vb.net", content, directoryBuildPropFile));
                    break;
                case ".fsproj":
                    projects.AddRange(ProcessProjectFile(fileName, filePath, "fsharp", content, directoryBuildPropFile));
                    break;
                case ".vbp":
                    projects.AddRange(ProcessProjectFile(fileName, filePath, "vb6", content));
                    break;
            }
            return projects;
        }

        private static List<Project> SearchSecondaryProjects(FileInfo fileInfo)
        {
            string fileName = fileInfo.Name;
            string filePath = fileInfo.FullName;
            string content = File.ReadAllText(filePath);
            List<Project> projects = new();

            //is it a .NET Core 1.0 or 1.1 project? These didn't use the project file format...
            if (fileInfo != null && fileInfo.Directory != null &&
                fileInfo.Name == "project.json")
            {
                //Check to see if it's a VB.NET or C# project
                string language = Classification.GetLanguage(fileInfo.Directory.FullName);
                projects.AddRange(ProcessProjectFile(fileName, filePath, language, content));
            }
            //is it a .NET Framework 2.0 or 3.5 web site - which has no project file
            else if (fileInfo != null && fileInfo.Directory != null &&
                fileInfo.Name == "web.config")
            {
                //Check to see if it's a VB.NET or C# project
                string language = Classification.GetLanguage(fileInfo.Directory.FullName);
                projects.AddRange(ProcessProjectFile(fileName, filePath, language, content));
            }

            //    //Is it a Unity3d project?
            //    if (fileInfo.Name == "ProjectVersion.txt")
            //    {
            //        projects.AddRange(ProcessDotNetProjectFile(fileName, filePath, "csharp", content));
            //    }

            return projects;
        }


        //Process individual project files
        private static List<Project> ProcessProjectFile(string fileName, string filePath, string language, string content, FileInfo? directoryBuildPropFile = null)
        {
            string[] lines = content.Split('\n');

            List<Project> projects = new();

            //Setup the project object
            Project? project = new()
            {
                FileName = fileName, //new FileInfo(filePath).Name,
                Path = filePath,
                Language = language
            };

            if (language == "vb6")
            {
                project.FrameworkCode = "vb6";
            }
            else if (new FileInfo(filePath).Name == "project.json")
            {
                //Load it into a JSON object
                JsonElement jsonObject = JsonSerializer.Deserialize<JsonElement>(content);
                //Search for the project version
                //jsonObject.TryGetProperty("frameworks", out JsonElement jsonElement);
                if (jsonObject.TryGetProperty("frameworks", out JsonElement jsonElement))
                {
                    foreach (JsonProperty item in jsonElement.EnumerateObject())
                    {
                        if (item.NameEquals("netcoreapp1.0") == true)
                        {
                            project.FrameworkCode = "netcoreapp1.0";
                            break;
                        }
                        else if (item.NameEquals("netcoreapp1.1") == true)
                        {
                            project.FrameworkCode = "netcoreapp1.1";
                            break;
                        }
                    }
                }
                else
                {
                    project = null;
                }
            }
            else if (new FileInfo(filePath).Name == "web.config")
            {
                foreach (string line in lines)
                {
                    if (line?.IndexOf("<add assembly=\"System.Core, Version=") >= 0)
                    {
                        string version = line.Replace("<add assembly=\"System.Core, Version=", "")
                            .Replace(", Culture=neutral, PublicKeyToken=B77A5C561934E089\"/>", "").Trim();
                        project.FrameworkCode = "v" + version[..3]; //.Substring(0, 3)
                        break;
                    }
                }
            }
            else
            {
                //scan the project file to identify the framework
                foreach (string line in lines)
                {
                    //.NET Framework version element
                    if (line.IndexOf("<TargetFrameworkVersion>") > 0)
                    {
                        project.FrameworkCode = CheckFrameworkCodeForVariable(line.Replace("<TargetFrameworkVersion>", "").Replace("</TargetFrameworkVersion>", "").Trim(), directoryBuildPropFile);
                        break;
                    }
                    //.NET Core version element
                    else if (line.IndexOf("<TargetFramework>") > 0)
                    {
                        project.FrameworkCode = CheckFrameworkCodeForVariable(line.Replace("<TargetFramework>", "").Replace("</TargetFramework>", "").Trim(), directoryBuildPropFile);
                        break;
                    }
                    //Multiple .NET flavors element
                    else if (line.IndexOf("<TargetFrameworks>") > 0)
                    {
                        string frameworks = CheckFrameworkCodeForVariable(line.Replace("<TargetFrameworks>", "").Replace("</TargetFrameworks>", "").Trim(), directoryBuildPropFile);
                        string[] frameworkList = frameworks.Split(';');
                        for (int i = 0; i < frameworkList.Length - 1; i++)
                        {
                            if (i == 0)
                            {
                                project.FrameworkCode = CheckFrameworkCodeForVariable(frameworkList[i], directoryBuildPropFile);
                            }
                            else
                            {
                                Project additionalProject = new()
                                {
                                    FileName = new FileInfo(filePath).Name,
                                    Path = filePath,
                                    Language = language,
                                    FrameworkCode = CheckFrameworkCodeForVariable(frameworkList[i], directoryBuildPropFile)
                                };
                                projects.Add(additionalProject);
                            }
                        }
                        break;
                    }
                    //Visual Studio version (for old .NET Framework versions that were tied directly to Visual Studio versions) 
                    else if (line.IndexOf("<ProductVersion>") > 0 ||
                             line.IndexOf("ProductVersion = ") > 0)
                    {
                        project.FrameworkCode = Classification.GetHistoricalFrameworkVersion(line);
                        //Note: Since product version could appear first in the lines list, and we could still find a target version, don't break out of the loop
                    }
                    ////Unity 3d project files
                    //else if (line.Contains("m_EditorVersion:"))
                    //{
                    //    project.Framework = GetUnityFrameworkVersion(line);
                    //    break;
                    //}
                }
            }

            if (project != null)
            {
                projects.Add(project);
            }

            //Add colors and families
            foreach (Project item in projects)
            {
                item.Status = Classification.GetStatus(item.FrameworkCode);
                item.Family = Classification.GetFrameworkFamily(item.FrameworkCode);
                item.FrameworkName = Classification.GetFriendlyName(item.FrameworkCode, item.Family);
            }

            return projects;
        }

        //Check to see if the framework 
        private static string CheckFrameworkCodeForVariable(string variable, FileInfo? directoryBuildProps)
        {
            if (variable.StartsWith("$(") == true && variable.EndsWith(")") == true)
            {
                //Open the Directory.Build.props file and look for the variable
                string searchVariable = variable.Replace("$(", "").Replace(")", "");
                if (directoryBuildProps != null)
                {
                    string[] lines = File.ReadAllLines(directoryBuildProps.FullName);
                    foreach (string line in lines)
                    {
                        if (line?.IndexOf(searchVariable) >= 0)
                        {
                            variable = line.Replace("<" + searchVariable + ">", "")
                                         .Replace("</" + searchVariable + ">", "")
                                         .Trim();
                            break;
                        }
                    }
                }
            }
            return variable;
        }

    }
}
