using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moderato.Domain.Entities;
using Xunit;

namespace Moderato.Tests
{
    internal class UserFactory
    {
        public static User CreateSample() => new User("User");
    }

    internal class RepositoryFactory
    {
        public static Repository CreateSample(string name, int size, int forks, int stars, int watchers)
            => new Repository(name, size, forks, stars, watchers);
    }

    public class UserTests
    {
        [Fact]
        public void User_GivenRepositories_AddsThemCorrectly()
        {
            var user = UserFactory.CreateSample();
            const string repo1 = "Repo#1";
            const string repo2 = "Repo#2";
            var repositories = new List<Repository>
            {
                RepositoryFactory.CreateSample(repo1, 1, 2, 3, 4),
                RepositoryFactory.CreateSample(repo2, 4, 3, 1, 9)
            };
            user.AddRepositories(repositories);
            user.Repositories.Should().NotBeNull();
            user.Repositories.Should().HaveCount(repositories.Count);
            user.Repositories.ToList()[0].Name.Should().Be(repo1);
            user.Repositories.ToList()[1].Name.Should().Be(repo2);
            
        }
    }
}