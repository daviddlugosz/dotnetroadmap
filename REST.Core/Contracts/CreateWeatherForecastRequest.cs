namespace REST.Core.Contracts
{
    public class CreateWeatherForecastRequest
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
    }
}
