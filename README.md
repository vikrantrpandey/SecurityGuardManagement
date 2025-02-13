# Security Guard Management System

A comprehensive system for managing security guards, their assignments, training records, and employment history.

## Project Status

### Completed Features
- Project structure and architecture setup
- Domain layer implementation
  - Base entities and abstractions
  - Guard-related entities and value objects
  - Enums and common types
- Initial database migration
- Basic CRUD operations for Guards
- Git repository setup with Phase1 branch

### In Progress
- Application layer service interfaces
- Additional controllers implementation
- Validation logic
- Testing setup
- Documentation

## Technology Stack

- .NET 9.0
- Entity Framework Core
- Clean Architecture
- CQRS Pattern with MediatR
- AutoMapper for object mapping

## Project Structure

- **SecurityGuardManagement.API**: Web API layer
- **SecurityGuardManagement.Application**: Application logic, commands, and queries
- **SecurityGuardManagement.Domain**: Domain entities and business logic
- **SecurityGuardManagement.Infrastructure**: Data access and external services

## Getting Started

1. Clone the repository
2. Ensure you have .NET 9.0 SDK installed
3. Update the connection string in `appsettings.json`
4. Run database migrations:
   ```
   dotnet ef database update
   ```
5. Run the application:
   ```
   dotnet run --project src/SecurityGuardManagement.API/SecurityGuardManagement.API.csproj
   ```

## Development Branches

- `master`: Main stable branch
- `Phase1`: Current development branch with initial implementation

## Next Steps
- Complete remaining service interfaces
- Implement additional controllers
- Add validation using FluentValidation
- Set up testing infrastructure
- Enhance error handling and logging
