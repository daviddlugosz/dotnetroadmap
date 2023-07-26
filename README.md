# dotnetroadmap
# ASP.NET Core - Processing Pipeline / Middleware / Filters / Attributes
### Text study material:
[ASP.NET Core Request Processing Pipeline](https://dotnettutorials.net/lesson/asp-net-core-request-processing-pipeline/)

[Write custom ASP.NET Core middleware](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-7.0)

[Implementing Action Filters in ASP.NET Core](https://code-maze.com/action-filters-aspnetcore/)

### Video study material:
[![ASP.NET Core Understanding the middleware pipeline, Startup configuration - Application Life Cycle](https://img.youtube.com/vi/2SRUc7zZiyw/0.jpg)](https://www.youtube.com/watch?v=2SRUc7zZiyw)
[![MIDDLEWARE in ASP.NET Core | Getting Started With ASP.NET Core Series](https://img.youtube.com/vi/5eifH7LEnGo/0.jpg)](https://www.youtube.com/watch?v=5eifH7LEnGo)
[![FILTERS In ASP NET Core | Getting Started With ASP.NET Core Series](https://img.youtube.com/vi/mKM6FbxMGI8/0.jpg)](https://www.youtube.com/watch?v=mKM6FbxMGI8)
## Assignments:
### 1.	1.	Add Get endpoint that will take int numberOfForecast parameter and return number of forecasts based on number in received parameter
### 1.  2.	Create Middleware that will validate numberOfForecast parameter and returns HTTP Status code Forbiden if its < 1
### 2.  1.	Create endpoint that always returns Exception to easily mock Exceptions
### 2.  2.	Create Middleware so we get HTTP Status code for Bad Request and JSON object with properties ErrorMessage and StackTrace populated from the exception whenever one is thrown
### 3.	Create filter and apply it via attribute to endpoint from point 1 that will double the value of the parameter and validate that passed in (not doubled) parameter is not bigger than 10, if it is throw exception
