﻿using REST.Core.Contracts;

namespace REST.Core.Repositories
{
    public interface IWeatherForecastRepository
    {
        Task<IEnumerable<WeatherForecastResponse>> GetAll(int days, int? minTemp, int? maxTemp);
        Task<WeatherForecastResponse> Get(int id);
        Task<WeatherForecastResponse> Create(CreateWeatherForecastRequest forecast);
        Task<WeatherForecastResponse> Update(UpdateWeatherForecastRequest forecast);
        Task Delete(int id);
        Task<IEnumerable<WeatherForecastResponse>> GetForDate(DateTime date);
    }
}
