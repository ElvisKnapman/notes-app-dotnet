namespace NotesApp.Api.Middleware;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimingMiddleware> _logger;

    public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();

        try
        {
            await _next(context);
        }
        finally
        {
            sw.Stop();

            var elapsedMs = sw.Elapsed.TotalMilliseconds;
            var method = context.Request.Method;
            var path = context.Request.Path.HasValue ? context.Request.Path.Value : "";
            var statusCode = context.Response?.StatusCode;

            // Log at Information by default; use Debug for high-volume environments
            _logger.LogInformation(
                "Request {Method} {Path} responded {StatusCode} in {Elapsed:0.000} ms",
                method, path, statusCode, elapsedMs);
        }
    }
}
