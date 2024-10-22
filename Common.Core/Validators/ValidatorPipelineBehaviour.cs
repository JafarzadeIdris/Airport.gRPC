using Common.Core.Queries;
using FluentValidation;
using MediatR;

namespace Common.Core.Validators
{
    public class ValidatorPipelineBehaviour<TRequest, TResponse>
        (IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IQuery<TRequest>

    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationContext = new ValidationContext<TRequest>(request);
            var validationResponse = await Task.WhenAll(validators.Select(x => x.ValidateAsync(validationContext, cancellationToken)));

            var validationErrors = validationResponse.Where(x => x.Errors.Any()).SelectMany(x => x.Errors).ToList();

            if (validationErrors.Any())
                throw new ValidationException(validationErrors);

            return await next();
        }
    }
}
