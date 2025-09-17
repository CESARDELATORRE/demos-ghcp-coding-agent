using Microsoft.EntityFrameworkCore;
using ECommerceDataModel.Data;
using ECommerceDataModel.Models;

Console.WriteLine("eCommerce Data Model Demo");
Console.WriteLine("========================");

// Configure Entity Framework to use in-memory database for demonstration
var options = new DbContextOptionsBuilder<ECommerceDbContext>()
    .UseInMemoryDatabase(databaseName: "ECommerceDemo")
    .Options;

using var context = new ECommerceDbContext(options);

// Ensure database is created and seeded
await context.Database.EnsureCreatedAsync();

Console.WriteLine("\n1. Categories:");
var categories = await context.Categories.Include(c => c.Products).ToListAsync();
foreach (var category in categories)
{
    Console.WriteLine($"   - {category.Name}: {category.Products.Count} products");
}

Console.WriteLine("\n2. Products:");
var products = await context.Products.Include(p => p.Category).ToListAsync();
foreach (var product in products)
{
    Console.WriteLine($"   - {product.Name} ({product.Category.Name}): ${product.Price:F2} - Stock: {product.StockQuantity}");
}

Console.WriteLine("\n3. Customers:");
var customers = await context.Customers.ToListAsync();
foreach (var customer in customers)
{
    Console.WriteLine($"   - {customer.FirstName} {customer.LastName} ({customer.Email})");
}

Console.WriteLine("\n4. Creating a sample order...");

// Create a sample order
var orderCustomer = await context.Customers.FirstAsync();
var laptop = await context.Products.FirstAsync(p => p.SKU == "LAP001");
var tshirt = await context.Products.FirstAsync(p => p.SKU == "TSH001");

var order = new Order
{
    OrderNumber = $"ORD-{DateTime.Now.Ticks}",
    CustomerId = orderCustomer.Id,
    OrderDate = DateTime.UtcNow,
    Status = "Pending",
    ShippingAddress = orderCustomer.Address,
    ShippingCity = orderCustomer.City,
    ShippingPostalCode = orderCustomer.PostalCode,
    ShippingCountry = orderCustomer.Country
};

var orderItems = new List<OrderItem>
{
    new OrderItem
    {
        ProductId = laptop.Id,
        Quantity = 1,
        UnitPrice = laptop.Price,
        TotalPrice = laptop.Price * 1
    },
    new OrderItem
    {
        ProductId = tshirt.Id,
        Quantity = 2,
        UnitPrice = tshirt.Price,
        TotalPrice = tshirt.Price * 2
    }
};

order.TotalAmount = orderItems.Sum(oi => oi.TotalPrice);
order.OrderItems = orderItems;

context.Orders.Add(order);
await context.SaveChangesAsync();

Console.WriteLine($"   Order {order.OrderNumber} created successfully!");

Console.WriteLine("\n5. Orders with items:");
var orders = await context.Orders
    .Include(o => o.Customer)
    .Include(o => o.OrderItems)
    .ThenInclude(oi => oi.Product)
    .ToListAsync();

foreach (var ord in orders)
{
    Console.WriteLine($"   Order: {ord.OrderNumber} by {ord.Customer.FirstName} {ord.Customer.LastName}");
    Console.WriteLine($"   Status: {ord.Status}, Total: ${ord.TotalAmount:F2}");
    Console.WriteLine("   Items:");
    foreach (var item in ord.OrderItems)
    {
        Console.WriteLine($"     - {item.Product.Name} x{item.Quantity} @ ${item.UnitPrice:F2} = ${item.TotalPrice:F2}");
    }
}

Console.WriteLine("\nDemo completed successfully! The POCO data model is working correctly with Entity Framework.");
Console.WriteLine("The model includes proper relationships, data annotations, and seed data.");
