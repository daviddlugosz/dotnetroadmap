using Microsoft.AspNetCore.Mvc.Filters;

namespace REST.FilterAttributes
{
    public class DoubleParameterActionFilter : Attribute, IActionFilter
    {
        private const string NumberOffForecastsParamName = "numberOfForecasts";

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey(NumberOffForecastsParamName))
            {
                var paramValue = context.ActionArguments[NumberOffForecastsParamName];

                if (paramValue != null && int.TryParse(paramValue.ToString(), out var intValue))
                {
                    if (intValue > 10)
                    {
                        throw new Exception("Maximum limit for number of forecasts is 10!");
                    }

                    context.ActionArguments[NumberOffForecastsParamName] = intValue * 2;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
