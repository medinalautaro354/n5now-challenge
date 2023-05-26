using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Behaviours;
public class ValidationBehavoir<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavoir(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (_validators.Any())
        {
            ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
            List<ValidationFailure> list = (from f in (await Task.WhenAll(_validators.Select((IValidator<TRequest> v) => v.ValidateAsync(context, cancellationToken)))).SelectMany((ValidationResult r) => r.Errors)
                                            where f != null
                                            select f).ToList();
            if (list.Count != 0)
            {
                throw new ValidationException(list);
            }
        }

        return await next();
    }
}
