using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitHub.Models;

namespace GitHub.Interfaces
{
    public interface IGitClient
    {
        /// <summary>
        /// Requires userName, oath token is optional, but you may face heavier rate limiting
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<UserRepository>> GetRepositories(string userName, string token);
    }
}