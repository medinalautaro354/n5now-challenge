using System.Net;
using System.Text.Json;
using FluentValidation;
using N5NowApi.Application.Classes;

namespace N5NowApi.Middlewares;

internal sealed class FluentValidationMiddleware : IMiddleware
{
    private readonly ILogger<FluentValidationMiddleware> _logger;

    public FluentValidationMiddleware(ILogger<FluentValidationMiddleware> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            if (ex is ValidationException)
            {
                await HandleFluentValidationExceptionAsync(context, ex);
            }
            else
            {
                await HandleServerExceptionAsync(context, ex);
            }
        }
    }

    private async Task HandleFluentValidationExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = (int)HttpStatusCode.BadRequest;
        var errors = GetErrors(exception);

        foreach (var notify in errors)
        {
            _logger.LogError(JsonSerializer.Serialize(notify));
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(errors));
    }

    private async Task HandleServerExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = (int)HttpStatusCode.InternalServerError;
        var errors = GetErrors(exception);

        foreach (var notify in errors)
        {
            _logger.LogError(JsonSerializer.Serialize(notify));
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(errors));
    }

    private List<Notify> GetErrors(Exception exception)
    {
        if (exception is ValidationException)
        {
            return (exception as ValidationException).Errors.Select(x => new Notify
            {
                Code = x.ErrorCode,
                Message = x.ErrorMessage,
                Property = x.PropertyName
            }).ToList();
        }

        return new List<Notify>()
        {
            new()
            {
                Code = "500 internal server error",
                Property = exception.GetType().ToString(),
                Message = exception.Message
            }
        };
    }
}