using System.Collections.Generic;
using System.Linq;
using GitHub.Models;
using Moderato.Application.ViewModels;

namespace Moderato.Application.Extensions
{
    public static class RepositoryMapper
    {
        public static RepositoryViewModel AsViewModel(this IEnumerable<UserRepository> repositories)
        {
            var userRepositories = repositories.ToList();
            return new RepositoryViewModel
            {
                Stars = userRepositories.Average(r => r.Stargazers),
                Watchers = userRepositories.Average(r => r.Watchers),
                Forks = userRepositories.Average(r => r.Forks),
                Size = userRepositories.Average(r => r.Size),
                Letters = userRepositories
                    .SelectMany(r => r.Name.ToCharArray())
                    .GroupBy(r => r)
                    .Select(r => new {Letter = r.FirstOrDefault(), Count = r.Count()})
            };
        }
    }
}