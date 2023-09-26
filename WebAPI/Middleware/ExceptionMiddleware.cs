using System.Net;
using System.Text.Json;
using FluentValidation;
using WebAPI.Models;

namespace WebAPI.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException e)
        {
            var validationErrors = e.Errors
                .Where(x => x != null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);

            await GenerateExceptionResponseAsync(e, context, (int)HttpStatusCode.UnprocessableEntity, validationErrors);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            await GenerateExceptionResponseAsync(e, context, (int)HttpStatusCode.InternalServerError);
        }
    }

    private async Task GenerateExceptionResponseAsync(
        Exception e,
        HttpContext context,
        int statusCode,
        Dictionary<string, string[]>? validationErrors = default)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new ErrorDetails(context.Response.StatusCode, e.Message, e.StackTrace?.Substring(0, 100));

        if (validationErrors is not null)
        {
            response.ValidationErrors = validationErrors;
        }

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        await context.Response.WriteAsync(json);
    }
}
