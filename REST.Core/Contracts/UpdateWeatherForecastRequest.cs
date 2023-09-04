namespace REST.Core.Contracts
{
    public class UpdateWeatherForecastRequest
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
    }
}
