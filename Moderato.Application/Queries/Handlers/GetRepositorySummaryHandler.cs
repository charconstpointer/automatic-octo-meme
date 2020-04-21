using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GitHub.Interfaces;
using MediatR;
using Moderato.Application.Extensions;
using Moderato.Application.Services;
using Moderato.Application.ViewModels;
using Moderato.Domain.Entities;

namespace Moderato.Application.Queries.Handlers
{
    public class GetRepositorySummaryHandler : IRequestHandler<GetRepositorySummary, RepositoryViewModel>
    {
        private readonly IGitClient _gitClient;
        private readonly IGitUsersService _gitUsersService;

        public GetRepositorySummaryHandler(IGitClient gitClient, IGitUsersService gitUsersService)
        {
            _gitClient = gitClient;
            _gitUsersService = gitUsersService;
        }

        public async Task<RepositoryViewModel> Handle(GetRepositorySummary request, CancellationToken cancellationToken)
        {
            var repositories = (await _gitClient.GetRepositories(request.UserName, request.Token)).ToList();
            var user = new User(request.UserName);
            user.AddRepositories(repositories.AsEntities());
            var summary = await _gitUsersService.CreateUserSummary(user);
            return summary;
        }
    }
}