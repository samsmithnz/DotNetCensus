using DotNetCensus.Core.Models;
using DotNetCensus.Core.Models.GitHub;
using DotNetCensus.Core.Projects;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace DotNetCensus.Core.APIs
{
    public static class GitHubAPI
    {
        public async static Task<List<Project>> GetRepoFiles(string? clientId, string? clientSecret,
            string owner, string repo, string branch = "main")
        {
            List<Project> results = new();
            TreeResponse treeResponse = new();

            //https://docs.github.com/en/rest/git/trees#get-a-tree
            string url = $"https://api.github.com/repos/{owner}/{repo}/git/trees/{branch}?recursive=true";
            string? response = await GetGitHubMessage(clientId, clientSecret, url, true);
            if (string.IsNullOrEmpty(response) == false)
            {
                dynamic? jsonObj = JsonConvert.DeserializeObject(response);
                treeResponse = JsonConvert.DeserializeObject<TreeResponse>(jsonObj?.ToString());
            }
            if (treeResponse != null && treeResponse.tree.Length > 0)
            {
                foreach (FileResponse item in treeResponse.tree)
                {
                    if (item != null && item.path != null)
                    {
                        FileInfo fileInfo = new(item.path);
                        string path = item.path;
                        if (string.IsNullOrEmpty(path) == false)
                        {
                            //Danger: What if the file name is in the path? It will be replaced. 
                            path = path.Replace("/" + fileInfo.Name, "/");
                        }
                        if (ProjectClassification.IsProjectFile(fileInfo.Name) == true ||
                            ProjectClassification.IsProjectFile(fileInfo.Name, false) == true ||
                            fileInfo.Name.ToLower() == "directory.build.props")
                        {
                            results.Add(new Project()
                            {
                                Path = path,
                                FileName = fileInfo.Name
                            });
                        }
                    }
                }
            }

            //Sort the results to make directory processing easier
            results = results.OrderBy(o => o.Path).ToList();

            return results;
        }

        public async static Task<FileDetails?> GetRepoFileContents(string? clientId, string? clientSecret,
            string owner, string repo, string path)
        {
            FileDetails? result = null;
            path = HttpUtility.UrlEncode(path);
            string url = $"https://api.github.com/repos/{owner}/{repo}/contents/{path}";
            System.Diagnostics.Debug.WriteLine(url);
            string? response = await GetGitHubMessage(clientId, clientSecret, url, true);
            if (string.IsNullOrEmpty(response) == false && response.Contains(@"""message"":""Not Found""") == false)
            {
                dynamic? jsonObj = JsonConvert.DeserializeObject(response);
                result = JsonConvert.DeserializeObject<FileDetails>(jsonObj?.ToString());

                //Decode the Base64 file contents result
                if (result != null && result.content != null)
                {
                    byte[]? valueBytes = System.Convert.FromBase64String(result.content);
                    result.content = Encoding.UTF8.GetString(valueBytes);
                }
            }
            return result;
        }

        public async static Task<List<string>?> GetGitHubOrganizationRepos(string? clientId, string? clientSecret, string organization)
        {
            List<string> results = new();
            List<RepoResponse>? repos = null;

            //https://docs.github.com/en/rest/repos/repos#list-organization-repositories
            string url = $"https://api.github.com/orgs/{organization}/repos";
            string? response = await GetGitHubMessage(clientId, clientSecret, url, true);
            if (string.IsNullOrEmpty(response) == false)
            {
                dynamic? jsonObj = JsonConvert.DeserializeObject(response);
                repos = JsonConvert.DeserializeObject<List<RepoResponse>>(jsonObj?.ToString());
            }
            if (repos != null && repos.Count > 0)
            {
                foreach (RepoResponse item in repos)
                {
                    if (item != null &&
                        item.archived == false &&
                        item.disabled == false &&
                        item.name != null)
                    {
                        results.Add(item.name);
                    }
                }
            }
            return results;
        }

        public async static Task<int?> GetRateLimit(string? clientId, string? clientSecret)
        {
            int result = 0;
            RateLimit? rateLimit = null;

            //https://docs.github.com/en/rest/rate-limit
            string url = $"https://api.github.com/rate_limit";
            string? response = await GetGitHubMessage(clientId, clientSecret, url, true);
            if (string.IsNullOrEmpty(response) == false)
            {
                dynamic? jsonObj = JsonConvert.DeserializeObject(response);
                rateLimit = JsonConvert.DeserializeObject<RateLimit>(jsonObj?.ToString());
            }
            if (rateLimit != null && rateLimit.resources != null && rateLimit.resources.core != null)
            {
                result = rateLimit.resources.core.remaining;
            }
            return result;
        }

        private async static Task<string?> GetGitHubMessage(string? clientId, string? clientSecret, string url, bool processErrors = true)
        {
            HttpClient client = BuildHttpClient(clientId, clientSecret, url);
            HttpResponseMessage response = await client.GetAsync(url);
            //A debugging function
            if (processErrors == true)
            {
                response.EnsureSuccessStatusCode();
            }
            return await response.Content.ReadAsStringAsync();
        }

        private static HttpClient BuildHttpClient(string? clientId, string? clientSecret, string url)
        {
            //Console.WriteLine($"Running GitHub url: {url}");
            if (!url.Contains("api.github.com"))
            {
                throw new Exception("api.github.com missing from URL");
            }
            HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("SamsRepoAutomation", "0.1"));
            //If we use a id/secret, we significantly increase the rate from 60 requests an hour to 5000. https://developer.github.com/v3/#rate-limiting
            if (string.IsNullOrEmpty(clientId) == false && string.IsNullOrEmpty(clientSecret) == false)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", clientId, clientSecret))));
            }
            return client;
        }
    }
}
