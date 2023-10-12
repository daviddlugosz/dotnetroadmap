using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using REST.Core.Contracts;

namespace REST.Core.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly ApiContext _context;
        private readonly WeatherSettings _configuration;

        public WeatherForecastRepository(ApiContext context, IOptions<WeatherSettings> config)
        {
            _context = context;
            _configuration = config.Value;
        }

        public async Task<WeatherForecastResponse> Create(CreateWeatherForecastRequest forecast)
        {
            var itemToCreate = new WeatherForecast
            {
                Date = forecast.Date,
                Summary = forecast.Summary,
                TemperatureC = forecast.TemperatureC
            };

            var createdItem = await _context.WeatherForecasts.AddAsync(itemToCreate);

            await _context.SaveChangesAsync();

            return new WeatherForecastResponse
            {
                Id = createdItem.Entity.Id,
                Date = createdItem.Entity.Date,
                Summary = createdItem.Entity.Summary,
                TemperatureC = createdItem.Entity.TemperatureC
            };
        }

        public async Task Delete(int id)
        {
            var itemToDelete = await _context.WeatherForecasts.FirstOrDefaultAsync(x => x.Id == id);

            if (itemToDelete != null)
            {
                _context.WeatherForecasts.Remove(itemToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<WeatherForecastResponse> Get(int id)
        {
            var item = await _context.WeatherForecasts.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                throw new Exception($"{GetType().Name} with id {id} not found!");
            }

            return new WeatherForecastResponse
            {
                Id = item.Id,
                Date = item.Date,
                Summary = item.Summary,
                TemperatureC = item.TemperatureC
            };
        }

        public async Task<IEnumerable<WeatherForecastResponse>> GetAll()
        {
            var items = await _context.WeatherForecasts
                .Take(_configuration.ForecastCount)
                .ToListAsync();

            return items.Select(x => new WeatherForecastResponse
            {
                Id = x.Id,
                Date = x.Date,
                Summary = _configuration.SummaryOverride,
                TemperatureC = x.TemperatureC
            });
        }

        public async Task<WeatherForecastResponse> Update(UpdateWeatherForecastRequest forecast)
        {
            var item = await _context.WeatherForecasts.FirstOrDefaultAsync(x => x.Id == forecast.Id);

            if (item == null)
            {
                throw new Exception($"{GetType().Name} with id {forecast.Id} not found!");
            }

            item.Date = forecast.Date;
            item.TemperatureC = forecast.TemperatureC;
            item.Summary = forecast.Summary;

            var updatedItem = _context.WeatherForecasts.Update(item);

            await _context.SaveChangesAsync();

            return new WeatherForecastResponse
            {
                Id = updatedItem.Entity.Id,
                Date = updatedItem.Entity.Date,
                Summary = updatedItem.Entity.Summary,
                TemperatureC = updatedItem.Entity.TemperatureC
            };
        }
    }
}
