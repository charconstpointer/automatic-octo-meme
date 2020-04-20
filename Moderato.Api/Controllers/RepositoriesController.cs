using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moderato.Application.Queries;

namespace Moderato.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RepositoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RepositoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userName:alpha}")]
        public async Task<IActionResult> GetSummary(string userName)
        {
            var token = Request.Headers["Authorization"];
            return Ok(await _mediator.Send(new GetRepositorySummary(userName, token)));
        }
    }
}