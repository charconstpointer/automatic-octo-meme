using FluentValidation;
using Moderato.Application.Queries;

namespace Moderato.Api.Validators
{
    public class GetRepositorySummaryValidator : AbstractValidator<GetRepositorySummary>
    {
        public GetRepositorySummaryValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty();
        }
    }
}