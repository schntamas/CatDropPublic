# CatDown
## _Rest Web Api to turn cat images around and more..._

The source of cat images is https://cataas.com/#/. As test project the only functionalty is to return a random cat image unchanged or turn in upside down.
The architecture demonstrates a shell of an extendible Rest API. It's designed to support unit and automated testing. In a basic form it has the major building blocks of an enterprise application such as authentication, audit and error logging, custom exceptions. 


## Technology
- .Net Web API 2 running under .Net 4.5.2
- Swagger for API documentation
- Nlog for logging
- Unity for dependency injection
- Moq for unit testing

## Projects

### CatDrop.WebApi
    Web Api project, the head of the application. It exposes two endpoints:
    -Cat: Loads a random cat and rotates the cat images.
    -User: Adds new users.
    
    It uses basic authentication with WWW-Authenticate. Username and password has to be typed in first time. As demo application the token never expires.

### CatDrop.Services
    Layer containing the business logic.(User management, Image processing)
    
### CatDrop.Interfaces
    Interface definitions for CatDrop.Services project.
  
### CatDrop.Models
    Business models and custom exception. 
    
### CatDrop.Services.Test
    Unit test project for CatDrop.Services. Only one dummy test is written. It needs to be extended. 
    
## TODOs/Known issues

    -   Auth filter depencency injection is not working. The temporary inline service instantiation breaks the IoC concept.
    -   There is an issue with png image processing. This needs to be investigated and fixed.
    -   There is no separate Data Access Layer defined. The Rest API wrapper and User repository shoud not be part of the Business Logic layer.  

