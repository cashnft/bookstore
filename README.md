# Online Bookstore Application


## Table of Contents
- [Architecture Overview](#architecture-overview)
- [Technology Stack](#technology-stack)
- [Database Design](#database-design)
- [Caching Strategy](#caching-strategy)
- [Setup Instructions](#setup-instructions)
- [API Endpoints](#api-endpoints)

## Architecture Overview

The application follows a clean architecture pattern with four main layers:
- **Domain**: Contains entities, interfaces, and business rules
- **Application**: Houses business logic and service implementations
- **Infrastructure**: Implements data access and external service integrations
- **API**: Handles HTTP requests and responses

## Technology Stack

- **Backend Framework**: .NET 8
- **ORM**: Entity Framework Core
- **Databases**:
  - PostgreSQL (Primary database)
  - Redis (Caching layer)
- **Containerization**: Docker & Docker Compose
- **API Documentation**: Swagger/OpenAPI


## Database Design

### Relational Database (PostgreSQL)
- **Books**: Core book information and metadata
- **Authors**: Author details with many-to-many relationship to books
- **Customers**: User accounts and preferences
- **Orders**: Transaction records and order status
- **Inventory**: Stock management and tracking

### NoSQL Database (Redis)
Used for:
- Caching frequently accessed book details
- User session management
- Shopping cart data
- Temporary data storage

## Caching Strategy

The application implements a sophisticated caching strategy:

1. **Book Details**: 
   - Cached for 24 hours
   - Invalidated on updates
   - Cache-aside pattern implementation

2. **Author Information**:
   - Cached with related books
   - Automatic invalidation on changes

3. **Shopping Cart**:
   - Redis-based session storage
   - 30-minute expiration
   - Persistent until checkout

## Setup Instructions

### Prerequisites
- Docker Desktop
- .NET 8 SDK
- Git

### Installation Steps

1. Clone the repository:
```bash
git clone https://github.com/yourusername/online-bookstore.git
cd online-bookstore
```

2. Start the databases:
```bash
docker-compose up -d
```

3. Update the connection strings in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=OnlineBookstore;Username=bookstore_user;Password=mysecretpassword;Port=5432",
    "Redis": "localhost:6379"
  }
}
```

4. Run the application:
```bash
dotnet run --project OnlineBookstore.Api
```

5. Access the Swagger documentation:
```
https://localhost:5067/swagger
```

## API Endpoints

### Books
- `GET /api/books` - List all books
- `GET /api/books/{id}` - Get book details
- `POST /api/books` - Add new book
- `PUT /api/books/{id}` - Update book details

### Orders
- `POST /api/orders` - Create new order
- `GET /api/orders/{id}` - Get order details
- `PUT /api/orders/{id}/status` - Update order status




## Design Choices

### Why PostgreSQL?
- ACID compliance for critical transactions
- Complex query support for reporting
- Strong data integrity through constraints
- Rich indexing capabilities

### Why Redis?
- High-performance caching
- Session state management
- Temporary data storage
- Pub/Sub capabilities for real-time features
