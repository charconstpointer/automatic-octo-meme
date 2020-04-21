using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GitHub.Models;
using Moderato.Application.Extensions;
using Xunit;

namespace Moderato.Tests
{
    public class RepositoryMapperTests
    {
        [Fact]
        public void RepositoryMapper_GivenNonEmptyCollection_ShouldReturnCorrectOccurrences()
        {
            var repos = new List<UserRepository>
            {
                new UserRepository
                {
                    Forks = 1,
                    Name = "foo",
                    Size = 2,
                    Stargazers = 3,
                    Watchers = 4
                },
                new UserRepository
                {
                    Forks = 2,
                    Name = "bar",
                    Size = 3,
                    Stargazers = 1,
                    Watchers = 9
                }
            };
            var summary = RepositoryMapper.SummarizeRepository(repos);
            summary.Should().NotBeNull();
            summary.Values.Any(x => x > 0).Should().BeTrue();
            summary['o'].Should().Be(2);
            summary['a'].Should().Be(1);
            summary['f'].Should().Be(1);
            summary['r'].Should().Be(1);
            summary['b'].Should().Be(1);
        }
        
        [Fact]
        public void RepositoryMapper_GivenEmptyCollection_ShouldReturnInitialValues()
        {
            var repos = new List<UserRepository>();
            var summary = RepositoryMapper.SummarizeRepository(repos);
            summary.Should().NotBeNull();
            summary.Values.Any(x => x > 0).Should().BeFalse();
        }
    }
}