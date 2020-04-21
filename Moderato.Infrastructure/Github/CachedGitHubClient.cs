using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GitHub.Extensions;
using GitHub.Interfaces;
using GitHub.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Moderato.Infrastructure.Github
{
    /// <summary>
    /// Caching git client with a decorator
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CachedGitHubClient : IGitClient
    {
        private readonly IGitClient _gitClient;
        private readonly IDistributedCache _cache;

        public CachedGitHubClient(IGitClient gitClient, IDistributedCache cache)
        {
            _gitClient = gitClient;
            _cache = cache;
        }

        public async Task<IEnumerable<UserRepository>> GetRepositories(string userName, string token)
        {
            var cachedResponse = await _cache.GetStringAsync(userName);
            if (!string.IsNullOrEmpty(cachedResponse))
            {
                return cachedResponse.AsEntity();
            }

            var response = await _gitClient.GetRepositories(userName, token);
            return response;

        }
    }
}