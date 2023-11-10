namespace REST.Core.Middleware
{
    public class RequestUrlCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestUrlCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestPath = context.Request.Path;

            if (requestPath.Value == null || !requestPath.Value.ToLower().Equals("/forbidden"))
            {
                // url path ok, continue processing -> Call the next middleware in the pipeline
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 403; // 403 Forbidden
            }
        }
    }
}
