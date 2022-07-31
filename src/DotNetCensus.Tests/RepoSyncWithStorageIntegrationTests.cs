//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;
//using System.Threading.Tasks;
//using TechDebtID.Core;

//namespace DotNetCensus.Tests;
//{
//    [TestClass]
//    public class RepoSyncWithStorageIntegrationTests
//    {
//        //[TestMethod]
//        //public async Task GetGitHubRepoIntegrationTest()
//        //{
//        //    //Arrange
//        //    GitHub gh = new GitHub();
//        //    string organization = "samsmithnz";

//        //    //Act
//        //    List<string> results = await gh.GetGitHubRepos(organization);

//        //    //Asset
//        //    Assert.IsTrue(results != null);
//        //    Assert.IsTrue(results.Count > 0);
//        //    Assert.IsTrue(results[0] == "samsmithnz/AppSettingsYamlTest");
//        //}


//        [TestMethod]
//        public async Task GetGitHubRepoFilesIntegrationTest()
//        {
//            //Arrange
//            GitHub gh = new();
//            string organization = "samsmithnz";
//            string repo = "AppSettingsYamlTest";
//            string defaultBranch = "master";

//            //Act
//            List<string> results = await gh.GetGitHubRepoFiles(organization, repo, defaultBranch);

//            //Asset
//            Assert.IsTrue(results != null);
//            Assert.AreEqual(10, results.Count);
//            Assert.IsTrue(results[0] == ".github");
//            Assert.IsTrue(results[^1] == "AppSettingsYamlTest/AppSettingsYamlTest.sln");
//        }

//        [TestMethod]
//        public void DownloadRepoToStorageIntegrationTest()
//        {
//            //Arrange
//            //IConfigurationBuilder config = new ConfigurationBuilder()
//            //   .SetBasePath(AppContext.BaseDirectory)
//            //   .AddJsonFile("appsettings.json")
//            //   .AddUserSecrets<RepoSyncWithStorageIntegrationTests>();
//            //IConfigurationRoot Configuration = config.Build();
//            //string azureStorageConnectionString = Configuration["AzureStorageConnectionString"];
//            RepoSyncWithStorage repoSync = new();
//            string repo = "samsmithnz/SamsFeatureFlags";
//            string destination = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
//                .Replace("\\TechDebtID.Tests\\bin\\Debug\\net6.0", "")
//                .Replace("\\TechDebtID.Tests\\bin\\Release\\net6.0", "") + "\\GitHubTempLocation";

//            //Act
//            repoSync.CloneRepoToAzureStorage(repo, destination);

//            //Asset
//            DirectoryInfo dir = new(destination);
//            Assert.IsTrue(dir.GetFiles().Length > 0);
//        }

//        //[TestMethod]
//        //public async Task UploadFilesToStorageIntegrationTest()
//        //{
//        //    //Arrange
//        //    IConfigurationBuilder config = new ConfigurationBuilder()
//        //       .SetBasePath(AppContext.BaseDirectory)
//        //       .AddJsonFile("appsettings.json")
//        //       .AddUserSecrets<RepoSyncWithStorageIntegrationTests>();
//        //    IConfigurationRoot Configuration = config.Build();
//        //    string azureStorageConnectionString = Configuration["AzureStorageConnectionString"];
//        //    RepoSyncWithStorage repoSync = new RepoSyncWithStorage();
//        //    string repo = "samsmithnz/SamsFeatureFlags";
//        //    string destination = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
//        //        .Replace("\\TechDebtID.Tests\\bin\\Debug\\net6.0", "")
//        //        .Replace("\\TechDebtID.Tests\\bin\\Release\\net6.0", "") + "\\GitHubTempLocation\\";
//        //    //int index = destination.IndexOf("GitHubTempLocation") + "GitHubTempLocation".Length;
//        //    //string rootFolder = destination.Substring(index); 

//        //    //Act
//        //    await repoSync.UploadFilesToStorageBlobs(azureStorageConnectionString, repo, destination);

//        //    //Asset
//        //    DirectoryInfo dir = new DirectoryInfo(destination);
//        //    Assert.IsTrue(dir.GetFiles().Length > 0);
//        //}


//    }
//}
