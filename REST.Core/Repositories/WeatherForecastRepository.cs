using Microsoft.EntityFrameworkCore;
using REST.Core.Contracts;

namespace REST.Core.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly ApiContext _context;

        public WeatherForecastRepository(ApiContext context)
        {
            _context = context;
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

        public async Task<IEnumerable<WeatherForecastResponse>> GetAll(int days, int? minTemp, int? maxTemp)
        {
            var items = _context.WeatherForecasts
                .Where(x => x.Date >= DateOnly.FromDateTime(DateTime.Now) &&
                    x.Date < DateOnly.FromDateTime(DateTime.Now.AddDays(days)));

            if (minTemp != null)
            {
                items = items.Where(x => x.TemperatureC >= minTemp);
            }

            if (maxTemp != null)
            {
                items = items.Where(x => x.TemperatureC <= maxTemp);
            }

            return items.Select(x => new WeatherForecastResponse
            {
                Id = x.Id,
                Date = x.Date,
                Summary = x.Summary,
                TemperatureC = x.TemperatureC
            });
        }

        public async Task<IEnumerable<WeatherForecastResponse>> GetForDate(DateTime date)
        {
            var items = await _context.WeatherForecasts
                .Where(x => x.Date == DateOnly.FromDateTime(date))
                .ToListAsync();

            return items.Select(x => new WeatherForecastResponse
            {
                Id = x.Id,
                Date = x.Date,
                Summary = x.Summary,
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
