
# # Authentication and JWT in ASP.NET Core

## Overview

Authentication is the process of determining the identity of a user or system, whereas authorization is the process of determining what that user or system can do. In this module, we'll be focusing on authentication using JWT (JSON Web Tokens) in ASP.NET Core.

## Learning objectives

- Explain the role of authentication and authorization in web applications.
- Describe what JWT is and how it can be used for stateless authentication.
- Implement JWT-based authentication in an ASP.NET Core API.
- Secure certain routes or methods to authenticated users.
- Validate and decode JWTs in a web application.

## Prerequisites

-   Basic understanding of C# and .NET Core.
-   Familiarity with ASP.NET Core basics.
-   Knowledge of how to set up an ASP.NET Core web application.


## Overview of JWT

A JSON Web Token (JWT) is a compact, URL-safe means of representing claims to be transferred between two parties. The claims in a JWT are encoded as a JSON object that is used as the payload of a JSON Web Signature (JWS) structure or as the plaintext of a JSON Web Encryption (JWE) structure.

#### Structure of a JWT

A JWT typically consists of three parts:

1.  **Header**: The header typically consists of two parts: the type of the token, which is JWT, and the signing algorithm.
2.  **Payload**: The payload contains the claims. Claims are statements about an entity (typically, the user) and additional data.
3.  **Signature**: To create the signature, you have to take the encoded header, the encoded payload, a secret, and the algorithm specified in the header and sign that.

A JWT typically looks like this:

Copy code

`eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c` 

This token is divided into three parts: Header, Payload, and Signature, separated by periods (`.`).

**Header**

The header typically consists of two parts: the type of the token, which is JWT, and the signing algorithm being used, such as HMAC SHA256 or RSA.

For our example:

```json
{
  "alg": "HS256",
  "typ": "JWT"
}
``` 

Base64Url encoding this header gives: `eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9`.

**Payload**

The payload (or claim) contains the actual data that's about the user or any other additional data.

For our example:

```json
{
  "sub": "1234567890",
  "name": "John Doe",
  "iat": 1516239022
}
```  

-   `sub`: Subject (usually the user ID)
-   `name`: Name of the user
-   `iat`: Issued At (timestamp of when the token was created)

Base64Url encoding this payload gives: `eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ`.

**Signature**

To create the signature, you have to take the encoded header, the encoded payload, a secret, and sign that using the algorithm specified in the header.

Signature is created from:
```scss
HMACSHA256(
  base64UrlEncode(header) + "." + base64UrlEncode(payload),
  your-256-bit-secret
)
```

This gives the signature: `SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c`.

**Full JWT**

So, combining the encoded header, payload, and signature gives us our complete JWT:

`eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
`

#### Benefits of JWT

1.  **Compact**: Can be sent via URL, POST parameter, or inside HTTP header.
2.  **Self-contained**: Contains all the required information about the user, preventing the need to query the database more than once.

## Practical Exercise: Weather Forecast Enhancements
For this practical exercise, we'll be implementing JWT authentication in the ASP.NET Core Weather Forecast example.

### Step-by-Step Implementation

1.  **Set Up ASP.NET Core API Project**

	Start with the default Weather Forecast API project.
    
3.  **Install Required Packages**
    
    `dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer` 
    
4.  **Update the `Program.cs`**

	add the authentication services
    
	   ```csharp
	   services
		    .AddAuthentication(opt =>
		    {
		        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		    })
		    .AddJwtBearer(options =>
		    {
		        options.TokenValidationParameters = new TokenValidationParameters
		        {
		            ValidateIssuerSigningKey = true,
		            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("YOUR_SECRET_KEY")),
		            ValidateIssuer = false,
		            ValidateAudience = false
		        };
		    });
  
5.  **Add `[Authorize]` Attribute** 

	On any controller or action method you wish to secure:
    
    ```csharp
    [Authorize]
    [HttpGet]
    public IEnumerable<WeatherForecast> Get() { ... }` 
    
6.  **Generate Tokens** 

	Create an endpoint to generate tokens for authenticated users. Ensure you sign the token with the same secret key used above.

	For this we will create new Controller `TokenController`

	```csharp
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.IdentityModel.Tokens;
	using System;
	using System.IdentityModel.Tokens.Jwt;
	using System.Security.Claims;
	using System.Text;
	
	[Route("api/[controller]")]
	[ApiController]
	public class TokenController : ControllerBase
	{
	    private const string SecretKey = "YOUR_SECRET_KEY"; // This should be moved to a secure configuration ideally
	    private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

	    [HttpGet]
	    public IActionResult Get()
	    {
	        var token = GenerateToken();

	        if (!string.IsNullOrEmpty(token))
	        {
	            return Ok(new { Token = token });
	        }
	        else
	        {
	            return BadRequest("Could not generate token");
	        }
	    }

	    private string GenerateToken()
	    {
	        var tokenHandler = new JwtSecurityTokenHandler();
	        var claims = new ClaimsIdentity(new[]
	        {
	            new Claim(ClaimTypes.Name, "UserName"),
	            // Add other claims as needed
	        });

	        var tokenDescriptor = new SecurityTokenDescriptor
	        {
	            Subject = claims,
	            Expires = DateTime.UtcNow.AddHours(1), // token expiration, adjust to your needs
	            SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256)
	        };

	        var token = tokenHandler.CreateToken(tokenDescriptor);
	        return tokenHandler.WriteToken(token);
	    }
	}
	```
    
	  This is a simplified example. In a real-world scenario:
	-   You'd likely include user-specific claims in the token after validating their credentials.
	-   The secret key should be stored securely, such as using the ASP.NET Core's secret manager or Azure Key Vault, rather than being hard-coded.
	-   The generation of a token is typically tied to an authentication process, so you'd have some logic that verifies user credentials before creating and returning a token.
7.  **Test the Application** 

	First, retrieve a token from the token generation endpoint and then use it to authenticate to access the Weather Forecast data.

### Conclusion & Insights

This hands-on experience has not only enhanced your understanding of token-based authentication but also underscored its importance in building secure applications. With this foundation, you're better equipped to tackle more advanced security challenges and ensure the safety of your future projects.

## Review & Questions
Explain the understanding of Authentication with JWT during code review session on your practical example.

To ensure your understanding, you should be able to answer the following:

- What is the difference between authentication and authorization?
- Explain the structure of a JWT. What are its main components?
- Why is the signature part of the JWT important?
- How can you secure certain routes or methods in ASP.NET Core to only authenticated users?
- How does the server validate the JWT?


TODO:
-   study materials (links, videos)
-   refinement of excerise (make it as task for junior to do on his own, not just copy paste)
-   test code in this file
-   implmentation with Identity server? Registration of users etc.? 