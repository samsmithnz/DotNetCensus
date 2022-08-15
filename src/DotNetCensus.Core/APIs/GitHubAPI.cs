using DotNetCensus.Core.Models;
using DotNetCensus.Core.Models.GitHub;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace DotNetCensus.Core.APIs
{
    public static class GitHubAPI
    {
        public async static Task<List<Project>> GetRepoContents(string? clientId, string? clientSecret,
            string owner, string repo, string branch = "main")
        {
            List<Project> results = new();
            TreeResponse tree = new();
            //https://docs.github.com/en/rest/git/trees#get-a-tree
            string url = $"/repos/{owner}/{repo}/git/trees/{branch}";
            string? response = await GetGitHubMessage(clientId, clientSecret, url, false);
            if (string.IsNullOrEmpty(response) == false)
            {
                dynamic? jsonObj = JsonConvert.DeserializeObject(response);
                tree = JsonConvert.DeserializeObject<List<TreeResponse>>(jsonObj?.ToString());
            }
            if (tree != null && tree.tree.Length > 0)
            {
                foreach (FileResponse item in tree.tree)
                {
                    Project project = new()
                    {
                        Path = item.path
                    };
                    results.Add(project);
                }
            }


            return results;
        }


        public async static Task<string?> GetGitHubMessage(string url, string clientId, string clientSecret, bool processErrors = true)
        {
            HttpClient client = BuildHttpClient(url, clientId, clientSecret);
            HttpResponseMessage response = await client.GetAsync(url);
            if (processErrors)
            {
                response.EnsureSuccessStatusCode();
            }
            return await response.Content.ReadAsStringAsync();
        }

        private static HttpClient BuildHttpClient(string url, string clientId, string clientSecret)
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
