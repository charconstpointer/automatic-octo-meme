using System.Collections.Generic;
using System.Linq;

namespace Moderato.Domain.Entities
{
    public class User
    {
        public string Username { get; }
        private readonly ISet<Repository> _repositories;

        public User(string username)
        {
            _repositories = new HashSet<Repository>();
            Username = username;
        }

        public IEnumerable<Repository> Repositories => _repositories.ToList();

        public void AddRepository(Repository repository)
        {
            _repositories.Add(repository);
        }

        public void AddRepositories(IEnumerable<Repository> repositories)
        {
            foreach (var repository in repositories)
            {
                AddRepository(repository);
            }
        }
    }
}