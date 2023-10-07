
# # Minimal APIs in ASP.NET Core

## Overview

ASP.NET Core introduced the concept of "Minimal APIs" to allow developers to build lightweight, succinct, and performant HTTP APIs with minimal boilerplate. Minimal APIs reduce the amount of ceremony required to set up a web API, making it faster and more straightforward to get up and running.

## Learning objectives

-   The motivation behind Minimal APIs.
-   How to create a Minimal API from scratch.
-   Working with request and response.
-   Basic routing and handling HTTP methods.

## Prerequisites

-   Basic understanding of C#.
-   Familiarity with HTTP and RESTful services.
-   Experience with ASP.NET Core would be beneficial but not required.

## Why Minimal APIs?

-   **Conciseness**: Less boilerplate means clearer and more readable code.
-   **Performance**: Stripped-down approach leads to faster startup times.
-   **Flexibility**: Easier to create lightweight microservices or standalone APIs.

### Setting Up a Minimal API

You no longer need a controller to get started. A simple `Program.cs` file is enough.

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello, World!");

app.Run();
```

### Middleware in Minimal APIs

Middleware components handle requests and responses, forming the request processing pipeline in ASP.NET Core. In Minimal APIs, you can add and configure middleware just like you do in traditional ASP.NET Core applications, but the way you set it up might be slightly different given the succinct nature of Minimal APIs.

#### Adding Middleware

In the context of Minimal APIs, middleware can be added using methods like `Use...` on the `WebApplication` instance. Here's a basic example of adding a middleware that logs the request path:

```csharp
var app = builder.Build();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Request for {context.Request.Path}");
    await next();
});

app.MapGet("/", () => "Hello, World!");

app.Run();
```
This middleware will print the request path to the console every time a request is made.

#### Ordering

The order in which you add middleware components is essential. In the example above, if you map the root route before adding the middleware, the middleware won't execute for that route. Middleware runs in the order they're added.

### Dependency Injection (DI) in Minimal APIs

ASP.NET Core has built-in support for dependency injection. With Minimal APIs, this mechanism remains, allowing you to inject services directly into your route handlers.

#### Registering Services

You can register services using the `Services` property of the application builder. Here's an example:

```csharp
builder.Services.AddSingleton<MyService>();
```

#### Using DI in Handlers

Once a service is registered, you can have it automatically injected into your route handlers. Here's how you can do that:

```csharp
app.MapGet("/myendpoint", (MyService myService) => 
{
    // Use myService here
    return "Response";
});
```

In this example, when the "/myendpoint" route is hit, ASP.NET Core will automatically resolve `MyService` from the DI container and pass it to the handler.

#### Scoped and Transient Services

Just like in traditional ASP.NET Core apps, you can register services as singleton, scoped, or transient:

-   **Singleton**: A single instance is created and shared across all requests.
-   **Scoped**: A new instance is created for each request.
-   **Transient**: A new instance is created every time the service is requested.

#### Built-in Services

ASP.NET Core's Minimal APIs have several built-in services you can inject without registering them, such as `ILogger<T>`, `IConfiguration`, and `IHttpContextAccessor`.

## Practical Exercise: Weather Forecast Minimal API
For this practical exercise, we'll be implementing Minimal API out of ASP.NET Core Weather Forecast example.

**Setting Up the Data Model**

```csharp
public record WeatherForecast(DateTime Date, int TemperatureC, string Summary);` 
```

**Generate Sample Data**

```csharp
var rng = new Random();
var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast(
    Date: DateTime.Now.AddDays(index),
    TemperatureC: rng.Next(-20, 55),
    Summary: summaries[rng.Next(summaries.Length)])
).ToArray();` 
```

**Map Endpoints**

```csharp
app.MapGet("/weather", () => forecasts);` 
```

This maps an HTTP GET request to the "/weather" route to the generated sample data.

### Conclusion & Insights

ASP.NET Core's Minimal APIs offer a streamlined approach to building web services, emphasizing succinctness and clarity. Despite their lightweight nature, they seamlessly integrate with the foundational features of the framework, such as middleware and dependency injection. This combination ensures developers can craft efficient web APIs without compromising on functionality, extensibility, or best practices, demonstrating the evolving and adaptive nature of the ASP.NET Core platform.

## Review & Questions
Explain the understanding of Minimal API during code review session on your practical example.

To ensure your understanding, you should be able to answer the following:

- How does a Minimal API differ from a traditional ASP.NET Core API?
- How can you handle different HTTP methods in a Minimal API?
- Explain how routing works in the context of Minimal APIs.
- How would you integrate middleware into a Minimal API project?
- How can you inject services into your Minimal API handler methods?


TODO:
-   study materials (links, videos)
-   refinement of excerise (make it as task for junior to do on his own, not just copy paste)
-   test code in this file
-   prepare code for this