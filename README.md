
# Routing in ASP.NET Core

## Overview

In ASP.NET Core, routing serves as a mechanism to map incoming HTTP requests to respective route handlers (typically controllers and their actions). It plays an integral role in web applications by determining how URLs should be matched and which code should be executed.

## Learning objectives

-   Understand how ASP.NET Core routes HTTP requests to the correct controller actions.
-   Distinguish between attribute routing and convention-based routing, and know when to use each.
-   Implement advanced routing techniques, such as route constraints and route data extraction.

## Prerequisites

-   Basic knowledge of C#.
-   Familiarity with the ASP.NET Core web framework.
-   Understanding of RESTful principles.

## Core Concepts of Routing

### Route Templates 

These are strings with placeholders (like `{id}`) that can match URLs. They're used to define routes.

Route templates define the URL pattern that the HTTP endpoint matches. For example, the template `products/{productId}` might match the URL `/products/5`.
```csharp
[HttpGet("products/{productId}")] 
public IActionResult GetProduct(int productId) 
{ 
	// ... Fetch and return the product 
}
```

### Route Constraints

You can apply constraints to route parameters to restrict which URLs match a particular route.

Route constraints ensure that specific conditions are met for the route parameters. For instance, we can constrain the `productId` to be an integer.
```csharp
[HttpGet("products/{productId:int}")] 
public IActionResult GetProduct(int productId) 
{ 
	// ... Fetch and return the product 
}
```
This route would match `/products/5` but not `/products/five`.

### Attribute Routing

With attribute routing, you can specify routing directly on the controllers and actions. This provides a decentralized way of managing routes.
```csharp
[Route("api/[controller]")] 
public  class  OrdersController : ControllerBase 
{ 
	[HttpGet("{orderId}")] 
	public IActionResult GetOrder(int orderId) 
	{ 
		// ... Fetch and return the order
	} 
}
```
With this setup, an HTTP GET request to `/api/orders/123` would invoke the `GetOrder` method.
#### **Advantages:**

1.  **Granularity**: Allows for very precise and fine-grained control over URLs.
2.  **Clarity**: The route is defined directly on the action method, making it clear which URLs the method will handle.
3.  **Flexibility**: It's easier to create complex URL patterns and constraints that might be cumbersome or less intuitive with convention-based routing.
4.  **API Design**: Particularly helpful for designing RESTful APIs, where URLs often map directly to resources and HTTP verbs.

#### **When to Use:**

1.  When you want direct control over the URLs for specific actions.
2.  When building RESTful APIs with clear resource-oriented URLs.
3.  For endpoints that don't fit neatly into the controller-action-id pattern (e.g., `/products/{productId}/reviews/latest`).
4.  When dealing with versions in API (e.g., `/v1/products`, `/v2/products`).
### Convention-based Routing

Specifies routes via centralized route table.

Convention-based routing uses a centralized route configuration, often defined in the Program.cs.
```csharp
app.UseEndpoints(endpoints =>
{
   endpoints.MapControllerRoute(
       name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}");
});
```
For a controller named `HomeController` with an action `Details`, a URL like `/home/details/5` would be matched.
#### **Advantages:**

1.  **Consistency**: Establishes a consistent URL pattern across the application.
2.  **Simplicity**: For applications where the majority of routes fit the default or a few patterns, convention-based routing reduces repetition.
3.  **Centralized Management**: All routes are managed in one place, making it easier to see and manage all route patterns.
4.  **Less Verbose**: Reduces the need to decorate every action with a route attribute.

#### **When to Use:**

1.  When your application's routes mostly fit a consistent pattern, like `{controller}/{action}/{id}`.
2.  For larger applications where having a central overview of routing is beneficial.
3.  When you want to reduce the verbosity in controllers and actions and keep them focused on business logic.
4.  For simpler applications or traditional MVC apps where the default routing conventions suffice.

### Route Data

Data extracted from the route, accessible within the handling action.

Route data contains data parsed from the URL. This data can be accessed within the handling action. For example:

```csharp
[HttpGet("articles/{category}/{id:int}")]
public IActionResult GetArticle(string category, int id)
{
    var routeDataCategory = RouteData.Values["category"];  // This should be same as the 'category' parameter.
    // ... Fetch and return the article
}
```
#### Advanced Route Data Scenarios

1.  **Route Constraints**: Constraints can be applied to route parameters to specify the format of the data they can match.
    
    ```csharp
    [HttpGet("{id:int}")]
    public IActionResult GetProduct(int id)
    {
        // 'id' will be an integer value due to the constraint
    }
2.  **Optional Parameters**: You can define a route parameter as optional by following it with a `?`.
    
    ```csharp
    [HttpGet("{category}/{id?}")]
    public IActionResult GetProducts(string category, int? id = null)
    {
        // 'id' is optional in the URL
    }
3.  **Catch-all Parameters**: Using `*` or `**` allows capturing multiple segments of a URL. This is helpful in scenarios like building a file path from the URL.
    
    ```csharp
    [HttpGet("files/{*filePath}")]
    public IActionResult GetFile(string filePath)
    {
        // 'filePath' can represent multiple segments, e.g., "images/pic.jpg"
    }
    
### Route Order

The order in which routes are defined matters. ASP.NET Core will stop at the first matching route it encounters.

The order in which routes are defined is essential. The routing system stops at the first match. Consider two route definitions:

```csharp
[HttpGet("archives/{year}")]
public IActionResult Archives(int year) { /*...*/ }
    
[HttpGet("archives/latest")]
public IActionResult LatestArchives() { /*...*/ }
```

If "archives/latest" is accessed, it'll match the first route because `{year}` will take "latest" as a string. To fix this, either define the specific route (`archives/latest`) before the general one or use constraints.

---
### Text study material
[Exploring ASP.NET Routing in .NET Core 7](https://medium.com/@codezone/exploring-asp-net-routing-in-net-core-7-d98a56f9b863)

[Routing in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-7.0/)

### Video study material
[![Web API Routing in .NET 8 | Ep 7](https://img.youtube.com/vi/diHQzmQfh6E/0.jpg)](https://www.youtube.com/watch?v=diHQzmQfh6E)

[![ATTRIBUTE ROUTING in ASP NET Core | Getting Started With ASP.NET Core Series](https://img.youtube.com/vi/a76et6ImGU8/0.jpg)](https://www.youtube.com/watch?v=a76et6ImGU8)

---
### Practical Routing Example in ASP.NET Core

In the standard weather forecast project, we can demonstrate routing through a simple enhancement.

In your `WeatherForecastController`, add the following action:

```csharp
[HttpGet("{id}")]
public ActionResult<WeatherForecast> GetSpecificForecast(int id)
{
    if (id >= 0 && id < _forecasts.Length)
    {
        return _forecasts[id];
    }

    return NotFound($"No forecast found for ID: {id}");
}
 ```

This action fetches a specific forecast based on its ID. The `{id}` in the route template will be replaced by an actual value when a request is made.

Start your application and navigate to:

`https://localhost:5001/WeatherForecast/2` 

This should return the third weather forecast (0-indexed).

## Questions to Gauge Understanding

- What is the primary purpose of routing in ASP.NET Core?
- How do route templates work, and why are they useful?
- What's the difference between convention-based and attribute routing?
- How can you apply constraints to a route template?
- In what scenarios might route order be significant?

---
## Homework Assignment: Weather Forecast Enhancements

### Step 1: Basic Route Parameters

Modify the GET endpoint to take a `days` parameter from the route, which specifies the number of days for which forecasts should be returned (default to 5 if not provided).

Route: `/WeatherForecast/{days?}`

### Step 2: Route Constraints

Add a route constraint to ensure the `days` parameter is an integer between 1 and 10.

### Step 3: Query String Parameters

Allow users to filter forecasts by temperature range using query string parameters: `minTemp` and `maxTemp`.

Test with: `/WeatherForecast/5?minTemp=0&maxTemp=30`

### Step 4: Attribute Routing

Add a new endpoint that returns a weather summary for a specific day using attribute routing.

Route: `/WeatherForecast/summary/{date}` where `date` is in the format `yyyy-MM-dd`.

Tip: You can use regex for date format

### Step 5: Catch-all Parameter

Add an endpoint to simulate fetching weather data files. It should take a file path and return the name of the file.

Route: `/WeatherForecast/data/{*filePath}`
