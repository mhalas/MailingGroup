# MailingGroup API
This project, is a sample of my knowledge and programming skills, and the main purpose is collecting email addresses in mailing group. Every user has his own mailing groups.

## Technology stack
1. ASP .NET Core 5.0
2. Microsoft SQL Server database
3. Entity Framework Core
4. NUnit with NSubstitute mocking framework
5. Inversion of Control (IoC)
6. MediatR
7. OAS - Swagger
8. JWT as a method of vuser verification

## API Methods
1. User
- Register(POST)
- Login(POST)

2. Mailing group
- Create (POST)
- Update (PUT)
- Retrieve (GET)
- Delete (DELETE)

3. Email address
- Create (POST)
- Update (PUT)
- Retrieve (GET)
- Delete (DELETE)

## POSTMAN request collection
There is also my exported collection of requests for POSTMAN, where can be found in 'Extras' folder, or [here](https://github.com/mhalas/MailingGroup/blob/master/Extras/Mailing%20Group%20Api.postman_collection.json)

## Database scheme
![Database scheme](https://github.com/mhalas/MailingGroup/blob/master/Extras/MailingGroupDatabaseScheme.png?raw=true)

## About solution:
The "MailingGroup" Solution has many projects, which every one of them has his own purpose of existence.
1. Api - This is main application written in .NET 5.0
2. Business - business logic e.g. handlers for endpoints(consumed by Api)
3. Contracts - mini project, shared to many other projects in solution
4. EF.SqlServer - communication with database by EFCore
5. Utilities - functionality which not suitable to other projects
6. Business.Tests and Utilities.Tests - Unit tests for written logic

## Future ideas
1. UI
2. Shared mailing groups with other users
