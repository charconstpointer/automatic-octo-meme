using System.Collections.Generic;
using System.Linq;
using GitHub.Models;
using Moderato.Domain.Entities;

namespace Moderato.Application.Extensions
{
    public static class RepositoryMapper
    {
        public static IEnumerable<Repository> AsEntities(this IEnumerable<UserRepository> repositories)
        {
            return repositories.Select(x => new Repository(x.Name, x.Size, x.Forks, x.Stargazers, x.Watchers));
        }
    }
}