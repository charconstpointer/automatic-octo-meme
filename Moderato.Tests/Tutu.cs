using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Moderato.Api;
using Moderato.Application.ViewModels;
using Newtonsoft.Json;
using Xunit;

namespace Moderato.Tests
{
    public class Tutu
    {
        private readonly HttpClient _httpClient;

        public Tutu()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _httpClient = appFactory.CreateClient();
        }

        [Fact]
        public async Task Test()
        {
            var response = await _httpClient.GetAsync("/repositories/jskeet");
            response.StatusCode.Should().Be(200);
            response.IsSuccessStatusCode.Should().BeTrue();
            var contentType = response.Content.Headers.GetValues("Content-Type").FirstOrDefault();
            contentType.Should().NotBeNull();
            contentType.Should().Be("application/json; charset=utf-8");
            var content = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<RepositoryViewModel>(content);
            deserialized.Should().NotBeNull();
            deserialized.Forks.Should().BeGreaterThan(0);
            deserialized.Owner.Should().NotBeEmpty();
        }
    }
}