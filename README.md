# Kochk

Kochk is an ASP.NET Core 9 Web API for an e-commerce style domain with authentication, products, carts, orders, and user address management. The solution follows a layered architecture with separate `API`, `Application`, `Domain`, and `Infrastructure` projects.

## Solution Layout

```text
src/
  Kochk.API/             HTTP host, controllers, middleware, mapping, config
  Kochk.Application/     use cases, MediatR handlers, DTOs, specifications
  Kochk.Domain/          entities, enums, domain contracts
  Kochk.Infrastructure/  EF Core, Identity, repositories, Redis, JWT services
```

## Tech Stack

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core with PostgreSQL (`Npgsql`)
- ASP.NET Core Identity
- JWT bearer authentication
- Redis via `StackExchange.Redis`
- MediatR
- AutoMapper
- Scalar/OpenAPI

## Architecture

- `Kochk.Domain` contains the core business model such as products, carts, vendors, reviews, orders, and identity-related entities.
- `Kochk.Application` contains feature handlers, DTOs, specifications, service contracts, and error/result models.
- `Kochk.Infrastructure` implements persistence, repository access, token generation, current-user resolution, and identity/data seeding.
- `Kochk.API` wires the application together and exposes HTTP endpoints.

## Prerequisites

- .NET SDK `9.0.100` or compatible feature band
- A PostgreSQL database
- A Redis instance

The repository includes a local tool manifest for `dotnet-ef` in `.config/dotnet-tools.json`.

## Configuration

The API reads configuration from `src/Kochk.API/appsettings.json` and environment-specific overrides.

Key settings:

- `ConnectionStrings:DefaultConnection`
- `redis:endpoint`
- `redis:password`
- `JWT:SecurityKey`
- `JWT:Audience`
- `JWT:Issuer`
- `JWT:DurationInDays`
- `MediatRLicenseKey`

For local development, prefer overriding these values with environment variables or user secrets instead of committing real credentials.

## Running Locally

Restore tools and packages:

```bash
dotnet tool restore
dotnet restore Kochk.slnx
```

Build the solution:

```bash
dotnet build Kochk.slnx
```

Run the API:

```bash
dotnet run --project src/Kochk.API/Kochk.API.csproj
```

In development, OpenAPI and Scalar are enabled by `Program.cs`.

## Data and Identity Seeding

- Product brands, categories, products, and delivery methods have JSON seed files under `src/Kochk.Infrastructure/Data/DataSeed/`.
- Role seeding is enabled in `Program.cs` through `IdentityDataSeed.SeedRolesAsync(...)`.
- Database seeding and migrations are currently present but commented out in `src/Kochk.API/Program.cs`.

## Current API Surface

Base route prefix: `api/[controller]`

Implemented controllers currently include:

- `AccountController`
  - `POST /api/account/signin`
  - `POST /api/account/signup/vendor`
  - `POST /api/account/signup/customer`
  - `GET /api/account/CuerrentUser`
  - `GET /api/account/UserAddress`
  - `PUT /api/account/Address/{addressId}`
  - `GET /api/account/EmailExist?email=...`
- `ProductController`
  - `GET /api/product`
  - `GET /api/product/{id}`
- `CartController`
  - `DELETE /api/cart/{id}`

`OrderController` exists in the codebase but its actions are currently commented out.

## Notes

- Static product assets are served from `src/Kochk.API/wwwroot/`.
- Global exception handling is implemented through `ExceptionMiddleware`.
- Authentication is configured with JWT bearer tokens and custom `401` and `403` JSON responses.
- The current codebase builds successfully, but there are existing warnings that should be cleaned up separately.

## Development Status

This repository is structured as a clean architecture backend foundation and already contains:

- core domain entities
- application-level commands, queries, DTOs, and specifications
- infrastructure wiring for EF Core, Identity, JWT, and Redis
- initial API controllers and middleware

The project still needs normal hardening work such as migrations flow, endpoint completion, validation cleanup, and production-safe secret management.
