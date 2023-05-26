using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> :
    IPipelineBehavior<
#nullable disable
    TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger) => this._logger = logger;

    public async Task<TResponse> Handle(
      TRequest request,
      CancellationToken cancellationToken,
      RequestHandlerDelegate<TResponse> next)
    {
        TResponse response;
        try
        {
            response = await next();
        }
        catch (Exception ex)
        {
            string name = typeof(TRequest).Name;
            if (!(ex is ValidationException))
                this._logger.LogError(ex, "Request: Unhandled Exception for Request {Name} {@Request}", (object)name, (object)request);
            throw;
        }
        return response;
    }
}
