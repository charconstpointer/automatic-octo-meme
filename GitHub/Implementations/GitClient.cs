using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GitHub.Exceptions;
using GitHub.Extensions;
using GitHub.Interfaces;
using GitHub.Models;

namespace GitHub.Implementations
{
    public class GitHubClient : IGitClient
    {
        private readonly HttpClient _httpClient;

        public GitHubClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<UserRepository>> GetRepositories(string userName, string token)
        {
            var api = $"https://api.github.com/users/{userName}/repos";
            var response = await GetRepos(token, api);
            if (!response.IsSuccessStatusCode)
            {
                HandleFailedRequest(response);
            }
            var repositories = await Deserialize(response);
            return repositories;
        }

        private static void HandleFailedRequest(HttpResponseMessage response)
        {
            throw response.StatusCode switch
            {
                HttpStatusCode.NotFound => new UserNotFoundException(),
                HttpStatusCode.Unauthorized => new UserNotAuthorizedException(),
                _ => new InfrastructureException()
            };
        }

        private static async Task<IEnumerable<UserRepository>> Deserialize(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content.AsEntity();
        }

        private async Task<HttpResponseMessage> GetRepos(string token, string api)
        {
            if (token != null)
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"token {token}");
            }

            _httpClient.DefaultRequestHeaders.Add("User-Agent", "charconstpoitner");
            var response = await _httpClient.GetAsync(api);
            return response;
        }
    }
}