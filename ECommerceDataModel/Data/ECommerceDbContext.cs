using Microsoft.EntityFrameworkCore;
using ECommerceDataModel.Models;

namespace ECommerceDataModel.Data;

/// <summary>
/// Entity Framework DbContext for the eCommerce data model
/// </summary>
public class ECommerceDbContext : DbContext
{
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
    {
    }

    // DbSets for each entity
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureCustomer(modelBuilder);
        ConfigureCategory(modelBuilder);
        ConfigureProduct(modelBuilder);
        ConfigureOrder(modelBuilder);
        ConfigureOrderItem(modelBuilder);
    }

    private static void ConfigureCustomer(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Customer>();
        
        entity.HasKey(c => c.CustomerId);
        entity.Property(c => c.CustomerId).ValueGeneratedOnAdd();
        
        entity.HasIndex(c => c.Email).IsUnique();
        
        entity.Property(c => c.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        
        // Configure one-to-many relationship with Orders
        entity.HasMany(c => c.Orders)
              .WithOne(o => o.Customer)
              .HasForeignKey(o => o.CustomerId)
              .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureCategory(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Category>();
        
        entity.HasKey(c => c.CategoryId);
        entity.Property(c => c.CategoryId).ValueGeneratedOnAdd();
        
        entity.HasIndex(c => c.Name).IsUnique();
        
        entity.Property(c => c.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        
        // Configure self-referencing relationship
        entity.HasOne(c => c.ParentCategory)
              .WithMany(c => c.SubCategories)
              .HasForeignKey(c => c.ParentCategoryId)
              .OnDelete(DeleteBehavior.Restrict);
              
        // Configure one-to-many relationship with Products
        entity.HasMany(c => c.Products)
              .WithOne(p => p.Category)
              .HasForeignKey(p => p.CategoryId)
              .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureProduct(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Product>();
        
        entity.HasKey(p => p.ProductId);
        entity.Property(p => p.ProductId).ValueGeneratedOnAdd();
        
        entity.HasIndex(p => p.SKU).IsUnique().HasFilter("[SKU] IS NOT NULL");
        entity.HasIndex(p => p.Name);
        
        entity.Property(p => p.Price).HasPrecision(18, 2);
        entity.Property(p => p.DiscountPrice).HasPrecision(18, 2);
        
        entity.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        
        // Configure one-to-many relationship with OrderItems
        entity.HasMany(p => p.OrderItems)
              .WithOne(oi => oi.Product)
              .HasForeignKey(oi => oi.ProductId)
              .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureOrder(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Order>();
        
        entity.HasKey(o => o.OrderId);
        entity.Property(o => o.OrderId).ValueGeneratedOnAdd();
        
        entity.HasIndex(o => o.OrderNumber).IsUnique();
        entity.HasIndex(o => o.OrderDate);
        entity.HasIndex(o => o.Status);
        
        entity.Property(o => o.SubTotal).HasPrecision(18, 2);
        entity.Property(o => o.TaxAmount).HasPrecision(18, 2);
        entity.Property(o => o.ShippingCost).HasPrecision(18, 2);
        entity.Property(o => o.DiscountAmount).HasPrecision(18, 2);
        entity.Property(o => o.TotalAmount).HasPrecision(18, 2);
        
        entity.Property(o => o.OrderDate).HasDefaultValueSql("GETUTCDATE()");
        entity.Property(o => o.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        
        entity.Property(o => o.Status).HasConversion<int>();
        
        // Configure one-to-many relationship with OrderItems
        entity.HasMany(o => o.OrderItems)
              .WithOne(oi => oi.Order)
              .HasForeignKey(oi => oi.OrderId)
              .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureOrderItem(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<OrderItem>();
        
        entity.HasKey(oi => oi.OrderItemId);
        entity.Property(oi => oi.OrderItemId).ValueGeneratedOnAdd();
        
        entity.Property(oi => oi.UnitPrice).HasPrecision(18, 2);
        entity.Property(oi => oi.DiscountAmount).HasPrecision(18, 2);
        entity.Property(oi => oi.TotalPrice).HasPrecision(18, 2);
        
        entity.Property(oi => oi.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        
        // Composite index for performance
        entity.HasIndex(oi => new { oi.OrderId, oi.ProductId });
    }
}