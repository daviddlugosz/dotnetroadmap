using Microsoft.EntityFrameworkCore;
using REST.Core;
using REST.Core.Contracts;
using REST.Core.Repositories;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApiContext>
    (o => o.UseInMemoryDatabase("WeatherForecasts"));

builder.Services.AddTransient<IWeatherForecastRepository, WeatherForecastRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

AddWeatherForecasts(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", async (IWeatherForecastRepository repository) => await repository.GetAll());
app.MapGet("/{id}", async (IWeatherForecastRepository repository, int id) => await repository.Get(id));
app.MapPost("/", async (IWeatherForecastRepository repository, CreateWeatherForecastRequest request) => await repository.Create(request));
app.MapPut("/", async (IWeatherForecastRepository repository, UpdateWeatherForecastRequest request) => await repository.Update(request));
app.MapDelete("/", async (IWeatherForecastRepository repository, int id) => await repository.Delete(id));

app.Use(async (context, next) =>
{
    if (context.Response.HttpContext.Response.StatusCode == (int)HttpStatusCode.OK);
    {
        context.Response.HttpContext.Response.StatusCode = (int)HttpStatusCode.Found;
    }
    await next();
});

app.Run();

static void AddWeatherForecasts(WebApplication app)
{
    var Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetService<ApiContext>();

    var forecasts = new List<WeatherForecast>
    {
        new WeatherForecast
        {
            Id = 1,
            Date = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-7)),
            Summary = Summaries[0],
            TemperatureC = -10
        },
        new WeatherForecast
        {
            Id = 2,
            Date = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-6)),
            Summary = Summaries[1],
            TemperatureC = -2
        },
        new WeatherForecast
        {
            Id = 3,
            Date = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-5)),
            Summary = Summaries[2],
            TemperatureC = 0
        },
        new WeatherForecast
        {
            Id = 4,
            Date = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-4)),
            Summary = Summaries[3],
            TemperatureC = 7
        },
        new WeatherForecast
        {
            Id = 5,
            Date = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-3)),
            Summary = Summaries[4],
            TemperatureC = 12
        },
        new WeatherForecast
        {
            Id = 6,
            Date = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-2)),
            Summary = Summaries[5],
            TemperatureC = 18
        },
        new WeatherForecast
        {
            Id = 7,
            Date = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(-1)),
            Summary = Summaries[6],
            TemperatureC = 22
        },
        new WeatherForecast
        {
            Id = 8,
            Date = DateOnly.FromDateTime(DateTime.Now.Date),
            Summary = Summaries[7],
            TemperatureC = 28
        },
        new WeatherForecast
        {
            Id = 9,
            Date = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(1)),
            Summary = Summaries[8],
            TemperatureC = 33
        },
            new WeatherForecast
        {
            Id = 10,
            Date = DateOnly.FromDateTime(DateTime.Now.Date.AddDays(2)),
            Summary = Summaries[9],
            TemperatureC = 39
        },
    };

    db.WeatherForecasts.AddRange(forecasts);

    db.SaveChanges();
}