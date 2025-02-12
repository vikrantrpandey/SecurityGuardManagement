# Security Guard Management System

A comprehensive system for managing security guards, their assignments, training records, and employment history.

## Features

- Guard Management (CRUD operations)
- Employment History Tracking
- Training Records
- Guard Assignments
- Address Management

## Technology Stack

- .NET 9.0
- Entity Framework Core
- Clean Architecture
- CQRS Pattern with MediatR
- AutoMapper for object mapping

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

## Project Structure

- **SecurityGuardManagement.API**: Web API layer
- **SecurityGuardManagement.Application**: Application logic, commands, and queries
- **SecurityGuardManagement.Domain**: Domain entities and business logic
- **SecurityGuardManagement.Infrastructure**: Data access and external services
