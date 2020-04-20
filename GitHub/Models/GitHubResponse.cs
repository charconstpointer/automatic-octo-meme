using System.Collections;
using System.Collections.Generic;

namespace GitHub.Models
{
    public enum ResponseStatus
    {
        Success,
        Failure
    }
    public class GitHubResponse<T>
    {
        public ResponseStatus Status { get; set; }
        public IEnumerable<T> Body { get; set; }
    }
}