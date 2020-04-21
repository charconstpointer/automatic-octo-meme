using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moderato.Application.Services;
using Moderato.Application.ViewModels;
using Moderato.Domain.Entities;
using Moderato.Infrastructure.Exceptions;

namespace Moderato.Infrastructure.Services.GitUsers
{
    public class GitUsersService : IGitUsersService
    {
        public async Task<RepositoryViewModel> CreateUserSummary(User user)
        {
            if (user.Repositories.Any())
            {
                var letters = SummarizeRepository(user);
                return await Task.FromResult(new RepositoryViewModel
                {
                    Owner = user.Username,
                    Stars = user.Repositories.AsParallel().Average(r => r.Stargazers),
                    Watchers = user.Repositories.AsParallel().Average(r => r.Watchers),
                    Forks = user.Repositories.AsParallel().Average(r => r.Forks),
                    Size = user.Repositories.AsParallel().Average(r => r.Size),
                    Letters = letters
                });
            }

            throw new InsufficientRepositoryCount();
        }

        public static Dictionary<char, int> SummarizeRepository(User user)
        {
            var userRepositories = user.Repositories;
            var letters = InitializeDictionary();
            CountCharacters(userRepositories, letters);
            return letters;
        }

        private static void CountCharacters(IEnumerable<Repository> userRepositories,
            IDictionary<char, int> letters)
        {
            var occurrences = userRepositories
                .SelectMany(r => r.Name.ToLower().ToCharArray())
                .Where(char.IsLetter);
            foreach (var occurrence in occurrences)
            {
                letters[occurrence]++;
            }
        }

        /// <summary>
        /// Initializes dictionary that counts character occurrences, from a-z, case insensitive
        /// </summary>
        /// <returns></returns>
        private static Dictionary<char, int> InitializeDictionary()
        {
            var letters = new Dictionary<char, int>();
            for (var i = 97; i < 123; i++)
            {
                var character = (char) i;
                letters[character] = 0;
            }

            return letters;
        }
    }
}