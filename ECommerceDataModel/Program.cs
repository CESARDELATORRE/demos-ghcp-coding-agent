using Microsoft.EntityFrameworkCore;
using ECommerceDataModel.Data;
using ECommerceDataModel.Models;

Console.WriteLine("🛒 eCommerce POCO Data Model Demo");
Console.WriteLine("==================================");

// Configure in-memory database for demonstration
var options = new DbContextOptionsBuilder<ECommerceDbContext>()
    .UseInMemoryDatabase(databaseName: "ECommerceDemo")
    .Options;

using var context = new ECommerceDbContext(options);

// Create sample data
await SeedSampleDataAsync(context);

// Display data model information
DisplayDataModelInfo();

// Demonstrate relationships
await DemonstrateRelationshipsAsync(context);

Console.WriteLine("\n✅ Demo completed successfully!");

static async Task SeedSampleDataAsync(ECommerceDbContext context)
{
    Console.WriteLine("\n📦 Creating sample data...");

    // Create categories
    var electronics = new Category { Name = "Electronics", Description = "Electronic devices and accessories" };
    var computers = new Category { Name = "Computers", Description = "Desktop and laptop computers", ParentCategory = electronics };
    var smartphones = new Category { Name = "Smartphones", Description = "Mobile phones and accessories", ParentCategory = electronics };

    context.Categories.AddRange(electronics, computers, smartphones);
    await context.SaveChangesAsync();

    // Create products
    var laptop = new Product
    {
        Name = "Gaming Laptop",
        Description = "High-performance gaming laptop",
        SKU = "LAP-001",
        Price = 1299.99m,
        StockQuantity = 15,
        Brand = "TechBrand",
        Category = computers
    };

    var phone = new Product
    {
        Name = "Smartphone Pro",
        Description = "Latest flagship smartphone",
        SKU = "PHN-001",
        Price = 899.99m,
        DiscountPrice = 799.99m,
        StockQuantity = 25,
        Brand = "PhoneCorp",
        Category = smartphones
    };

    context.Products.AddRange(laptop, phone);
    await context.SaveChangesAsync();

    // Create customer
    var customer = new Customer
    {
        FirstName = "John",
        LastName = "Doe",
        Email = "john.doe@email.com",
        PhoneNumber = "+1-555-0123",
        Address = "123 Main St",
        City = "Anytown",
        PostalCode = "12345",
        Country = "USA"
    };

    context.Customers.Add(customer);
    await context.SaveChangesAsync();

    // Create order
    var order = new Order
    {
        OrderNumber = "ORD-2024-001",
        Customer = customer,
        Status = OrderStatus.Confirmed,
        ShippingAddress = customer.Address,
        ShippingCity = customer.City,
        ShippingPostalCode = customer.PostalCode,
        ShippingCountry = customer.Country,
        PaymentMethod = "Credit Card"
    };

    context.Orders.Add(order);
    await context.SaveChangesAsync();

    // Create order items
    var orderItem1 = new OrderItem
    {
        Order = order,
        Product = laptop,
        Quantity = 1,
        UnitPrice = laptop.Price,
        TotalPrice = laptop.Price,
        ProductName = laptop.Name,
        ProductSKU = laptop.SKU
    };

    var orderItem2 = new OrderItem
    {
        Order = order,
        Product = phone,
        Quantity = 2,
        UnitPrice = phone.DiscountPrice ?? phone.Price,
        TotalPrice = (phone.DiscountPrice ?? phone.Price) * 2,
        ProductName = phone.Name,
        ProductSKU = phone.SKU
    };

    context.OrderItems.AddRange(orderItem1, orderItem2);

    // Update order totals
    order.SubTotal = orderItem1.TotalPrice + orderItem2.TotalPrice;
    order.TaxAmount = order.SubTotal * 0.08m; // 8% tax
    order.ShippingCost = 15.99m;
    order.TotalAmount = order.SubTotal + order.TaxAmount + order.ShippingCost;

    await context.SaveChangesAsync();

    Console.WriteLine("✅ Sample data created successfully!");
}

static void DisplayDataModelInfo()
{
    Console.WriteLine("\n📋 Data Model Overview:");
    Console.WriteLine("=======================");
    Console.WriteLine("🏪 Customer - Stores customer information and contact details");
    Console.WriteLine("📂 Category - Product categorization with hierarchical support");
    Console.WriteLine("📱 Product - Catalog items with pricing, inventory, and metadata");
    Console.WriteLine("🛒 Order - Customer orders with status tracking and addresses");
    Console.WriteLine("📦 OrderItem - Individual line items within orders");
    Console.WriteLine();
    Console.WriteLine("🔗 Key Relationships:");
    Console.WriteLine("• Customer 1 ↔ Many Orders");
    Console.WriteLine("• Category 1 ↔ Many Products (with self-referencing hierarchy)");
    Console.WriteLine("• Product 1 ↔ Many OrderItems");
    Console.WriteLine("• Order 1 ↔ Many OrderItems");
}

static async Task DemonstrateRelationshipsAsync(ECommerceDbContext context)
{
    Console.WriteLine("\n🔍 Querying Sample Data:");
    Console.WriteLine("=========================");

    // Query customers with orders
    var customersWithOrders = await context.Customers
        .Include(c => c.Orders)
        .ThenInclude(o => o.OrderItems)
        .ThenInclude(oi => oi.Product)
        .ToListAsync();

    foreach (var customer in customersWithOrders)
    {
        Console.WriteLine($"👤 Customer: {customer.FirstName} {customer.LastName} ({customer.Email})");
        Console.WriteLine($"   📍 Address: {customer.Address}, {customer.City}, {customer.Country}");
        
        foreach (var order in customer.Orders)
        {
            Console.WriteLine($"   🛒 Order #{order.OrderNumber} - Status: {order.Status}");
            Console.WriteLine($"      💰 Total: ${order.TotalAmount:F2} (Subtotal: ${order.SubTotal:F2}, Tax: ${order.TaxAmount:F2}, Shipping: ${order.ShippingCost:F2})");
            Console.WriteLine($"      📅 Order Date: {order.OrderDate:yyyy-MM-dd HH:mm}");
            
            foreach (var item in order.OrderItems)
            {
                Console.WriteLine($"      📦 {item.Quantity}x {item.ProductName} (SKU: {item.ProductSKU}) - ${item.TotalPrice:F2}");
            }
        }
    }

    // Query categories with products
    Console.WriteLine();
    var categoriesWithProducts = await context.Categories
        .Include(c => c.Products)
        .Include(c => c.SubCategories)
        .Where(c => c.ParentCategoryId == null) // Root categories only
        .ToListAsync();

    foreach (var category in categoriesWithProducts)
    {
        Console.WriteLine($"📂 Category: {category.Name}");
        if (category.SubCategories.Any())
        {
            foreach (var subCategory in category.SubCategories)
            {
                Console.WriteLine($"   📁 Subcategory: {subCategory.Name} ({subCategory.Products.Count} products)");
                foreach (var product in subCategory.Products)
                {
                    var price = product.DiscountPrice.HasValue ? 
                        $"${product.DiscountPrice:F2} (was ${product.Price:F2})" : 
                        $"${product.Price:F2}";
                    Console.WriteLine($"      📱 {product.Name} - {price} (Stock: {product.StockQuantity})");
                }
            }
        }
    }
}
