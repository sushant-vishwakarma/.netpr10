using System.Diagnostics;

namespace StudentManagementApi.Middleware
{
    /// <summary>
    /// Custom middleware that logs each incoming HTTP request, its response status code,
    /// and total execution duration in milliseconds.
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var method = context.Request.Method;
            var path = context.Request.Path;

            // Attach execution time header right before the response starts streaming
            context.Response.OnStarting(() =>
            {
                stopwatch.Stop();
                context.Response.Headers["X-Response-Time-Ms"] = stopwatch.ElapsedMilliseconds.ToString();
                return Task.CompletedTask;
            });

            _logger.LogInformation("Incoming Request: {Method} {Path}", method, path);

            // Pass control to the next middleware in the HTTP request pipeline
            await _next(context);

            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
            }

            _logger.LogInformation(
                "Completed Request: {Method} {Path} => Status {StatusCode} in {ElapsedMs} ms",
                method,
                path,
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }
}
