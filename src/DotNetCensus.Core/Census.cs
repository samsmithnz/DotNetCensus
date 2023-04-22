using DotNetCensus.Core.Models;
using DotNetCensus.Core.Projects;

namespace DotNetCensus.Core
{
    public static class Census
    {
        public static List<FrameworkSummary> AggregateFrameworks(List<Project> projects, bool includeTotal)
        {
            int total = 0;
            List<FrameworkSummary> frameworkSummary = new();
            foreach (Project project in projects)
            {
                project.Family = ProjectClassification.GetFrameworkFamily(project.FrameworkCode);
                if (string.IsNullOrEmpty(project.FrameworkCode))
                {
                    project.FrameworkCode = "(Unknown framework)";
                }

                //Process each indvidual framework
                FrameworkSummary? framework = frameworkSummary.Find(i => i.Framework == project.FrameworkName);

                //If this framework isn't in the current list, create a new one
                if (framework == null)
                {
                    frameworkSummary.Add(new FrameworkSummary
                    {
                        Framework = project.FrameworkName,
                        FrameworkFamily = project.Family,
                        Status = ProjectClassification.GetStatus(project.FrameworkCode),
                        Count = 1 //it's the first time, start with a count of 1
                    });
                }
                else
                {
                    //There is an existing entry, increment the count
                    framework.Count++;
                }
                total++;
            }

            //Sort the result
            List<FrameworkSummary> sortedFrameworks = frameworkSummary.OrderBy(o => o.Framework).ToList();

            //Add a total line if we need one
            if (includeTotal)
            {
                sortedFrameworks.Add(new FrameworkSummary { Framework = "total frameworks", Count = total });
            }
            return sortedFrameworks;
        }

        public static List<LanguageSummary> AggregateLanguages(List<Project> projects, bool includeTotal)
        {
            int total = 0;
            List<LanguageSummary> languageSummary = new();
            foreach (Project project in projects)
            {
                if (string.IsNullOrEmpty(project.Language))
                {
                    project.Language = "(Unknown language)";
                }

                //Process each indvidual language
                LanguageSummary? language = languageSummary.Find(i => i.Language == project.Language);

                //If this language isn't in the current list, create a new one
                if (language == null)
                {
                    languageSummary.Add(new LanguageSummary
                    {
                        Language = project.Language,
                        Count = 1 //it's the first time, start with a count of 1
                    });
                }
                else
                {
                    //There is an existing entry, increment the count
                    language.Count++;
                }
                total++;
            }

            //Sort the result
            List<LanguageSummary> sortedLanguages = languageSummary.OrderBy(o => o.Language).ToList();

            //Add a total line if we need one
            if (includeTotal == true)
            {
                sortedLanguages.Add(new LanguageSummary { Language = "total languages", Count = total });
            }
            return sortedLanguages;
        }
    }
}
