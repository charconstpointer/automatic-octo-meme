using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using GitHub.Interfaces;
using GitHub.Models;
using MediatR;
using Moderato.Application.Queries;
using Moderato.Application.Queries.Handlers;
using Moderato.Application.Services;
using Moderato.Infrastructure.Services.GitUsers;
using Moq;
using Xunit;

namespace Moderato.Tests
{
    public class QueryHandlersTests
    {
        [Theory]
        [InlineData("charconstpointer")]
        public async Task GetRepositorySummary_ValidUser(string username)
        {
            var gitClient = new Mock<IGitClient>();
            var gitService = new GitUsersService();
            gitClient.Setup(x => x.GetRepositories(username, null)).ReturnsAsync(
                new List<UserRepository>()
                {
                    new UserRepository
                    {
                        Name = "foo",
                        Forks = 1,
                        Size = 2,
                        Stargazers = 3,
                        Watchers = 4
                    },
                    new UserRepository
                    {
                        Name = "bar",
                        Forks = 5,
                        Size = 6,
                        Stargazers = 7,
                        Watchers = 8
                    }
                });
            var query = new GetRepositorySummary(username, null);
            var handler = new GetRepositorySummaryHandler(gitClient.Object, gitService);
            var response = await handler.Handle(query, CancellationToken.None);
            response.Should().NotBeNull();
            response.Forks.Should().Be(3.0);
            response.Size.Should().Be(4.0);
            response.Stars.Should().Be(5.0);
            response.Watchers.Should().Be(6.0);
        }
    }
}