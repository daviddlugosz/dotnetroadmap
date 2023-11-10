namespace REST.Core.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (context.Request.Headers.TryGetValue("X-CUSTOM-API-KEY", out Microsoft.Extensions.Primitives.StringValues value) && value.Equals("12345"))
            {
                // authorized, continue processing -> Call the next middleware in the pipeline
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 401; // 401 Unauthorized
            }
        }
    }
}
