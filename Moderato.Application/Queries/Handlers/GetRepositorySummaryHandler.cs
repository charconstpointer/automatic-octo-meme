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
        private readonly IGitClient _gitClient;

        public GetRepositorySummaryHandler(IGitClient gitClient)
        {
            _gitClient = gitClient;
        }

        public async Task<RepositoryViewModel> Handle(GetRepositorySummary request, CancellationToken cancellationToken)
        {
            var repositories = await _gitClient.GetRepositories(request.UserName, request.Token);
            return repositories.AsViewModel(request.UserName);
        }
    }
}