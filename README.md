# Sales API Project

This project implements a sales management API with product catalog functionality and tax calculation rules.

## Features

- Product management
- Sales processing with tax calculation
- Event-driven architecture
- RESTful API with standardized responses
- Gateway API for service orchestration

## Technology Stack

- .NET 8.0 with C#
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Ocelot Gateway
- Docker & Docker Compose

## Tax Calculation Rules

- 0% tax for sales with less than 4 identical items
- 10% tax (IVA) for sales with 4-9 identical items
- 20% tax (SPECIAL IVA) for sales with 10-20 identical items
- Maximum limit of 20 identical items per sale

## Project Structure

The solution follows Domain-Driven Design principles:

- **Domain Layer**: Contains entities, value objects, and domain logic
- **Application Layer**: Contains application services, DTOs, and business logic
- **Infrastructure Layer**: Contains repositories, database context, and external services
- **Presentation Layer**: Contains API controllers and request/response models
- **Gateway**: Provides a unified entry point for all services

## Getting Started

### Prerequisites

- Docker and Docker Compose
- .NET 8.0 SDK (for local development)

### Running the Application

1. Clone the repository
2. Navigate to the root directory
3. Run the application using Docker Compose:

```bash
docker-compose up -d
```

4. Access the API at http://localhost:7777

### API Endpoints

- `GET /products`: List all products
- `POST /products`: Create a new product
- `GET /sales`: List all sales
- `POST /sales`: Create a new sale
- `DELETE /sales/{id}`: Cancel a sale

For detailed API documentation, refer to the [API Documentation](./general-api.txt) file.

## Development

### Building Locally

```bash
dotnet build
```

### Running Tests

```bash
dotnet test
```

## License

This project is licensed under the MIT License - see the LICENSE file for details.
