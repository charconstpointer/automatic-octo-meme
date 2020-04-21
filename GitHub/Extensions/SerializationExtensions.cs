using System.Collections.Generic;
using GitHub.Models;
using Newtonsoft.Json;

namespace GitHub.Extensions
{
    public static class SerializationExtensions
    {
        public static IEnumerable<UserRepository> AsEntity(this string source)
        {
            return JsonConvert.DeserializeObject<IEnumerable<UserRepository>>(source);
        }
    }
}