using Console = System.Diagnostics.Debug;   //to see messages in Immediate Window (Tools - Options - Debug - General: "Redirect all Output Window text to the Immediate Window" must be enabled)

namespace REST.Core.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log before the action
            Console.WriteLine($"Before Action: {DateTime.UtcNow}: {context.Request.Method} - {context.Request.Path}");

            // Call the next middleware in the pipeline
            await _next(context);

            // Log after the action
            Console.WriteLine($"After Action: {DateTime.UtcNow}: Response - {context.Response.StatusCode}");
        }
    }
}
