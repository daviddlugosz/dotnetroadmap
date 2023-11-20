

# Configuration in ASP.NET Core
## Overview
ASP.NET Core provides a rich configuration system. Applications can pull configuration data from various sources in a consistent way, allowing the environment to dictate how applications behave.
## Learning objectives
- Understand how configuration in ASP.NET Core works
- Know how to use various configuration providers
- Be familiar with secrets management
- Understand configuration precedence and how to reload configuration
- Be able to implement strongly typed configuration settings
## Prerequisites
-   Basic understanding of C# and .NET Core
-   Familiarity with ASP.NET Core basics
-   Knowledge of how to set up an ASP.NET Core web application
## Study materials
### Text study material
[Configuration in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration)
[Options Pattern in ASP.NET Core – Bind & Validate Configurations from appsettings.json](https://codewithmukesh.com/blog/options-pattern-in-aspnet-core/)
### Video study material
[![Appsettings.json in .NET: How to read and get a value](https://img.youtube.com/vi/UiqTDvIFJ3g/0.jpg)](https://www.youtube.com/watch?v=UiqTDvIFJ3g)
[![Manage Secrets in DotNet 6](https://img.youtube.com/vi/WgtEQCEgFVU/0.jpg)](https://www.youtube.com/watch?v=WgtEQCEgFVU)
## Homework: Weather Forecast Enhancements
### Step 1: Add Configuration to `appsettings.json`
-   Add a section called `WeatherSettings` to your `appsettings.json`.
-   Inside `WeatherSettings`, add the following properties:
    -   `SummaryOverride`: Set this to any string, e.g., "This is summary string from configuration".
    -   `ForecastCount`: Set this to 3.
-   Bind this configuration to object
### Step 2: Modify WeatherForecastRepository:
-   Update the `Get` method to:
    -   Read the `SummaryOverride` from the configuration and set it to Summary parameter of forecast object.
    -   Limit the number of forecast items returned based on the `ForecastCount` setting.
### Step 3: Override Configuration by `appsettings.Development.json`
-   Rewrite `ForecastCount` in `appsettings.Development.json` file to value 2
### Step 4: Override Configuration by secrets manager
-   Use secrets manager to override `SummaryOverride` to "This is summary string from configuration from secrets.json"
### Step 5: Override Configuration by environment variable
-   Override the `ForecastCount` using an environment variable to return 4 items
## Review & Questions
- How does ASP.NET Core decide which configuration source takes precedence when there are conflicts?
- How would you secure sensitive configuration data, especially in a production environment?
- What's the benefit of centralizing configurations in `appsettings.json` compared to hardcoding them?
