using System.Diagnostics;

namespace REST.Core.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Stopwatch _stopwatch;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            _stopwatch = Stopwatch.StartNew();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log before the action
            Console.WriteLine($"Before Action: {DateTime.UtcNow}: {context.Request.Method} - {context.Request.Path}");

            Console.WriteLine($"Request header User-Agent: {context.Request.Headers.UserAgent}");

            _stopwatch.Restart();

            // Call the next middleware in the pipeline
            await _next(context);

            _stopwatch.Stop();
            Console.WriteLine($"Request duration: {_stopwatch.Elapsed}");

            // Log after the action
            Console.WriteLine($"After Action: {DateTime.UtcNow}: Response - {context.Response.StatusCode}");
        }
    }
}
