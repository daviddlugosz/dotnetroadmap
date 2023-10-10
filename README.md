# ASP.NET Core: Understanding REST

## Prerequisites
Before diving into this section, the student should have:

Basic understanding of C# and .NET.
An idea of web development and HTTP.

## Learning Objectives
By the end of this section, the student should be able to:

Explain the principles of REST and its constraints.
Implement a basic CRUD RESTful API using ASP.NET Core.
Understand and work with different HTTP methods and status codes.
Define and work with standard RESTful routes.
Apply best practices in designing RESTful APIs.

## What is REST?
REST stands for Representational State Transfer. It's an architectural style for designing networked applications. APIs built using REST are referred to as RESTful APIs.

### Principles of REST
Statelessness: Every request from a client contains all the information needed to process the request.
Client-Server Architecture: The client and server are separate entities and can be developed independently.
Cacheability: Responses can be cached to improve performance.
Layered System: A client cannot ordinarily tell whether it is connected directly to the end server or to an intermediary.
Uniform Interface: Simplifies the architecture by using a standard way to communicate.
Code on Demand (optional): Servers can provide executable code to the client.

### Text study material:
[What Is A RESTful API?](https://aws.amazon.com/what-is/restful-api/)

[Build a RESTful Web API with ASP.NET Core 6](https://medium.com/net-core/build-a-restful-web-api-with-asp-net-core-6-30747197e229)

[REST API response codes and error messages](https://www.ibm.com/docs/en/odm/8.5.1?topic=api-rest-response-codes-error-messages)

[PUT vs POST - Comparing HTTP Methods](https://www.keycdn.com/support/put-vs-post)
### Video study material:
[![What is a REST API?](https://img.youtube.com/vi/lsMQRaeKNDk/0.jpg)](https://www.youtube.com/watch?v=lsMQRaeKNDk)
[![Build a RESTful API in ASP.NET 6.0 in 9 Steps!](https://img.youtube.com/vi/Tj3qsKSNvMk/0.jpg)](https://www.youtube.com/watch?v=Tj3qsKSNvMk)

## Implementing a RESTful API with ASP.NET Core

1. The Model and Static Data Store
For this example, let's work with a simple Book model:

```csharp
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
}
```

Within BooksController, create a static list that will act as our data store:
```csharp
private static List<Book> _books = new List<Book>
{
    new Book { Id = 1, Title = "Book 1", Author = "Author A", Year = 2001 },
    new Book { Id = 2, Title = "Book 2", Author = "Author B", Year = 2002 }
};
```
2. Implementing CRUD Actions

a. GET All Books

```csharp
[HttpGet]
public ActionResult<IEnumerable<Book>> GetBooks()
{
    return Ok(_books); // 200 OK
}
```

b. GET a Single Book by ID

```csharp
[HttpGet("{id}")]
public ActionResult<Book> GetBook(int id)
{
    var book = _books.FirstOrDefault(b => b.Id == id);
    if (book == null)
    {
        return NotFound(); // 404 Not Found
    }
    return Ok(book); // 200 OK
}
```

c. POST a New Book

```csharp
[HttpPost]
public ActionResult<Book> PostBook(Book book)
{
    var maxId = _books.Max(b => b.Id);
    book.Id = maxId + 1;
    _books.Add(book);
    return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book); // 201 Created
}
```

d. PUT to Update an Existing Book

```csharp
[HttpPut("{id}")]
public ActionResult PutBook(int id, Book updatedBook)
{
    var book = _books.FirstOrDefault(b => b.Id == id);
    if (book == null)
    {
        return NotFound(); // 404 Not Found
    }

    book.Title = updatedBook.Title;
    book.Author = updatedBook.Author;
    book.Year = updatedBook.Year;

    return Ok(book); // 200 OK
}
```

e. DELETE a Book

```csharp
[HttpDelete("{id}")]
public ActionResult DeleteBook(int id)
{
    var book = _books.FirstOrDefault(b => b.Id == id);
    if (book == null)
    {
        return NotFound(); // 404 Not Found
    }

    _books.Remove(book);
    return NoContent(); // 204 No Content
}
```

## Best Practices
1. Use HTTP status codes: Use standard HTTP status codes to indicate the success or failure of an API request.
2. Version your API: As your API evolves, you might need to introduce breaking changes. Versioning allows older apps to keep working.
3. Use nouns for resource names: For example, /books for a collection of books and /books/1 for a specific book.

## Questions to Gauge Understanding
1. What is the difference between a REST API and a traditional web service?
2. How does statelessness in REST impact scalability?
3. Why is versioning important in a RESTful API?
4. When would you use PUT vs POST in a RESTful API?
5. How do you handle errors or invalid requests in a REST API?

## Homework Assignment: Build a RESTful API for a Library System

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
