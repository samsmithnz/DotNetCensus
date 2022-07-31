﻿using DotNetCensus.Core.Models;

namespace DotNetCensus.Core
{
    public static class Aggregations
    {
        public static List<FrameworkSummary> AggregateFrameworks(List<Project> projects, bool includeTotal)
        {
            int total = 0;
            List<FrameworkSummary> frameworkSummary = new();
            foreach (Project project in projects)
            {
                project.Family = DotNetProjectScanning.GetFrameworkFamily(project.Framework);
                if (project.Framework == null)
                {
                    project.Framework = "(Unknown framework)";
                }

                //Process each indvidual framework
                FrameworkSummary? framework = frameworkSummary.Find(i => i.Framework == project.Framework);

                //If this framework isn't in the current list, create a new one
                if (framework == null)
                {
                    frameworkSummary.Add(new FrameworkSummary
                    {
                        Framework = project.Framework,
                        FrameworkFamily = project.Family,
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
            List<FrameworkSummary> sortedFrameworks = frameworkSummary.OrderBy(o => o.Framework).ToList();
            if (includeTotal == true)
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
                if (project.Language == null)
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
            List<LanguageSummary> sortedLanguages = languageSummary.OrderBy(o => o.Language).ToList();
            if (includeTotal == true)
            {
                sortedLanguages.Add(new LanguageSummary { Language = "total languages:", Count = total });
            }
            return sortedLanguages;
        }
    }
}