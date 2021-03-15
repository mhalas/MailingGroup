# MailingGroup API
This repository, is a sample of my knowledge and programming skills and the main purpose is collecting email addresses in mailing group. Every user has his own mailing groups.

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
- POST      /api/User/register
- POST      /api/User/login

2. Mailing group
- POST      /api/MailingGroup
- PUT       /api/MailingGroup/:mailingGroupId
- DELETE    /api/MailingGroup/:mailingGroupId
- GET       /api/MailingGroup

3. Email address
- POST      /api/EmailAddress
- PUT       /api/EmailAddress/:emailAddressId
- DELETE    /api/EmailAddress/:emailAddressId
- GET       /api/EmailAddress
- GET       /api/EmailAddress?mailingGroupId={mailingGroupId}

## Postman request collection
I've attached exported collection of requests for Postman which can be found in 'Extras' folder ( [here](https://github.com/mhalas/MailingGroup/blob/master/Extras/Mailing%20Group%20Api.postman_collection.json) )

## Database scheme
![Database scheme](https://github.com/mhalas/MailingGroup/blob/master/Extras/MailingGroupDatabaseScheme.png?raw=true)

## About solution:
The "MailingGroup" Solution consists of multiple projects, its with his own purpose.
1. Api - This is the main application written in .NET 5.0
2. Business - business logic like handlers for endpoints(consumed by the Api)
3. Contracts - interfaces for other elements in solution
4. EF.SqlServer - database communication via EFCore
5. Utilities - a set of utility functions
6. Business.Tests and Utilities.Tests - Unit tests

## Future ideas
1. UI
2. Shared mailing groups with other users
