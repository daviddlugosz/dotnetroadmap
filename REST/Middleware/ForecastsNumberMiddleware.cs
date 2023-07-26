using System.Net;

namespace REST.Middleware
{
    public class ForecastsNumberMiddleware
    {
        private readonly RequestDelegate _next;
        private const string NumberOfForecastsQueryParamName = "numberOfForecasts";

        public ForecastsNumberMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Query.ContainsKey(NumberOfForecastsQueryParamName))
            {
                var queryParam = context.Request.Query[NumberOfForecastsQueryParamName].ToString();
                var parsed = int.TryParse(queryParam, out var numberOfForecasts);

                if (parsed && numberOfForecasts < 1)
                {
                    context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                    return;
                }
            }

            await _next(context);
        }
    }
}
