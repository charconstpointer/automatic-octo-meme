using System.Collections.Generic;
using System.Linq;
using GitHub.Models;
using Moderato.Application.ViewModels;

namespace Moderato.Application.Extensions
{
    public static class RepositoryMapper
    {
        public static RepositoryViewModel AsViewModel(this IEnumerable<UserRepository> repositories, string username)
        {
            var userRepositories = repositories.ToList();
            var letters = SummarizeRepository(userRepositories);

            return new RepositoryViewModel
            {
                Owner = username,
                Stars = userRepositories.AsParallel().Average(r => r.Stargazers),
                Watchers = userRepositories.AsParallel().Average(r => r.Watchers),
                Forks = userRepositories.AsParallel().Average(r => r.Forks),
                Size = userRepositories.AsParallel().Average(r => r.Size),
                Letters = letters
            };
        }

        public static Dictionary<char, int> SummarizeRepository(IEnumerable<UserRepository> userRepositories)
        {
            var letters = InitializeDictionary();
            CountCharacters(userRepositories, letters);
            return letters;
        }

        /// <summary>
        /// Flattens and counts characters
        /// </summary>
        /// <param name="userRepositories"></param>
        /// <param name="letters"></param>
        private static void CountCharacters(IEnumerable<UserRepository> userRepositories,
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