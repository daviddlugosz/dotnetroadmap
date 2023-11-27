
# Routing in ASP.NET Core
## Overview
In ASP.NET Core, routing serves as a mechanism to map incoming HTTP requests to respective route handlers (typically controllers and their actions). It plays an integral role in web applications by determining how URLs should be matched and which code should be executed.
## Learning objectives
-   Understand how ASP.NET Core routes HTTP requests to the correct controller actions.
-   Distinguish between attribute routing and convention-based routing, and know when to use each.
-   Implement advanced routing techniques, such as route constraints and route data extraction.
## Prerequisites
-   Basic knowledge of C#
-   Familiarity with the ASP.NET Core web framework
-   Understanding of RESTful principles
## Study materials
### Text study material
[Exploring ASP.NET Routing in .NET Core 7](https://medium.com/@codezone/exploring-asp-net-routing-in-net-core-7-d98a56f9b863)

[Routing in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-7.0/)

### Video study material
[![Web API Routing in .NET 8 | Ep 7](https://camo.githubusercontent.com/da02489b044e4949c0c06fc6d364f4e3826bbb23ff8aa456bc66ac228267a601/68747470733a2f2f696d672e796f75747562652e636f6d2f76692f646948517a6d51666836452f302e6a7067)](https://www.youtube.com/watch?v=diHQzmQfh6E)

[![ATTRIBUTE ROUTING in ASP NET Core | Getting Started With ASP.NET Core Series](https://camo.githubusercontent.com/587d38ea136f4db32730440b37fa01178b5c3509690b0fd149f58cdc10c2a071/68747470733a2f2f696d672e796f75747562652e636f6d2f76692f613736657436496d4755382f302e6a7067)](https://www.youtube.com/watch?v=a76et6ImGU8)

## Homework: Weather Forecast Enhancements

### Step 1: Basic Route Parameters
Modify the GET endpoint to take a  `days`  parameter from the route, which specifies the number of days for which forecasts should be returned (default to 5 if not provided).

Route:  `/WeatherForecast/{days?}`
### Step 2: Route Constraints
Add a route constraint to ensure the  `days`  parameter is an integer between 1 and 10.

### Step 3: Query String Parameters
Allow users to filter forecasts by temperature range using query string parameters:  `minTemp`  and  `maxTemp`.

Test with:  `/WeatherForecast/5?minTemp=0&maxTemp=30`
### Step 4: Attribute Routing
Add a new endpoint that returns a weather summary for a specific day using attribute routing.

Route:  `/WeatherForecast/summary/{date}`  where  `date`  is in the format  `yyyy-MM-dd`.

Tip: You can use regex for date format
### Step 5: Catch-all Parameter
Add an endpoint to simulate fetching weather data files. It should take a file path and return the name of the file.

Route:  `/WeatherForecast/data/{*filePath}`
## Review & Questions
-   What is the primary purpose of routing in ASP.NET Core?
-   How do route templates work, and why are they useful?
-   What's the difference between convention-based and attribute routing?
-   How can you apply constraints to a route template?
-   In what scenarios might route order be significant?
