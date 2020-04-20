using MediatR;

namespace Moderato.Application.Queries
{
    public class GetRepositorySummary : IRequest<object>
    {
        public string UserName { get;  }
        public string Token { get; }

        public GetRepositorySummary(string userName, string token)
        {
            UserName = userName;
            Token = token;
        }
    }
}