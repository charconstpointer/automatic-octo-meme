using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moderato.Application.Services;
using Moderato.Domain.Entities;
using Moderato.Infrastructure.Services.GitUsers;
using Moq;
using Xunit;

namespace Moderato.Tests
{
    public class GitUsersServiceTests
    {
        [Fact]
        public async Task GitUsersService_GivenUser_CreatesValidSummary()
        {
            var user = UserFactory.CreateSample();
            const string repo1 = "Repo#1";
            const string repo2 = "Rep#2";
            var repositories = new List<Repository>
            {
                RepositoryFactory.CreateSample(repo1, 1, 2, 3, 4),
                RepositoryFactory.CreateSample(repo2, 4, 3, 1, 9)
            };
            user.AddRepositories(repositories);
            var service = new GitUsersService();
            var summary = await service.CreateUserSummary(user);
            summary.Should().NotBeNull();
            summary.Forks.Should().Be(2.5);
            summary.Stars.Should().Be(2.0);
            summary.Watchers.Should().Be(6.5);
            summary.Letters.Any(x => x.Value > 0).Should().BeTrue();
            summary.Letters['r'].Should().Be(2);
            summary.Letters['e'].Should().Be(2);
            summary.Letters['p'].Should().Be(2);
            summary.Letters['o'].Should().Be(1);
        }
    }
}