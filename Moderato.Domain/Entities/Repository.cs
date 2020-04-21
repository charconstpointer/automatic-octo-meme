namespace Moderato.Domain.Entities
{
    public class Repository
    {
        public string Name { get; }
        public int Size { get; }
        public int Forks { get; }
        public int Stargazers { get; }
        public int Watchers { get; }

        public Repository(string name, int size, int forks, int stargazers, int watchers)
        {
            Name = name;
            Size = size;
            Forks = forks;
            Stargazers = stargazers;
            Watchers = watchers;
        }
    }
}