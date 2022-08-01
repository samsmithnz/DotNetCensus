using DotNetCensus.Core.Models;

namespace DotNetCensus.Core
{
    public static class ProjectScanning
    {

        //Search directory for project files
        public static List<Project> SearchDirectory(string dir)
        {
            List<Project> projects = new();
            foreach (FileInfo fileInfo in new DirectoryInfo(dir).GetFiles("*.*", SearchOption.AllDirectories))
            {
                //if .NET project files are found, process them
                switch (fileInfo.Extension.ToLower())
                {
                    case ".csproj":
                    case ".sqlproj":
                        projects.AddRange(ProcessProjectFile(fileInfo.FullName, "csharp"));
                        break;
                    case ".vbproj":
                        projects.AddRange(ProcessProjectFile(fileInfo.FullName, "vb.net"));
                        break;
                    case ".vbp":
                        projects.AddRange(ProcessProjectFile(fileInfo.FullName, "vb6"));
                        break;
                        //default:
                        //    //Is it a Unity3d project?
                        //    if (fileInfo.Name == "ProjectVersion.txt")
                        //    {
                        //        projects.AddRange(ProcessDotNetProjectFile(fileInfo.FullName, "csharp"));
                        //    }
                        //    break;
                }
            }

            return projects;
        }

        //Process individual project files
        private static List<Project> ProcessProjectFile(string filePath, string language)
        {
            string[] lines = File.ReadAllLines(filePath);

            List<Project> projects = new();

            //Setup the project object
            Project project = new()
            {
                FileName = new FileInfo(filePath).Name,
                Path = filePath,
                Language = language
            };

            if (language == "vb6")
            {
                project.FrameworkCode = "vb6";
            }
            else
            {
                //scan the project file to identify the framework
                foreach (string line in lines)
                {
                    //.NET Framework version element
                    if (line.IndexOf("<TargetFrameworkVersion>") > 0)
                    {
                        project.FrameworkCode = line.Replace("<TargetFrameworkVersion>", "").Replace("</TargetFrameworkVersion>", "").Trim();
                        break;
                    }
                    //.NET Core version element
                    else if (line.IndexOf("<TargetFramework>") > 0)
                    {
                        project.FrameworkCode = line.Replace("<TargetFramework>", "").Replace("</TargetFramework>", "").Trim();
                        break;
                    }
                    //Multiple .NET flavors element
                    else if (line.IndexOf("<TargetFrameworks>") > 0)
                    {
                        string frameworks = line.Replace("<TargetFrameworks>", "").Replace("</TargetFrameworks>", "").Trim();
                        string[] frameworkList = frameworks.Split(';');
                        for (int i = 0; i < frameworkList.Length - 1; i++)
                        {
                            if (i == 0)
                            {
                                project.FrameworkCode = frameworkList[i];
                            }
                            else
                            {
                                Project additionalProject = new()
                                {
                                    FileName = new FileInfo(filePath).Name,
                                    Path = filePath,
                                    Language = language,
                                    FrameworkCode = frameworkList[i]
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

            projects.Add(project);

            //Add colors and families
            foreach (Project item in projects)
            {
                item.Color = GetColor(item.FrameworkCode);
                item.Family = GetFrameworkFamily(item.FrameworkCode);
                item.FrameworkName = GetFriendlyName(item.FrameworkCode, item.Family);
            }

            return projects;
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
                return family + " " + frameworkCode.Replace("v", "");
            }
            else if (frameworkCode.StartsWith("net4"))
            {
                string number = frameworkCode.Replace("net", "");
                string formattedNumber = "";
                //Add .'s between each number. Gross.
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
        public static string? GetColor(string? framework)
        {
            if (framework == null)
            {
                //Unknown/gray
                return "gray";
            }
            else if (framework.Contains(".NET Framework v1") ||
                framework.Contains(".NET Framework v2") ||
                framework.Contains(".NET Framework v3.0") ||
                framework.Contains(".NET Framework v4.0") ||
                framework.Contains(".NET Framework v4.1") ||
                framework.Contains(".NET Framework v4.2") ||
                framework.Contains(".NET Framework v4.3") ||
                framework.Contains(".NET Framework v4.4") ||
                framework.Contains(".NET Framework v4.5") ||
                framework.Contains(".NET Framework v4.6.0") ||
                framework.Contains(".NET Framework v4.6.1") ||
                framework.Contains("net5.0"))
            {
                //Unsupported/End of life/red
                return "red";
            }
            else if (framework.Contains(".NET Framework") ||
                framework.Contains("netcoreapp"))
            {
                //Supported, but old/orange
                return "orange";
            }
            else if (framework.Contains("net6.0") ||
                framework.Contains("net7.0") ||
                framework.Contains("netstandard") ||
                framework.Contains("Unity3d v2020") ||
                framework.Contains(".NET Framework v3.5.0") ||
                framework.Contains(".NET Framework v4.6.2") ||
                framework.Contains(".NET Framework v4.7") ||
                framework.Contains(".NET Framework v4.8"))
            {
                //Supported/Ok/blue
                return "blue";
            }
            else
            {
                //Unknown/gray
                return "gray";
            }
        }
    }
}
