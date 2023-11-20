
# Dependency Injection (DI) in ASP.NET Core
## Overview
Dependency Injection (DI) is a design pattern that allows an object to receive its dependencies from outside rather than creating them internally. ASP.NET Core has built-in support for DI, making it easier to manage dependencies, enhance testability, and promote a clean, modular architecture.
## Learning Objectives
- Understand the fundamental concept of Dependency Injection
- Extract and encapsulate business logic into services
- Implement and switch between different service implementations using DI
- Appreciate the Separation of Concerns principle in software design
## Prerequisites
- Basic knowledge of C#.
- Familiarity with ASP.NET Core basics.
## Study material:
### Text study material:
[.NET dependency injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection/)

[Dependency Injection In .NET Core](https://www.c-sharpcorner.com/article/dependency-injection-in-net-core/)

### Video study material:
[![What is Dependency Injection in .NET?](https://img.youtube.com/vi/KMErAbXRQdg/0.jpg)](https://www.youtube.com/watch?v=KMErAbXRQdg)
[![Dependency Injection for ASP NET Core 6.0 - An UPDATED 2022 Tutorial](https://img.youtube.com/vi/9J9a77ga9R0/0.jpg)](https://www.youtube.com/watch?v=9J9a77ga9R0)
## Homework: Implementing Dependency Injection in ASP.NET Core
### Objective
The purpose of this assignment is to demonstrate your understanding of Dependency Injection (DI) in ASP.NET Core. You will be tasked with creating different services and implementing them in a simple ASP.NET Core application.
### Requirements
1. **Set Up a New Project**: Start a new ASP.NET Core Web API project.
2. **Define New Entities**:
   - Create a simple `Product` class with properties: `Id`, `Name`, `Description`, and `Price`.
   - Create a `Customer` class with properties: `Id`, `Name`, and `Email`.
3. **Service Interfaces & Implementations**:
   - Create a `IDataService<T>` generic interface with methods for `Add`, `GetAll`, and `GetById`.
   - Implement two versions of the `IDataService<T>`:
     - `InMemoryDataService<T>`: This will store entities in a List in memory.
     - `MockedDataService<T>`: This will generate random mock data.
4. **Controllers**:
   - Create two controllers: `ProductsController` and `CustomersController`.
   - Each controller should have endpoints for:
     - Getting all entities.
     - Getting a single entity by Id.
     - Adding a new entity.
   - Utilize Dependency Injection to inject the appropriate `IDataService<T>` implementation into each controller.
5. **Configure Dependency Injection**:
   - In the `ConfigureServices` method in `Startup.cs`, configure the DI container to use the `InMemoryDataService<T>` for one of the controllers and `MockedDataService<T>` for the other.
6. **Testing**:
   - Test the endpoints using a tool like Postman or Swagger (if integrated).
   - Observe the different behaviors based on the service implementations.
### Bonus
1. **Switch Implementations**: Modify the `ConfigureServices` method to swap the service implementations for each controller. Observe the behavior change.
2. **Scoped Lifetimes**: Implement a counter in the `InMemoryDataService<T>` that increments with each call. Register this service with a scoped lifetime and observe its behavior. How does the count change with each new web request?
3. **Extend Functionality**: Add methods in the `IDataService<T>` for updating and deleting entities. Implement this functionality in the controllers and the two service implementations.
## Review & Questions
- What is Dependency Injection, and why is it beneficial?
- Explain the three service lifetimes in ASP.NET Core DI. Give an example use case for each.
- How do you register a service for DI in an ASP.NET Core application?
- How do you consume a registered service in a controller?
- How does DI help in unit testing?
