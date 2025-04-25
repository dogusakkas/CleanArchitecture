using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var errorDictionary = _validators.Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .GroupBy(x => x.PropertyName, x => x.ErrorMessage, (propertyName, errorMessage) => new
                {
                    Key = propertyName,
                    Values = errorMessage.Distinct().ToArray()
                })
                .ToDictionary(x => x.Key, y => y.Values[0]);

            if (errorDictionary.Any())
            {
                var errors = errorDictionary.Select(x => new ValidationFailure
                {
                    PropertyName = x.Key,
                    ErrorMessage = x.Value
                });
                throw new ValidationException(errors);
            }
            return await next();
        }
    }
}
