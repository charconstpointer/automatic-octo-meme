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
            var mediator = new Mock<IMediator>();
            var gitClient = new Mock<IGitHubClient>();
            gitClient.Setup(x => x.GetRepositories(username, null)).ReturnsAsync(
                new List<UserRepository>()
                {
                    new UserRepository {Name = "foo"},
                    new UserRepository {Name = "bar"}
                });
            var query = new GetRepositorySummary(username, null);
            var handler = new GetRepositorySummaryHandler(gitClient.Object);
            var response = await handler.Handle(query, CancellationToken.None);
            response.Should().NotBeNull();
        }
    }
}