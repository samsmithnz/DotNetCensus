using DotNetCensus.Core.Models;
using System.Text.Json;

namespace DotNetCensus.Core
{
    //TODO: Break this into smaller classes
    public static class ProjectScanning
    {
        public static List<Project> SearchDirectory(string directory, FileInfo? directoryBuildPropFile = null)
        {
            List<Project> projects = new();
            bool foundProjectFile = false;

            //Get all files for the current directory, looking for projects.
            foreach (FileInfo fileInfo in new DirectoryInfo(directory).GetFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                List<Project> directoryProjects = SearchProjects(fileInfo, directoryBuildPropFile);
                if (directoryProjects.Count > 0)
                {
                    projects.AddRange(directoryProjects);
                    foundProjectFile = true;
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

        private static List<Project> SearchProjects(FileInfo fileInfo, FileInfo? directoryBuildPropFile = null)
        {
            List<Project> projects = new();
            switch (fileInfo.Extension.ToLower())
            {
                case ".csproj":
                case ".sqlproj":
                    projects.AddRange(ProcessProjectFile(fileInfo.FullName, "csharp", directoryBuildPropFile));
                    break;
                case ".vbproj":
                    projects.AddRange(ProcessProjectFile(fileInfo.FullName, "vb.net", directoryBuildPropFile));
                    break;
                case ".fsproj":
                    projects.AddRange(ProcessProjectFile(fileInfo.FullName, "fsharp", directoryBuildPropFile));
                    break;
                case ".vbp":
                    projects.AddRange(ProcessProjectFile(fileInfo.FullName, "vb6"));
                    break;
            }
            return projects;
        }

        private static List<Project> SearchSecondaryProjects(FileInfo fileInfo)
        {
            List<Project> projects = new();

            //is it a .NET Core 1.0 or 1.1 project? These didn't use the project file format...
            if (fileInfo != null && fileInfo.Directory != null &&
                fileInfo.Name == "project.json")
            {
                //Check to see if it's a VB.NET or C# project
                string language = GetLanguage(fileInfo.Directory.FullName);
                projects.AddRange(ProcessProjectFile(fileInfo.FullName, language));
            }
            //Is it a .NET Framework 2.0 or 3.5 web site - which has no project file
            else if (fileInfo != null && fileInfo.Directory != null &&
                fileInfo.Name == "web.config")
            {
                //Check to see if it's a VB.NET or C# project
                string language = GetLanguage(fileInfo.Directory.FullName);
                projects.AddRange(ProcessProjectFile(fileInfo.FullName, language));
            }

            //    //Is it a Unity3d project?
            //    if (fileInfo.Name == "ProjectVersion.txt")
            //    {
            //        projects.AddRange(ProcessDotNetProjectFile(fileInfo.FullName, "csharp"));
            //    }

            return projects;
        }


        //Process individual project files
        private static List<Project> ProcessProjectFile(string filePath, string language, FileInfo? directoryBuildPropFile = null)
        {
            string[] lines = File.ReadAllLines(filePath);

            List<Project> projects = new();

            //Setup the project object
            Project? project = new()
            {
                FileName = new FileInfo(filePath).Name,
                Path = filePath,
                Language = language
            };

            if (language == "vb6")
            {
                project.FrameworkCode = "vb6";
            }
            else if (new FileInfo(filePath).Name == "project.json")
            {
                //Get the text of the file
                string contents = File.ReadAllText(filePath);
                //Load it into a JSON object
                JsonElement jsonObject = JsonSerializer.Deserialize<JsonElement>(contents);
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
                        project.FrameworkCode = GetHistoricalFrameworkVersion(line);
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
                item.Status = GetStatus(item.FrameworkCode);
                item.Family = GetFrameworkFamily(item.FrameworkCode);
                item.FrameworkName = GetFriendlyName(item.FrameworkCode, item.Family);
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

        public static string GetFrameworkFamily(string frameworkCode)
        {
            if (string.IsNullOrEmpty(frameworkCode) == true)
            {
                return "(Unknown)";
            }
            else if (frameworkCode.StartsWith("netstandard"))
            {
                return ".NET Standard";
            }
            else if (frameworkCode.StartsWith("v1.") ||
                     frameworkCode.StartsWith("v2.") ||
                     frameworkCode.StartsWith("v3.") ||
                     frameworkCode.StartsWith("v4.") ||
                     frameworkCode.StartsWith("net4") ||
                     frameworkCode.StartsWith("Unity"))
            {
                return ".NET Framework";
            }
            else if (frameworkCode.StartsWith("netcoreapp"))
            {
                return ".NET Core";
            }
            else if (frameworkCode.StartsWith("net")) //net5.0, net6.0, etc)
            {
                return ".NET";
            }
            else if (frameworkCode.StartsWith("vb6"))
            {
                return "Visual Basic 6";
            }
            else
            {
                return "(Unknown)";
            }
        }

        public static string GetFriendlyName(string frameworkCode, string family)
        {

            if (string.IsNullOrEmpty(frameworkCode) == true)
            {
                return "(Unknown)";
            }
            else if (frameworkCode.StartsWith("netstandard"))
            {
                return family + " " + frameworkCode.Replace("netstandard", "");
            }
            else if (frameworkCode.StartsWith("v1.") ||
                     frameworkCode.StartsWith("v2.") ||
                     frameworkCode.StartsWith("v3.") ||
                     frameworkCode.StartsWith("v4."))
            {
                //Drop the v from the version string. (e.g. v3.0 becomes 3.0)
                return family + " " + frameworkCode.Replace("v", "");
            }
            else if (frameworkCode.StartsWith("net4"))
            {
                string number = frameworkCode.Replace("net", "");
                string formattedNumber = "";
                //Add .'s between each number. Gross. (e.g. net462 becomes 4.6.2)
                for (int i = 0; i < number.Length; i++)
                {
                    formattedNumber += number[i];
                    if (i < number.Length - 1)
                    {
                        formattedNumber += ".";
                    }
                }
                return family + " " + formattedNumber;
            }
            else if (frameworkCode.StartsWith("netcoreapp"))
            {
                return family + " " + frameworkCode.Replace("netcoreapp", "");
            }
            else if (frameworkCode.StartsWith("net")) //net5.0, net6.0, etc)
            {
                return family + " " + frameworkCode.Replace("net", "");
            }
            else if (frameworkCode.StartsWith("vb6"))
            {
                return "Visual Basic 6";
            }
            else
            {
                return "(Unknown)";
            }
        }

        private static string GetHistoricalFrameworkVersion(string line)
        {
            string productVersion = line.Replace("<ProductVersion>", "").Replace("</ProductVersion>", "").Replace("ProductVersion = ", "").Replace("\"", "").Trim();
            //https://en.wikipedia.org/wiki/Microsoft_Visual_Studio#History
            //+---------------------------+---------------+-----------+----------------+
            //|       Product name        |   Codename    | Version # | .NET Framework | 
            //+---------------------------+---------------+-----------+----------------+
            //| Visual Studio .NET (2002) | Rainier       | 7.0.*     | 1              |
            //| Visual Studio .NET 2003   | Everett       | 7.1.*     | 1.1            |
            //| Visual Studio 2005        | Whidbey       | 8.0.*     | 2.0, 3.0       |
            //| Visual Studio 2008        | Orcas         | 9.0.*     | 2.0, 3.0, 3.5  |
            //| Visual Studio 2010        | Dev10/Rosario | 10.0.*    | 2.0 – 4.0      |
            //| Visual Studio 2012        | Dev11         | 11.0.*    | 2.0 – 4.5.2    |
            //| Visual Studio 2013        | Dev12         | 12.0.*    | 2.0 – 4.5.2    |
            //| Visual Studio 2015        | Dev14         | 14.0.*    | 2.0 – 4.6      |
            //+---------------------------+---------------+-----------+----------------+

            //Only process the earliest Visual Studio's, as the later versions should be picked up by the product version
            //Note that this may not be entirely accurate - for example, VS2008 could ignore a .NET 3 version, but these should be pretty rare - even if it misidentifies .NET framework 1/2/3 - the story is the same - these are wildly obsolete and need to be resolved.
            if (productVersion.StartsWith("7.0") == true)
            {
                return "v1.0";
            }
            else if (productVersion.StartsWith("7.1") == true)
            {
                return "v1.1";
            }
            else if (productVersion.StartsWith("8.0") == true)
            {
                return "v2.0";
            }
            else
            {
                return "";
            }
        }

        //private static string GetUnityFrameworkVersion(string line)
        //{
        //    //An example of what to expect:
        //    //m_EditorVersion: 2020.3.12f1
        //    //m_EditorVersionWithRevision: 2020.3.12f1(b3b2c6512326)
        //    string fullVersion = line.Replace("m_EditorVersion:", "").Trim();
        //    string[] splitVersion = fullVersion.Split('.');
        //    string unityVersion = "";
        //    if (splitVersion.Length >= 2)
        //    {
        //        unityVersion = "Unity3d v" + splitVersion[0] + "." + splitVersion[1];
        //    }

        //    return unityVersion;
        //}

        // get a color to represent the support. Kinda rough for now, but highlights really old versions.
        public static string? GetStatus(string? framework)
        {
            if (framework == null)
            {
                //Unknown/gray
                return "unknown";
            }
            else if (framework == "vb6" ||
                framework.Contains("v1") ||
                framework.Contains("v2") ||
                framework.Contains("v3.0") ||
                framework.Contains("v4.0") ||
                framework.Contains("v4.1") ||
                framework.Contains("v4.2") ||
                framework.Contains("v4.3") ||
                framework.Contains("v4.4") ||
                framework.Contains("v4.5") ||
                framework == "net45" || //Unclear if this should be net 45 or v4.5 - I've seen both in wild
                framework == "net46" ||
                framework == "net461" ||
                framework.Contains("netcoreapp1") ||
                framework.Contains("netcoreapp2") ||
                framework == "netcoreapp3.0" ||
                framework == "net5.0")
            {
                //Unsupported/End of life/red
                return "deprecated";
            }
            else if (framework == "netcoreapp3.1")
            {
                //Supported, but old/orange
                return "EOL: 13-Dec-2022";
            }
            else if (framework.Contains("v3.5"))
            {
                //Supported, but old/orange
                return "EOL: 9-Jan-2029";
            }
            else if (framework.Contains("net6.0") ||
                framework.Contains("netstandard") ||
                framework == "net462" ||
                framework == "v4.6.2" ||
                framework.Contains("net47") ||
                framework.Contains("v4.7") ||
                framework.Contains("net48") ||
                framework.Contains("v4.8"))
            {
                //Supported/Ok/blue
                return "supported";
            }
            else if (framework.Contains("net7.0"))
            {
                return "in preview";
            }
            else
            {
                //Unknown/gray
                return "unknown";
            }
        }

        private static string GetLanguage(string directory)
        {
            //Check to see if it's a VB.NET or C# project
            int csFiles = new DirectoryInfo(directory).GetFiles("*.cs", SearchOption.AllDirectories).Length;
            int vbFiles = new DirectoryInfo(directory).GetFiles("*.vb", SearchOption.AllDirectories).Length;
            string language;
            if (csFiles >= vbFiles)
            {
                language = "csharp";
            }
            else
            {
                language = "vb.net";
            }
            return language;
        }

    }
}
