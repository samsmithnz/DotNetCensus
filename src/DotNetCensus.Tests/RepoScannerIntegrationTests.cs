//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.IO;
//using System.Reflection;
//using System.Threading;
//using TechDebtID.Core;
//using TechDebtID.Core.Models;

//namespace DotNetCensus.Tests;
//{
//    [TestClass]
//    public class RepoScannerIntegrationTests
//    {

//        [TestMethod]
//        public void RepoScannerSamplesIntegrationTest()
//        {
//            //Arrange
//            RepoScanner repoScanner = new RepoScanner();
//            IProgress<ProgressMessage> progress = new Progress<ProgressMessage>(ReportProgress);
//            CancellationTokenSource tokenSource = new CancellationTokenSource();
//            bool includeTotal = true;
//            //Sometimes we have a Debug build, sometimes Release, handle both to find the samples folder
//            string rootFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
//                .Replace("\\TechDebtID.Tests\\bin\\Debug\\net6.0", "")
//                .Replace("\\TechDebtID.Tests\\bin\\Release\\net6.0", "") + "\\Samples";

//            //Act
//            ScanSummary results = repoScanner.ScanRepo(progress, tokenSource.Token, rootFolder, includeTotal, "results.csv");
//            progress.Report(new ProgressMessage { ProjectsProcessed = 1, RootProjectsProcessed = 1 });

//            //Asset
//            Assert.AreEqual(9, results.ProjectCount);
//           // Assert.AreEqual(1, this.ProjectsProcessed);
//            Assert.AreEqual(1, this.RootProjectsProcessed);

//            //Framework tests
//            Assert.AreEqual(8, results.FrameworkSummary.Count);
//            Assert.AreEqual("net462", results.FrameworkSummary[0].Framework);
//            Assert.AreEqual(1, results.FrameworkSummary[0].Count);
//            Assert.AreEqual("net5.0", results.FrameworkSummary[1].Framework);
//            Assert.AreEqual(1, results.FrameworkSummary[1].Count);
//            Assert.AreEqual("netcoreapp3.1", results.FrameworkSummary[2].Framework);
//            Assert.AreEqual(3, results.FrameworkSummary[2].Count);
//            Assert.AreEqual("netstandard2.0", results.FrameworkSummary[3].Framework);
//            Assert.AreEqual(1, results.FrameworkSummary[3].Count);
//            Assert.AreEqual("v1.0", results.FrameworkSummary[4].Framework);
//            Assert.AreEqual(1, results.FrameworkSummary[4].Count);
//            Assert.AreEqual("v4.7.2", results.FrameworkSummary[5].Framework);
//            Assert.AreEqual(1, results.FrameworkSummary[5].Count);
//            Assert.AreEqual("vb6", results.FrameworkSummary[6].Framework);
//            Assert.AreEqual(1, results.FrameworkSummary[6].Count);
//            Assert.AreEqual("total frameworks", results.FrameworkSummary[7].Framework);
//            Assert.AreEqual(9, results.FrameworkSummary[7].Count);

//            //Language tests
//            Assert.AreEqual(4, results.LanguageSummary.Count);
//            Assert.AreEqual("csharp", results.LanguageSummary[0].Language);
//            Assert.AreEqual(6, results.LanguageSummary[0].Count);
//            Assert.AreEqual("vb.net", results.LanguageSummary[1].Language);
//            Assert.AreEqual(2, results.LanguageSummary[1].Count);
//            Assert.AreEqual("vb6", results.LanguageSummary[2].Language);
//            Assert.AreEqual(1, results.LanguageSummary[2].Count);
//            Assert.AreEqual("total languages:", results.LanguageSummary[3].Language);
//            Assert.AreEqual(9, results.LanguageSummary[3].Count);
//            string csv = null;
//            using (var sr = new StreamReader("results.csv"))
//            {
//                csv = sr.ReadToEnd();
//            }
//            Assert.IsNotNull(csv);
//            Assert.IsTrue(csv.Length > 0);
//            //TODO: Add checks to confirm contents are as expected
//        }

//        private int ProjectsProcessed { get; set; }
//        private int RootProjectsProcessed { get; set; }
//        private void ReportProgress(ProgressMessage message)
//        {
//            this.ProjectsProcessed = message.ProjectsProcessed;
//            this.RootProjectsProcessed = message.RootProjectsProcessed;
//        }
//    }
//}
