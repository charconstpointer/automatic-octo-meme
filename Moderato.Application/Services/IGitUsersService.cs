using System.Threading.Tasks;
using Moderato.Application.ViewModels;
using Moderato.Domain.Entities;

namespace Moderato.Application.Services
{
    public interface IGitUsersService
    {
        Task<RepositoryViewModel> CreateUserSummary(User user);
    }
}