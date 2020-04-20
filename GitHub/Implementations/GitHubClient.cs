using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GitHub.Interfaces;
using GitHub.Models;

namespace GitHub.Implementations
{
    public class GitHubClient : IGitHubClient
    {
        public async Task<IEnumerable<UserRepository>> GetRepositories(string userName, string token)
        {
            var api = $"https://api.github.com/users/{userName}/repos";
            var response = await GetRepos(token, api);
            if ((int) response.StatusCode != 200)
            {
                throw new Exception(((int) response.StatusCode).ToString());
            }

            var repositories = await Deserialize(response);
            return repositories;
        }

        private static async Task<IEnumerable<UserRepository>> Deserialize(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var deserialized =
                await JsonSerializer.DeserializeAsync<IEnumerable<UserRepository>>(content, options);
            return deserialized;
        }

        private static async Task<HttpResponseMessage> GetRepos(string token, string api)
        {
            // const string token = "5ac3afcb86aafd69eaf4ecb9f4a8bbd34324d592";
            using var client = new HttpClient();
            if (token != null)
            {
                client.DefaultRequestHeaders.Add("Authorization", $"token {token}");
            }

            client.DefaultRequestHeaders.Add("User-Agent", "charconstpoitner");
            var response = await client.GetAsync(api);
            return response;
        }
    }
}