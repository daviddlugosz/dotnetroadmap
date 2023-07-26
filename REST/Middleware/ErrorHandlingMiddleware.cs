using REST.Contracts;
using System.Net;
using System.Text.Json;

namespace REST.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var exceptionResponse = new ExceptionResponse
                {
                    ErrorMessage = ex.Message,
                    StackTrace = ex.StackTrace ?? string.Empty
                };
                var exceptionResponseJson = JsonSerializer.Serialize(exceptionResponse);
                await context.Response.WriteAsync(exceptionResponseJson);
            }
        }
    }
}
