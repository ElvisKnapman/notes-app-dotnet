using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using NotesApp.Api.DTOs.Responses;
using System.Text.Json;

namespace NotesApp.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public ExceptionHandlingMiddleware(
        RequestDelegate requestDelegate,
        ILogger<ExceptionHandlingMiddleware> logger,
        IOptions<JsonOptions> options
    )
    {
        _next = requestDelegate;
        _logger = logger;
        _jsonOptions = options.Value.SerializerOptions;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");

            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var error = new ErrorResponse("ServerError", "An unexpected error has occurred.");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var json = JsonSerializer.Serialize(error, _jsonOptions);

        return context.Response.WriteAsync(json);
    }
}
