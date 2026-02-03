# Product Management API

A .NET 10 Web API for managing products, built with Clean Architecture, CQRS, and JWT Authentication.

## Prerequisites

Before running the application, ensure you have the following installed:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Redis](https://redis.io/download/)

## Installation

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/muratkck/ProductManagementPhase2
    cd ProductManagementPhase2/ProductManagement
    ```

2.  **Configure Secrets:**
    It is recommended to use **User Secrets** for sensitive data (Connection Strings, Redis, JWT Secret) during development.

    Run the following commands in the `ProductManagement.API` directory:

    ```bash
    dotnet user-secrets init
    dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=ProductDb;Username=postgres;Password=yourpassword"
    dotnet user-secrets set "Redis:Configuration" "localhost:6379"
    dotnet user-secrets set "Jwt:SecretKey" "your_super_secret_key_should_be_long"
    dotnet user-secrets set "Jwt:Issuer" "ProductManagement"
    dotnet user-secrets set "Jwt:Audience" "ProductManagementUser"
    ```

3.  **Apply Migrations:**
    Initialize the database by running EF Core migrations.
    ```bash
    cd ProductManagement.API
    dotnet ef database update
    ```

## How to Run

1.  **Start Redis:**
    Ensure your Redis server is running.

2.  **Run the API:**
    ```bash
    cd ProductManagement.API
    dotnet run
    ```

    The API will start at:
    - HTTPS: `https://localhost:7157`
    - HTTP: `http://localhost:5105`

## API Endpoints

You can explore and test the API using Swagger UI:
- **URL:** `https://localhost:7157/swagger`

### Auth Controller
- `POST /api/auth/register`: Register a new user.
- `POST /api/auth/login`: Login to receive a JWT token.

### Products Controller (Requires Authentication)
- `GET /api/products`: Retrieve all products.
- `GET /api/products/{id}`: Retrieve a specific product by ID.
- `POST /api/products`: Create a new product.
- `PUT /api/products/{id}`: Update an existing product.
- `DELETE /api/products/{id}`: Delete a product.

## Architecture

This project follows **Clean Architecture** principles and uses **CQRS** (Command Query Responsibility Segregation).

- **Domain**: Core entities and business rules.
- **Application**: Business logic, commands, queries, and interfaces (uses MediatR).
- **Infrastructure**: External concerns like Database (EF Core), Caching (Redis), and JWT generation.
- **API**: The entry point (Controllers) and configuration.

### Key Technologies
- **.NET 10**
- **Entity Framework Core** (PostgreSQL)
- **Redis** (Caching)
- **MediatR** (CQRS)
- **JWT** (Authentication)
- **Serilog/ILogger** (Logging)
