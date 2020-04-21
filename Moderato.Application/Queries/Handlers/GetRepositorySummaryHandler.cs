using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GitHub.Interfaces;
using MediatR;
using Moderato.Application.Extensions;
using Moderato.Application.ViewModels;

namespace Moderato.Application.Queries.Handlers
{
    public class GetRepositorySummaryHandler : IRequestHandler<GetRepositorySummary, RepositoryViewModel>
    {
        private readonly IGitHubClient _gitHubClient;

        public GetRepositorySummaryHandler(IGitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
        }

        public async Task<RepositoryViewModel> Handle(GetRepositorySummary request, CancellationToken cancellationToken)
        {
            var repositories = await _gitHubClient.GetRepositories(request.UserName, request.Token);
            return repositories.AsViewModel(request.UserName);
        }
    }
}