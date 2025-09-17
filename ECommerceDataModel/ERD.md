# eCommerce Data Model - Entity Relationship Diagram

```
┌─────────────────┐         ┌─────────────────┐
│     Category    │         │     Customer    │
├─────────────────┤         ├─────────────────┤
│ Id (PK)         │         │ Id (PK)         │
│ Name            │         │ FirstName       │
│ Description     │         │ LastName        │
└─────────────────┘         │ Email           │
         │                  │ Phone           │
         │ 1:N              │ Address         │
         ▼                  │ City            │
┌─────────────────┐         │ PostalCode      │
│     Product     │         │ Country         │
├─────────────────┤         │ CreatedDate     │
│ Id (PK)         │         └─────────────────┘
│ Name            │                  │
│ Description     │                  │ 1:N
│ Price           │                  ▼
│ StockQuantity   │         ┌─────────────────┐
│ SKU             │         │      Order      │
│ IsActive        │         ├─────────────────┤
│ CreatedDate     │         │ Id (PK)         │
│ CategoryId (FK) │         │ OrderNumber     │
└─────────────────┘         │ OrderDate       │
         │                  │ TotalAmount     │
         │ N:M              │ Status          │
         │ (via OrderItem)  │ ShippingAddress │
         ▼                  │ ShippingCity    │
┌─────────────────┐         │ ShippingPostalCode │
│    OrderItem    │         │ ShippingCountry │
├─────────────────┤         │ ShippedDate     │
│ Id (PK)         │         │ DeliveredDate   │
│ Quantity        │         │ CustomerId (FK) │
│ UnitPrice       │         └─────────────────┘
│ TotalPrice      │                  ▲
│ OrderId (FK)    │                  │ 1:N
│ ProductId (FK)  │                  │
└─────────────────┘──────────────────┘
```

## Relationships:

1. **Category → Product** (1:N)
   - One category can have many products
   - Each product belongs to one category

2. **Customer → Order** (1:N)
   - One customer can place many orders
   - Each order belongs to one customer

3. **Order → OrderItem** (1:N)
   - One order can have many order items
   - Each order item belongs to one order

4. **Product → OrderItem** (1:N)
   - One product can appear in many order items
   - Each order item references one product

5. **Product ↔ Order** (N:M via OrderItem)
   - Many-to-many relationship through OrderItem junction table
   - Allows tracking quantity and pricing per order

## Key Constraints:

- **Unique Indexes**: Category.Name, Product.SKU, Customer.Email, Order.OrderNumber
- **Foreign Key Constraints**: All relationships enforced with restrict/cascade rules
- **Data Validation**: Required fields, max lengths, email format validation
- **Default Values**: Timestamps, boolean flags, status fields