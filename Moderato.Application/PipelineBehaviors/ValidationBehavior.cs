using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace Moderato.Application.PipelineBehaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var validationContext = new ValidationContext(request);
            var errors = _validators.Select(x => x.Validate(validationContext))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();
            if (errors.Any())
            {
                throw new ValidationException(errors);
            }

            return next();
        }
    }
}