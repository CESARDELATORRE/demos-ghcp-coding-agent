# eCommerce POCO Data Model

This project demonstrates a complete C# POCO (Plain Old CLR Object) data model for Entity Framework representing a simple catalog and order system for an eCommerce application.

## 🚀 Quick Start

To run the demonstration:

```bash
cd ECommerceDataModel
dotnet restore
dotnet build
dotnet run
```

## 📊 Data Model Overview

The eCommerce data model consists of five main entities that represent the core functionality of an online store:

### 🏪 Customer
Represents customers in the system with complete contact information:
- **Primary Key**: `CustomerId` (int)
- **Fields**: FirstName, LastName, Email (unique), PhoneNumber, Address, City, PostalCode, Country
- **Relationships**: One-to-Many with Orders
- **Features**: Email uniqueness, audit timestamps

### 📂 Category
Product categorization with hierarchical support for nested categories:
- **Primary Key**: `CategoryId` (int)
- **Fields**: Name (unique), Description, ParentCategoryId, IsActive
- **Relationships**: 
  - Self-referencing (Parent/Child categories)
  - One-to-Many with Products
- **Features**: Hierarchical categories, soft delete support

### 📱 Product
Catalog items with comprehensive product information:
- **Primary Key**: `ProductId` (int)
- **Fields**: Name, Description, SKU (unique), Price, DiscountPrice, StockQuantity, MinStockLevel, IsActive, ImageUrl, Weight, Brand
- **Relationships**: 
  - Many-to-One with Category
  - One-to-Many with OrderItems
- **Features**: Inventory tracking, pricing with discounts, product metadata

### 🛒 Order
Customer orders with complete order management:
- **Primary Key**: `OrderId` (int)
- **Fields**: OrderNumber (unique), CustomerId, OrderDate, Status (enum), financial fields (SubTotal, Tax, Shipping, Discount, Total), shipping/billing addresses, payment information
- **Relationships**: 
  - Many-to-One with Customer
  - One-to-Many with OrderItems
- **Features**: Status tracking, financial calculations, address management, payment tracking

### 📦 OrderItem
Individual line items within orders:
- **Primary Key**: `OrderItemId` (int)
- **Fields**: OrderId, ProductId, Quantity, UnitPrice, DiscountAmount, TotalPrice, ProductName (snapshot), ProductSKU (snapshot)
- **Relationships**: 
  - Many-to-One with Order
  - Many-to-One with Product
- **Features**: Price snapshots, quantity tracking, line totals

## 🔗 Entity Relationships

```
Customer (1) ←→ (Many) Order (1) ←→ (Many) OrderItem (Many) ←→ (1) Product
                                                                     ↑
Category (1) ←→ (Many) Product                                      │
    ↑                                                               │
    └── Self-referencing (Parent/Child)                            │
                                                                    │
OrderItem captures product information at time of order ──────────┘
```

## ⚙️ Entity Framework Configuration

The `ECommerceDbContext` includes comprehensive EF Core configurations:

- **Precision Settings**: Decimal fields configured with 18,2 precision for financial calculations
- **Indexes**: Performance indexes on frequently queried fields (Email, OrderNumber, SKU, etc.)
- **Relationships**: Properly configured foreign keys with appropriate delete behaviors
- **Constraints**: Unique constraints, required fields, and data validation
- **Default Values**: Automatic timestamp generation using SQL Server functions

## 🎯 Key Features

### Data Integrity
- **Unique Constraints**: Email addresses, SKUs, order numbers
- **Foreign Key Constraints**: Maintain referential integrity
- **Cascade Rules**: Appropriate delete behaviors (Restrict vs Cascade)

### Performance Optimizations
- **Strategic Indexing**: Indexes on frequently searched fields
- **Composite Indexes**: Multi-column indexes for complex queries
- **Lazy Loading**: Virtual navigation properties for on-demand loading

### Business Logic Support
- **Audit Timestamps**: Created/Updated tracking on all entities
- **Soft Deletes**: IsActive flags for logical deletion
- **Status Enums**: Strongly-typed order status management
- **Price Snapshots**: Historical price preservation in order items

### Financial Accuracy
- **Decimal Precision**: 18,2 decimal precision for all monetary values
- **Comprehensive Pricing**: Support for discounts, taxes, shipping costs
- **Line Item Totals**: Calculated totals at the order item level

## 🛠️ Technologies Used

- **.NET 8.0**: Latest .NET framework
- **Entity Framework Core 9.0**: ORM for data access
- **C# 12**: Modern C# language features
- **SQL Server Provider**: Production-ready database provider
- **In-Memory Provider**: For testing and demonstrations

## 📝 Usage Examples

The `Program.cs` file demonstrates:
- Creating sample data with relationships
- Querying data using EF Core navigation properties
- Displaying hierarchical category structures
- Calculating order totals and relationships

## 🔄 Extensibility

The data model is designed for easy extension:
- Add new entities by following the established patterns
- Extend existing entities with additional properties
- Implement new relationships using EF Core conventions
- Add business logic through partial classes or inheritance

## 🏗️ Architecture Notes

- **POCO Design**: Clean separation between data models and framework dependencies
- **Convention over Configuration**: Leverages EF Core conventions where possible
- **Explicit Configuration**: Uses Fluent API for complex relationships and constraints
- **Separation of Concerns**: Models, data context, and configuration are properly separated

This data model provides a solid foundation for an eCommerce application with room for growth and customization based on specific business requirements.