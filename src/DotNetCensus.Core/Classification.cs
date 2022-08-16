namespace DotNetCensus.Core
{
    public static class Classification
    {
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

        public static string GetHistoricalFrameworkVersion(string line)
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

        //public static string GetUnityFrameworkVersion(string line)
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

        public static string GetLanguage(string directory)
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
