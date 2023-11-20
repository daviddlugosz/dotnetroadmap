
# ASP.NET Core: Understanding REST
## Overview
REST (Representational State Transfer) is an architectural style widely used for web development. It emphasizes stateless communication and CRUD (Create, Read, Update, Delete) operations using standard HTTP methods such as GET, POST, PUT, and DELETE. RESTful services are designed to be scalable, performant, and easily consumable by different clients, including browsers and mobile devices. Key principles include using resource-based URLs, stateless operations, and the ability to handle various data formats like JSON or XML. This approach leads to APIs that are intuitive to understand and integrate, promoting a more flexible and efficient way to build web services.
## Learning Objectives
- Explain the principles of REST and its constraints
- Implement a basic CRUD RESTful API using ASP.NET Core
- Understand and work with different HTTP methods and status codes
- Define and work with standard RESTful routes
- Apply best practices in designing RESTful APIs
## Prerequisites
- Basic understanding of C# and .NET
- An idea of web development and HTTP
## Study material
### Udemy course: 
[The Complete Guide To Build Rest Api's with Asp.Net and C#](https://ciklum.udemy.com/course/restapi-in-aspnet/)
### Text study material:
[What Is A RESTful API?](https://aws.amazon.com/what-is/restful-api/)

[Build a RESTful Web API with ASP.NET Core 6](https://medium.com/net-core/build-a-restful-web-api-with-asp-net-core-6-30747197e229)

[REST API response codes and error messages](https://www.ibm.com/docs/en/odm/8.5.1?topic=api-rest-response-codes-error-messages)

[PUT vs POST - Comparing HTTP Methods](https://www.keycdn.com/support/put-vs-post)
### Video study material:
[![What is a REST API?](https://img.youtube.com/vi/lsMQRaeKNDk/0.jpg)](https://www.youtube.com/watch?v=lsMQRaeKNDk)
[![Build a RESTful API in ASP.NET 6.0 in 9 Steps!](https://img.youtube.com/vi/Tj3qsKSNvMk/0.jpg)](https://www.youtube.com/watch?v=Tj3qsKSNvMk)
## Homework: Build a RESTful API for a Library System
### Objective:
Create a RESTful API using ASP.NET Core for a simple library management system that covers book and library user operations.

### Requirements:

1. **Models**:

**Book**: 
 - Id 
 - Title 
 - Author 
 - Year 
 - ISBN 
 - BorrowedByUserId (nullable)

**LibraryUser**: 
 - Id 
 - Name 
 - Email 
 - DateJoined 
 - BorrowedBooks (list of borrowed book Ids)

2. **Controllers**:

**BooksController**:
 - `GET` all books
 - `GET` a single book by its Id
 - `POST` a new book
 - `PUT` to update an existing book
 - `DELETE` a book by its Id
 - Extra: `POST` to mark a book as borrowed and by whom
 
**LibraryUsersController**:
 - `GET` all library users
 - `GET` a library user by their Id
 - `POST` a new library user
 - `PUT` to update an existing library user
 - `DELETE` a library user by their Id
 - Extra: `GET` all books borrowed by a library user

3. **Status Codes**: Ensure that each action properly returns the right HTTP status code, as mentioned in the course material.

4. **Data Store**: Use a static collection for both books and library users.

5. **Testing**: Test your API endpoints using a tool like Postman or Swagger.
## Review & Questions
1. What is the difference between a REST API and a traditional web service?
2. How does statelessness in REST impact scalability?
3. Why is versioning important in a RESTful API?
4. When would you use PUT vs POST in a RESTful API?
5. How do you handle errors or invalid requests in a REST API?
