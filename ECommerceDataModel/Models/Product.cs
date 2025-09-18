using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceDataModel.Models;

/// <summary>
/// Represents a product in the eCommerce catalog
/// </summary>
public class Product
{
    public int ProductId { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(50)]
    public string? SKU { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? DiscountPrice { get; set; }

    public int StockQuantity { get; set; }

    public int? MinStockLevel { get; set; }

    public bool IsActive { get; set; } = true;

    [StringLength(255)]
    public string? ImageUrl { get; set; }

    public double? Weight { get; set; }

    [StringLength(100)]
    public string? Brand { get; set; }

    public int CategoryId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public virtual Category Category { get; set; } = null!;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}