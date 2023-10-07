
# Configuration in ASP.NET Core

## Overview

ASP.NET Core provides a rich configuration system. Applications can pull configuration data from various sources in a consistent way, allowing the environment to dictate how applications behave.

## Learning objectives

- Understand how configuration in ASP.NET Core works.
- Know how to use various configuration providers.
- Be familiar with secrets management.
- Understand configuration precedence and how to reload configuration.
- Be able to implement strongly typed configuration settings.

## Prerequisites

-   Basic understanding of C# and .NET Core.
-   Familiarity with ASP.NET Core basics.
-   Knowledge of how to set up an ASP.NET Core web application.

## Core Concepts of Configuration


### Basics

ASP.NET Core uses a set of providers that read configuration key-pairs from different sources. The built-in configuration providers are:

-   AppSettings (JSON, XML, INI)
-   Environment Variables
-   Command-line Arguments
-   User Secrets
-   Azure Key Vault
-   Custom Providers

The configuration data is presented as a key-value pair.

### appsettings.json

The `appsettings.json` file provides a default configuration for ASP.NET Core applications. It's a JSON-formatted file and can contain configuration settings for the application, libraries, and frameworks.

Provider for this configuration is implicitly setup in ASP.NET projects and you don't need to add it yourself.

#### Environment-Specific Overrides

ASP.NET Core supports environment-specific configuration files like `appsettings.Development.json`, `appsettings.Staging.json`, etc. These files can override or add to the settings in `appsettings.json` based on the current environment.

Any settings in the environment-specific file will override the same settings in the main `appsettings.json` file. If a setting exists only in the environment-specific file, it will be added to the configuration.

If the `ASPNETCORE_ENVIRONMENT` variable isn't set, ASP.NET Core defaults to `Production`. This is a safety feature to avoid accidentally leaking sensitive development-time data.

### Environment Variables

Environment variables are key-value pairs that can be accessed by the operating system and any applications running on it. They're especially popular for configuring cloud-based and containerized applications because they allow the environment to dictate application behavior without code changes.

**Setting in Different Environments**

-   Windows: `setx MySetting "MyValue"`
-   Linux/macOS: `export MySetting=MyValue`

**Adding the Provider**
The environment variables provider is added with the `AddCommandLine` method in `Program.cs` file:

```csharp
var configBuilder = new ConfigurationBuilder()
	.AddEnvironmentVariables();
	
configBuilder.Build();
```

**Prefix Filtering**
You can specify a prefix to filter environment variables:

```csharp
.AddEnvironmentVariables("MYAPP_")
```

With the above, only environment variables starting with `MYAPP_` would be imported.

**Accessing Environment Variables**
Once loaded into the configuration, they're accessed like any other configuration setting:

```csharp
string mySetting = Configuration["MYAPP_TestConfig"];
```

### Command-line Arguments

Command-line arguments allow configuration data to be passed directly when starting an application.

**Adding the Provider**
The environment variables provider is added with the `AddCommandLine` method in `Program.cs` file:

```csharp
var configBuilder = new ConfigurationBuilder()
	.AddCommandLine(args);
	
configBuilder.Build();
```

**Argument Format**

By default, arguments should be in the format `--key value` or `--key=value`.

**Mapping to Configuration**

Command-line arguments can be mapped directly to configuration keys:

```
dotnet run --urls "http://localhost:5001"
```

**Using a Switch Mapping**

You can also use a switch mapping to support alternative formats:

```csharp
var switchMapping = new Dictionary<string, string>
{
    { "-u", "urls" }
};

var configBuilder = new ConfigurationBuilder()
    .AddCommandLine(args, switchMapping);

configBuilder.Build();
```
Now you can use `-u` as a shortcut for `--urls`:

```
dotnet run -u "http://localhost:5001"
```

### User Secrets

For sensitive data like connection strings, API keys, etc., you should avoid storing them directly in the code or configuration files. Use the **Secret Manager**:

1.  Enable secret storage by CLI command `dotnet  user-secrets init`
2.  Use the `secrets.json` file for local development which you can access it in Visual Studio by right clicking project and selecting **Manage User Secrets**

`secrets.json` file is unlike `appsettings.json` file included in `.gitignore` default file for .NET solutions and is not used when running as Release.

Remember, this is **only for development**. For production, consider secure solutions like Azure Key Vault.

### Azure Key Vault
Azure Key Vault is a cloud service provided by Microsoft Azure that allows you to securely store and manage sensitive information such as secrets, encryption keys, and certificates. Integration with ASP.NET Core offers a way to fetch configuration values directly from Azure Key Vault, providing a centralized and secure mechanism for app configuration.

###  Strongly Typed Configuration


Instead of fetching individual values using string keys, you can map your configuration to C# classes.

1. Add your configuration to `appsettings.json`
```json
"MySection": {
	"MySetting": "myCustomSettingString"
}
```
2. Create a class representing the configuration:
```csharp
public class MyConfig
{
    public string MySetting { get; set; }
}
```

3. In `Program.cs`, bind the configuration to your class:

```csharp
services.Configure<MyConfig>(Configuration.GetSection("MySection"));
```

4. Inject `IOptions<MyConfig>` to access the configuration:

```csharp
public MyClass(IOptions<MyConfig> config)
{
    var mySetting = config.Value.MySetting;
}
```

### Configuration Precedence

If a configuration key exists in multiple providers, the last provider wins. The default order is:

1.  `appsettings.json`
2.  `appsettings.{Environment}.json`
3.  User secrets (development only)
4.  Environment variables
5.  Command-line arguments

---
### Practical Configuration Example in ASP.NET Core

#### Step 1: Set Up a New ASP.NET Core Web API

Create a new ASP.NET Core Web API project

`dotnet new webapi -n ConfigurationExample`

or use already provided project in this feature brach of repository.
    
#### Step 2: Configure `appsettings.json`

Add the following to the `appsettings.json`:


```json
  "AppSettings": {
    "ApplicationName": "ConfigurationExample",
    "Version": "1.0.0"
  }
``` 

#### Step 3: Access Configuration in the Controller

Modify the default `WeatherForecastController`:

1.  Inject the `IConfiguration` interface to access configuration settings:
    
	```csharp
	private readonly IConfiguration _configuration;
    
	public WeatherForecastController(IConfiguration configuration)
	{
	    _configuration = configuration;
	}
	```
    
2.  Add a new API endpoint to return the app settings:
    
	```csharp
    [HttpGet("app-info")]
    public ActionResult GetAppInfo()
    {
        var appName = _configuration["AppSettings:ApplicationName"];
        var version = _configuration["AppSettings:Version"];
    
        return Ok(new { ApplicationName = appName, Version = version });
    }
    ``` 
   
#### Step 4: Override Configuration Using Environment Variables

For demonstration purposes, you can override configuration using environment variables:

1.  On **Windows**:
    `setx AppSettings__Version "2.0.0"` 
    
2.  On **Linux/macOS**:
    `export AppSettings__Version=2.0.0` 
    

#### Step 5: Further Override Configuration Using Command-line Arguments

Run the application, passing in a command-line argument:

`dotnet run --AppSettings:ApplicationName="CommandLineConfigApp"` 

#### Step 6: Test the Configuration
Using a tool like Postman or your browser, navigate to:

`http://localhost:5000/weatherforecast/app-info` 

You should see the `ApplicationName` as `CommandLineConfigApp` (from the command-line argument), and the `Version` as `2.0.0` (from the environment variable).

## Practical Exercise: Weather Forecast Enhancements


**Add Configuration to `appsettings.json`**:

-   Add a section called `WeatherSettings` to your `appsettings.json`.
    
-   Inside `WeatherSettings`, add the following properties:
    
    -   `DefaultCity`: Set this to any city name, e.g., "Seattle".
    -   `ForecastCount`: Set this to 5.
    
    ```json
    "WeatherSettings": {
        "DefaultCity": "Seattle",
        "ForecastCount": 5
    }
    ```

**Modify WeatherForecastController**:

-   Inject `IConfiguration` into your controller.
-   Update the `Get` method to:
    -   Read the `DefaultCity` from the configuration and attach it to each weather forecast item.
    -   Limit the number of forecast items returned based on the `ForecastCount` setting.

**Test Your Changes**:

-   Run your application.
-   Use a tool (like Postman or a browser) to access the `/WeatherForecast` endpoint. Ensure the city is set correctly and the number of items matches the `ForecastCount`.

**Override Configuration**:

-   Override the `ForecastCount` using an environment variable to return a different number of items.
    -   For Windows: `setx WeatherSettings__ForecastCount "3"`
    -   For Linux/macOS: `export WeatherSettings__ForecastCount=3`
-   Restart your application and access the `/WeatherForecast` endpoint again. Ensure it now returns the overridden count of items.

### Conclusion & Insights

This hands-on task highlights the importance of decoupling behavior from code, understanding configuration precedence, and the adaptability it brings to applications. This foundational knowledge is crucial for building scalable and maintainable real-world ASP.NET Core applications.

## Review & Questions
Explain the understanding of Configuration during code review session on your practical example.

To ensure your understanding, you should be able to answer the following:

- How does ASP.NET Core decide which configuration source takes precedence when there are conflicts?
- How would you secure sensitive configuration data, especially in a production environment?
- What's the benefit of centralizing configurations in `appsettings.json` compared to hardcoding them?


TODO:
-   study materials (links, videos)
-   refinement of excerise (make it as task for junior to do on his own, not just copy paste)
-   test code in this file, not sure you need to register providers...