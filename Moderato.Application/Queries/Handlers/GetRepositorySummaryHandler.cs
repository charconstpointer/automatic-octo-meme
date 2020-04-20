using System.Threading;
using System.Threading.Tasks;
using GitHub.Interfaces;
using MediatR;

namespace Moderato.Application.Queries.Handlers
{
    public class GetRepositorySummaryHandler : IRequestHandler<GetRepositorySummary, object>
    {
        private readonly IGitHubClient _gitHubClient;

        public GetRepositorySummaryHandler(IGitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
        }

        public async Task<object> Handle(GetRepositorySummary request, CancellationToken cancellationToken)
        {
            var repositories = await _gitHubClient.GetRepositories(request.UserName, request.Token);
            return repositories;
        }
    }
}