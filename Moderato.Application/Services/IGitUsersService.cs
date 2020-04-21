using System.Threading.Tasks;
using Moderato.Application.ViewModels;

namespace Moderato.Application.Services
{
    public interface IGitUsersService
    {
        Task<RepositoryViewModel> CreateUserSummary();
    }
}