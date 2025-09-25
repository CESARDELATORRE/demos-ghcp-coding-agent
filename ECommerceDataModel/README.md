# eCommerce Data Model - C# POCO with Entity Framework

This project demonstrates a complete POCO (Plain Old CLR Object) data model for an eCommerce system using Entity Framework Core.

## Data Model Structure

The eCommerce system includes the following entities:

### 1. Category
- **Purpose**: Organizes products into categories
- **Properties**:
  - `Id` (Primary Key)
  - `Name` (Required, Max 100 chars, Unique)
  - `Description` (Optional, Max 500 chars)
- **Relationships**: One-to-Many with Products

### 2. Product
- **Purpose**: Represents items available for sale
- **Properties**:
  - `Id` (Primary Key)
  - `Name` (Required, Max 200 chars)
  - `Description` (Optional, Max 1000 chars)
  - `Price` (Required, Decimal 18,2)
  - `StockQuantity` (Required, Integer)
  - `SKU` (Optional, Max 50 chars, Unique)
  - `IsActive` (Boolean, Default: true)
  - `CreatedDate` (DateTime, Default: UTC Now)
  - `CategoryId` (Foreign Key)
- **Relationships**: 
  - Many-to-One with Category
  - One-to-Many with OrderItems

### 3. Customer
- **Purpose**: Represents customers who can place orders
- **Properties**:
  - `Id` (Primary Key)
  - `FirstName` (Required, Max 100 chars)
  - `LastName` (Required, Max 100 chars)
  - `Email` (Required, Max 255 chars, Unique, Email format)
  - `Phone` (Optional, Max 20 chars)
  - `Address` (Optional, Max 255 chars)
  - `City` (Optional, Max 100 chars)
  - `PostalCode` (Optional, Max 10 chars)
  - `Country` (Optional, Max 100 chars)
  - `CreatedDate` (DateTime, Default: UTC Now)
- **Relationships**: One-to-Many with Orders

### 4. Order
- **Purpose**: Represents customer orders
- **Properties**:
  - `Id` (Primary Key)
  - `OrderNumber` (Required, Max 50 chars, Unique)
  - `OrderDate` (DateTime, Default: UTC Now)
  - `TotalAmount` (Required, Decimal 18,2)
  - `Status` (Required, Max 50 chars, Default: "Pending")
  - `ShippingAddress` (Optional, Max 255 chars)
  - `ShippingCity` (Optional, Max 100 chars)
  - `ShippingPostalCode` (Optional, Max 10 chars)
  - `ShippingCountry` (Optional, Max 100 chars)
  - `ShippedDate` (Optional, DateTime)
  - `DeliveredDate` (Optional, DateTime)
  - `CustomerId` (Foreign Key)
- **Relationships**: 
  - Many-to-One with Customer
  - One-to-Many with OrderItems

### 5. OrderItem
- **Purpose**: Represents individual items within an order (junction table)
- **Properties**:
  - `Id` (Primary Key)
  - `Quantity` (Required, Integer)
  - `UnitPrice` (Required, Decimal 18,2)
  - `TotalPrice` (Required, Decimal 18,2)
  - `OrderId` (Foreign Key)
  - `ProductId` (Foreign Key)
- **Relationships**: 
  - Many-to-One with Order
  - Many-to-One with Product

## Entity Framework Configuration

The `ECommerceDbContext` class provides:

- **Fluent API Configuration**: Detailed entity configuration including constraints, indexes, and relationships
- **Cascade Delete Rules**: Configured to maintain data integrity
- **Seed Data**: Sample data for testing and demonstration
- **Database Provider Support**: Configured for SQL Server with easy adaptation for other providers

## Key Features

1. **Data Annotations**: Used for validation and basic configuration
2. **Fluent API**: Advanced configuration for relationships and constraints
3. **Navigation Properties**: Virtual properties for Entity Framework lazy loading
4. **Unique Constraints**: Email, SKU, and OrderNumber uniqueness
5. **Default Values**: Automatic timestamps and status defaults
6. **Foreign Key Relationships**: Proper referential integrity
7. **Index Optimization**: Composite indexes for query performance

## Running the Demo

```bash
cd ECommerceDataModel
dotnet restore
dotnet build
dotnet run
```

The demo application will:
1. Create an in-memory database
2. Apply the data model and seed data
3. Display categories, products, and customers
4. Create a sample order with multiple items
5. Demonstrate querying with relationships

## Dependencies

- .NET 8.0
- Microsoft.EntityFrameworkCore (9.0.9)
- Microsoft.EntityFrameworkCore.SqlServer (9.0.9)
- Microsoft.EntityFrameworkCore.Tools (9.0.9)
- Microsoft.EntityFrameworkCore.InMemory (9.0.9) - for demo

## Database Migration

To use with a real SQL Server database:

1. Update connection string in DbContext
2. Remove InMemory provider configuration
3. Run: `dotnet ef migrations add InitialCreate`
4. Run: `dotnet ef database update`