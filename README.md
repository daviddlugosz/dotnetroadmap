# Middleware in ASP.NET Core

## Prerequisites
Basic understanding of C# and .NET Core.
Familiarity with ASP.NET Core project structure.

### Learning Objectives
By the end of this module, students should be able to:

- Explain the concept and purpose of middleware in ASP.NET Core.
- Understand the middleware pipeline and its behavior.
- Identify and describe the default middlewares in ASP.NET Core.
- Build and register custom middleware components.
- Describe the impact of middleware order and short-circuiting.
- Introduction to Middleware
- Middleware in ASP.NET Core provides a way to handle requests and responses in a modular and ordered fashion. Each middleware component has access to the HttpContext and can perform operations both before and after the next middleware in the pipeline.

## How Middleware Works
1. Middleware Pipeline: Middleware components form a sequential pipeline. When a request arrives, it travels through each middleware in the order they're registered.

2. Short-circuiting: If a middleware doesn't pass the request to the next component (by not calling _next(context)), the request is short-circuited. This means any downstream middleware components aren't executed.

3. Before and After Actions: Middleware can handle operations both before and after the main request processing, offering flexibility in managing requests and responses.

### Text study material:
[ASP.NET Core Request Processing Pipeline](https://dotnettutorials.net/lesson/asp-net-core-request-processing-pipeline/)

[Write custom ASP.NET Core middleware](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-7.0)

### Video study material:
[![ASP.NET Core Understanding the middleware pipeline, Startup configuration - Application Life Cycle](https://img.youtube.com/vi/2SRUc7zZiyw/0.jpg)](https://www.youtube.com/watch?v=2SRUc7zZiyw)
[![MIDDLEWARE in ASP.NET Core | Getting Started With ASP.NET Core Series](https://img.youtube.com/vi/5eifH7LEnGo/0.jpg)](https://www.youtube.com/watch?v=5eifH7LEnGo)

## Practical Example: Logging Middleware

### Goal
Implement a middleware that logs each incoming request, including the method, path, time received, and the outgoing response status code.

### Steps

1. Create a Middleware Component

```csharp
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log before the action
        Console.WriteLine($"Before Action: {DateTime.UtcNow}: {context.Request.Method} - {context.Request.Path}");

        // Call the next middleware in the pipeline
        await _next(context);

        // Log after the action
        Console.WriteLine($"After Action: {DateTime.UtcNow}: Response - {context.Response.StatusCode}");
    }
}

```
2. Register the Middleware in .NET 7

In your Program.cs

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// ...

app.UseMiddleware<RequestLoggingMiddleware>();

// ...

app.Run();
```

3. Test the Middleware

Run the application and send some requests. Observe the console for the logged request details.

## Knowledge Validation
To validate your understanding, be prepared to answer the following:

1. How does the order of registered middleware impact the request processing?
2. What does it mean for a middleware to short-circuit a request?
3. Name three default middlewares and their primary functions.
4. How can middleware components handle tasks both before and after main request processing?
5. Why is the await _next(context); line important in the custom middleware example?

## Homework: Middleware Mastery

**Objective:** Demonstrate a clear understanding of middleware in ASP.NET Core by designing and implementing a series of custom middleware components.

### Tasks:

#### 1. Enhanced Logging Middleware:
   
   - Extend the `RequestLoggingMiddleware` we discussed in the lesson.
   - In addition to the current logs, capture and log the User-Agent from the request header and the total time taken to process the request.
   
#### 2. Request Limiting Middleware:

   - Create a middleware that limits the number of requests a client can make within a 10-minute window.
   - If a client exceeds 10 requests in this time frame, return a `429 Too Many Requests` status code without processing further middlewares.
   - *Hint:* Consider using the client's IP address as a way to track requests.

#### 3. Custom Authentication Middleware:

   - Design a middleware that checks for a custom header named `X-CUSTOM-API-KEY`.
   - If the header is present and has the value `12345`, let the request proceed.
   - If the header is missing or has an incorrect value, return a `401 Unauthorized` status code.

#### 4. Short-circuit Demonstration:

   - Build a middleware that checks the path of incoming requests. If the path is `/forbidden`, return a `403 Forbidden` status code without calling the next middleware.
   - Place this middleware before your logging middleware. Then, make a request to `/forbidden` and observe the logs. Is your logging middleware executed?

### Submission:

- Push your updated ASP.NET Core project to a GitHub repository.
- Submit the link to your GitHub repository.
