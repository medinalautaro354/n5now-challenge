using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Behaviours;
public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<
#nullable disable
TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(
      ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        this._logger = logger;
    }

    public async Task<TResponse> Handle(
      TRequest request,
      CancellationToken cancellationToken,
      RequestHandlerDelegate<TResponse> next)
    {
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
        {
            MaxDepth = 0
        };
        this._logger.LogInformation("Handling Request: {name} with params: {json}", (object)typeof(TRequest).Name, (object)JsonSerializer.Serialize<TRequest>(request, jsonOptions));
        TResponse response = await next();
        this._logger.LogInformation("Response Request: {name} with Content: {json}", (object)typeof(TRequest).Name, (object)JsonSerializer.Serialize<TResponse>(response, jsonOptions));
        return response;
    }
}

