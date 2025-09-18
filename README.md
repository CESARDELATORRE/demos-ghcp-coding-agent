# demos-ghcp-coding-agent
Repo for very simple demos on GHCP Coding Agent

## 🛒 eCommerce Data Model Demo

This repository contains a comprehensive C# POCO data model demonstration for Entity Framework representing a simple catalog and order system for an eCommerce application.

### 📁 Project Structure

- **ECommerceDataModel/**: Complete C# console application demonstrating EF Core POCO entities
  - **Models/**: POCO entity classes (Customer, Category, Product, Order, OrderItem)
  - **Data/**: Entity Framework DbContext with configurations
  - **Program.cs**: Demo application showing data model usage
  - **README.md**: Detailed documentation of the data model

### 🚀 Quick Start

```bash
cd ECommerceDataModel
dotnet restore
dotnet build
dotnet run
```

### 🎯 What's Included

- **5 Core Entities**: Customer, Category, Product, Order, OrderItem
- **Complete Relationships**: Foreign keys, navigation properties, and proper EF configurations
- **Business Features**: Hierarchical categories, order status tracking, pricing with discounts
- **Data Integrity**: Unique constraints, indexes, and validation attributes
- **Working Demo**: Sample data creation and relationship queries

This demo showcases best practices for designing POCO entities with Entity Framework Core, including proper relationship configuration, data validation, and performance optimization through strategic indexing.
