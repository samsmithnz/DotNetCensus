﻿using DotNetCensus.Core.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace DotNetCensus.Core.Projects
{
    public static class ProjectFileProcessing
    {
        public static List<Project> SearchProjectFile(FileInfo fileInfo, string filePath,
            string? content,
            string? propFileContent = null)
        {
            string fileName = fileInfo.Name;
            if (content == null)
            {
                //This is a directory search - not a repo search and we need to read in the contents of the file
                content = File.ReadAllText(filePath);
            }
            List<Project> projects = new();
            switch (fileInfo.Extension.ToLower())
            {
                case ".csproj":
                case ".sqlproj":
                    projects.AddRange(ProcessProjectFile(fileName, filePath, "csharp", content, propFileContent));
                    break;
                case ".vbproj":
                    projects.AddRange(ProcessProjectFile(fileName, filePath, "vb.net", content, propFileContent));
                    break;
                case ".fsproj":
                    projects.AddRange(ProcessProjectFile(fileName, filePath, "fsharp", content, propFileContent));
                    break;
                case ".vbp":
                    projects.AddRange(ProcessProjectFile(fileName, filePath, "vb6", content));
                    break;
            }
            return projects;
        }

        public static List<Project> SearchSecondaryProjects(FileInfo fileInfo, string filePath, string? content)
        {
            string fileName = fileInfo.Name;
            List<Project> projects = new();

            //is it a .NET Core 1.0 or 1.1 project? These didn't use the project file format...
            if (fileInfo != null && fileInfo.Directory != null &&
                fileInfo.Name == "project.json")
            {
                //Check to see if it's a VB.NET or C# project
                string language = ProjectClassification.GetLanguage(fileInfo.Directory.FullName);
                if (content == null)
                {
                    //This is a directory search - not a repo search and we need to read in the contents of the file
                    content = File.ReadAllText(filePath);
                }
                projects.AddRange(ProcessProjectFile(fileName, filePath, language, content));
            }
            //is it a .NET Framework 2.0 or 3.5 web site - which has no project file
            else if (fileInfo != null && fileInfo.Directory != null &&
                fileInfo.Name == "web.config")
            {
                //Check to see if it's a VB.NET or C# project
                string language = ProjectClassification.GetLanguage(fileInfo.Directory.FullName);
                if (content == null)
                {
                    //This is a directory search - not a repo search and we need to read in the contents of the file
                    content = File.ReadAllText(filePath);
                }
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
        public static List<Project> ProcessProjectFile(string fileName, string filePath,
            string language, string content,
            string? propFileContent = null)
        {
            string[] lines = content.Split('\n');

            List<Project> projects = new();

            //Setup the project object
            Project? project = new()
            {
                FileName = fileName, 
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
                try
                {
                    JsonElement jsonObject = JsonSerializer.Deserialize<JsonElement>(content);
                    //Search for the project version
                    if (jsonObject.TryGetProperty("frameworks", out JsonElement jsonElement))
                    {
                        foreach (JsonProperty item in jsonElement.EnumerateObject())
                        {
                            if (item.NameEquals("netcoreapp1.0"))
                            {
                                project.FrameworkCode = "netcoreapp1.0";
                                break;
                            }
                            else if (item.NameEquals("netcoreapp1.1"))
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
                catch
                {   
                    //sometimes the json is invalid - just skip over the issue
                    project = null;
                }

            }
            else if (new FileInfo(filePath).Name == "web.config")
            {
                foreach (string line in lines)
                {
                    if (line.Contains("<add assembly=\"System.Core, Version="))
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
                    if (line.Contains("<TargetFrameworkVersion>"))
                    {
                        project.FrameworkCode = CheckFrameworkCodeForVariable(line.Replace("<TargetFrameworkVersion>", "").Replace("</TargetFrameworkVersion>", "").Trim(), propFileContent);
                        break;
                    }
                    //.NET Core version element
                    else if (line.Contains("<TargetFramework>"))
                    {
                        project.FrameworkCode = CheckFrameworkCodeForVariable(line.Replace("<TargetFramework>", "").Replace("</TargetFramework>", "").Trim(), propFileContent);
                        break;
                    }
                    //Multiple .NET flavors element
                    else if (line.Contains("<TargetFrameworks>"))
                    {
                        string frameworks = CheckFrameworkCodeForVariable(line.Replace("<TargetFrameworks>", "").Replace("</TargetFrameworks>", "").Trim(), propFileContent);
                        string[] frameworkList = frameworks.Split(';');
                        for (int i = 0; i <= frameworkList.Length - 1; i++)
                        {
                            if (i == 0)
                            {
                                project.FrameworkCode = CheckFrameworkCodeForVariable(frameworkList[i], propFileContent);
                            }
                            else
                            {
                                Project additionalProject = new()
                                {
                                    FileName = new FileInfo(filePath).Name,
                                    Path = filePath,
                                    Language = language,
                                    FrameworkCode = CheckFrameworkCodeForVariable(frameworkList[i], propFileContent)
                                };
                                projects.Add(additionalProject);
                            }
                        }
                        break;
                    }
                    //Visual Studio version (for old .NET Framework versions that were tied directly to Visual Studio versions) 
                    else if (line.Contains("<ProductVersion>") ||
                             line.Contains("ProductVersion = "))
                    {
                        project.FrameworkCode = ProjectClassification.GetHistoricalFrameworkVersion(line);
                        //Note: Since product version could appear first in the lines list, and we could still find a target version, don't break out of the loop
                    }
                    ////Unity 3d project files
                    //else if (line.Contains("m_EditorVersion:"))
                    //{
                    //    project.Framework = GetUnityFrameworkVersion(line);
                    //    break;
                    //}
                }

                //If we didn't find targetframework in the project file, check any .props files
                if (project.FrameworkCode == "" &&
                    propFileContent != null)
                {
                    lines = propFileContent.Split("\n");
                    foreach (string line in lines)
                    {
                        //.NET Framework version element
                        if (line.Contains("<TargetFrameworkVersion>"))
                        {
                            project.FrameworkCode = CheckFrameworkCodeForVariable(line.Replace("<TargetFrameworkVersion>", "").Replace("</TargetFrameworkVersion>", "").Trim(), propFileContent);
                            break;
                        }
                        //.NET Core version element
                        else if (line.Contains("<TargetFramework>"))
                        {
                            project.FrameworkCode = CheckFrameworkCodeForVariable(line.Replace("<TargetFramework>", "").Replace("</TargetFramework>", "").Trim(), propFileContent);
                            break;
                        }
                        //Multiple .NET flavors element
                        else if (line.Contains("<TargetFrameworks>"))
                        {
                            string frameworks = CheckFrameworkCodeForVariable(line.Replace("<TargetFrameworks>", "").Replace("</TargetFrameworks>", "").Trim(), propFileContent);
                            string[] frameworkList = frameworks.Split(';');
                            for (int i = 0; i < frameworkList.Length - 1; i++)
                            {
                                if (i == 0)
                                {
                                    project.FrameworkCode = CheckFrameworkCodeForVariable(frameworkList[i], propFileContent);
                                }
                                else
                                {
                                    Project additionalProject = new()
                                    {
                                        FileName = new FileInfo(filePath).Name,
                                        Path = filePath,
                                        Language = language,
                                        FrameworkCode = CheckFrameworkCodeForVariable(frameworkList[i], propFileContent)
                                    };
                                    projects.Add(additionalProject);
                                }
                            }
                            break;
                        }
                        //Visual Studio version (for old .NET Framework versions that were tied directly to Visual Studio versions) 
                        else if (line.Contains("<ProductVersion>") ||
                                 line.Contains("ProductVersion = "))
                        {
                            project.FrameworkCode = ProjectClassification.GetHistoricalFrameworkVersion(line);
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
            }

            if (project != null)
            {
                projects.Add(project);
            }

            //Add colors and families
            foreach (Project item in projects)
            {
                //Debug.WriteLine("item.FrameworkCode:" + item.FrameworkCode);
                item.Status = ProjectClassification.GetStatus(item.FrameworkCode);
                item.Family = ProjectClassification.GetFrameworkFamily(item.FrameworkCode);
                item.FrameworkName = ProjectClassification.GetFriendlyName(item.FrameworkCode, item.Family);
                //Debug.WriteLine($"Framework: {item.FrameworkCode}; Name: {item.FrameworkName}; Project: {item.FileName}");
            }

            return projects;
        }

        //Check to see if the framework 
        private static string CheckFrameworkCodeForVariable(string variable, string? propFileContent)
        {
            StringBuilder variableResult = new();
            if (variable.Contains("$(") && variable.Contains(')'))
            {
                string[] variables = variable.Split(';');
                int i = 0;
                foreach (string variableItem in variables)
                {
                    if (variableItem.Trim().Length > 0 && variableItem.Contains("$(") && variableItem.Contains(')'))
                    {
                        string prefix = "";
                        string suffix = "";
                        //Open any .props files and look for the variables
                        int pFrom = variableItem.IndexOf("$(") + 2; // ("$(").Length;
                        int pTo = variableItem.LastIndexOf(")");
                        string searchVariable = variableItem[pFrom..pTo];
                        //Capture the suffix and prefix if the variable is with regular text, for example "net$(variable)"
                        if (pFrom >= 2)
                        {
                            prefix = variableItem[..(pFrom - 2)];
                        }
                        suffix = variableItem[(pTo + 1)..];
                        if (propFileContent != null)
                        {
                            string processedVariable = "";
                            string[] lines = propFileContent.Split("\n");
                            foreach (string line in lines)
                            {
                                if (line.Contains("<" + searchVariable + ">"))
                                {
                                    processedVariable = line.Replace("<" + searchVariable + ">", "")
                                                 .Replace("</" + searchVariable + ">", "")
                                                 .Trim();
                                    break;
                                }
                            }
                            variableResult.Append(prefix);
                            variableResult.Append(processedVariable);
                            variableResult.Append(suffix);
                            if (i < variables.Length - 1)
                            {
                                variableResult.Append(';');
                            }
                        }
                    }
                    else
                    {
                        variableResult.Append(variableItem);
                        variableResult.Append(';');
                    }
                    i++;
                }
                //If it's a variable within a variable, process it again
                if (variableResult.ToString().Contains("$(") &&
                    variableResult.ToString().Contains(')'))
                {
                    string variableResultTmp = variableResult.ToString();
                    variableResult = new();
                    variableResult.Append(CheckFrameworkCodeForVariable(variableResultTmp, propFileContent));
                }
            }
            else
            {
                variableResult.Append(variable);
            }
            return variableResult.ToString();
        }

    }
}
