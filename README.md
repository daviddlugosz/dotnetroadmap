# Dependency Injection (DI) in ASP.NET Core

## Overview

Dependency Injection (DI) is a design pattern that allows an object to receive its dependencies from outside rather than creating them internally. ASP.NET Core has built-in support for DI, making it easier to manage dependencies, enhance testability, and promote a clean, modular architecture.

## Learning Objectives:

- Understand the fundamental concept of Dependency Injection.
- Extract and encapsulate business logic into services.
- Implement and switch between different service implementations using DI.
- Appreciate the Separation of Concerns principle in software design.

## Prerequisites:

- Basic knowledge of C#.
- Familiarity with ASP.NET Core basics.

---

## Core Concepts of Dependency Injection (DI)

### What is Dependency Injection?

Dependency Injection (DI) is a design pattern that deals with how components acquire their dependencies. Instead of components creating their dependencies or using static references, their dependencies are "injected" into them, often via their constructors, setters, or methods.

### Why Use Dependency Injection?

1. **Separation of Concerns (SoC)**: By separating the creation of an object from its use, our code becomes more modular and easier to maintain.
2. **Testability**: With DI, it's easier to substitute real implementations with mock objects, making unit testing more straightforward.
3. **Reusability and Interchangeability**: When components are decoupled from their dependencies, it's easier to reuse them or replace one dependency with another.
4. **Configurability**: Externalize a component’s configuration from the component itself, allowing settings to be applied externally.

### Inversion of Control (IoC)

Dependency Injection is a form of Inversion of Control (IoC). IoC is a general principle where the flow of control of a system is inverted compared to procedural programming. In the case of DI, instead of a component controlling how and when a dependency is created, the system (often an IoC container) takes care of providing the required dependencies.

### Service Lifetimes

In ASP.NET Core's built-in DI, services can be registered with various lifetimes:

1. **Transient**: A new instance of the service is created each time it's requested.
    * **Example**: Consider a service that generates a unique **temporary token** for a user action. If you want a new token every time you request one, you'd use a transient service.

```csharp
    public interface ITemporaryTokenGenerator
{
    string GenerateToken();
}

public class TemporaryTokenGenerator : ITemporaryTokenGenerator
{
    public string GenerateToken()
    {
        return Guid.NewGuid().ToString();
    }
}
```
```csharp
services.AddTransient<ITemporaryTokenGenerator, TemporaryTokenGenerator>();
```
    
2. **Scoped**: A new instance of the service is created once per request (e.g., for each web request in ASP.NET Core).
    * **Example**: In a web application, **user session data** might need to persist throughout a single web request but not across multiple requests. This can be achieved with a scoped service.

```csharp
public interface IUserSession
{
    string UserId { get; set; }
    // ... other session data
}

public class UserSession : IUserSession
{
    public string UserId { get; set; }
    // ... other session data
}

```
```csharp
services.AddScoped<IUserSession, UserSession>();
```

3. **Singleton**: A single instance is created and that same instance is returned on every subsequent request.
    * **Example**: If you have a **configuration manager** that loads configuration settings when the application starts and retains them for all subsequent requests, a singleton service would be appropriate.

```csharp
public interface IConfigurationManager
{
    string GetSetting(string key);
}

public class ConfigurationManager : IConfigurationManager
{
    private Dictionary<string, string> _settings;

    public ConfigurationManager()
    {
        _settings = LoadSettingsFromDisk();
    }

    private Dictionary<string, string> LoadSettingsFromDisk()
    {
        // Logic to load settings from a configuration file
    }

    public string GetSetting(string key)
    {
        return _settings.ContainsKey(key) ? _settings[key] : null;
    }
}
```
```csharp
services.AddSingleton<IConfigurationManager, ConfigurationManager>();
```
---

### Practical DI Example in ASP.NET Core:

Consider an e-commerce application where we have a service that calculates discounts. Initially, it may calculate discounts based on flat rates, but in the future, we anticipate supporting various discount strategies.

Without DI, you might have:
```csharp
public class OrderProcessor
{
    private FlatRateDiscountService _discountService = new FlatRateDiscountService();
    
    public void ProcessOrder(Order order)
    {
        var discount = _discountService.ApplyDiscount(order);
        // ... other logic
    }
}
```

With DI, this tight coupling can be removed:

```csharp
public class OrderProcessor
{
    private IDiscountService _discountService;

    public OrderProcessor(IDiscountService discountService)
    {
        _discountService = discountService;
    }
    
    public void ProcessOrder(Order order)
    {
        var discount = _discountService.ApplyDiscount(order);
        // ... other logic
    }
}
```
In the DI example, the OrderProcessor no longer depends directly on FlatRateDiscountService. Instead, it works with any implementation of IDiscountService that's injected into it, providing more flexibility.

---

## Practical Exercise: Switching Data Sources with Dependency Injection

### Step 1: Extract Logic from `WeatherForecastController`

Remove logic that generates weather forecasts, as this will be handled by our services:

```csharp
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _forecastService;

    public WeatherForecastController(IWeatherForecastService forecastService)
    {
        _forecastService = forecastService;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return _forecastService.GetForecasts();
    }
}
```

### Step 2: Create the `IWeatherForecastService` Interface & Implementations

#### Interface:

```csharp
public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> GetForecasts();
}
```
#### Cached Service

```csharp
public class CachedWeatherForecastService : IWeatherForecastService
{
    private List<WeatherForecast> _cachedForecasts = new List<WeatherForecast>
    {
        new WeatherForecast { Date = DateTime.Now.AddDays(1), TemperatureC = 22, Summary = "Sunny" },
        // ... add more cached data
    };

    public IEnumerable<WeatherForecast> GetForecasts()
    {
        return _cachedForecasts;
    }
}
```

#### Mocked service

```csharp
public class MockedWeatherForecastService : IWeatherForecastService
{
    private readonly Random _rng = new Random();

    public IEnumerable<WeatherForecast> GetForecasts()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = _rng.Next(-20, 55),
            Summary = Summaries[_rng.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
```

#### Step 3: Register Services in Program.cs

Register the Cached service (or the Mocked one):

```csharp
services.AddTransient<IWeatherForecastService, CachedWeatherForecastService>();
// Or
// services.AddTransient<IWeatherForecastService, MockedWeatherForecastService>();
```

#### Step 4: Test the Service & Switching
Run the application and navigate to /weatherforecast. Notice the data output, and then switch between services in Startup.cs to observe the flexibility offered by DI.

### Conclusion & Insights
By simply adjusting a single line in Startup.cs, we switched between a cached and a mocked data source. This exercise highlights how Dependency Injection allows applications to adapt seamlessly to different needs without having to rewrite or adjust core components.

## Review & Questions
Explain the understanding of DI during code review session on your practical example.

To ensure your understanding, you should be able to answer the following:

- What is Dependency Injection, and why is it beneficial?
- Explain the three service lifetimes in ASP.NET Core DI. Give an example use case for each.
- How do you register a service for DI in an ASP.NET Core application?
- How do you consume a registered service in a controller?
- How does DI help in unit testing?