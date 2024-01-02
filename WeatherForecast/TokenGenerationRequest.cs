using System.Text.Json;

namespace WeatherForecast
{
    public class TokenGenerationRequest
    {
        public Guid UserId { get; set; }
        public string? Email { get; set; }
        public Dictionary<string, JsonElement> CustomClaims { get; set; } = new Dictionary<string, JsonElement>();
    }
}
