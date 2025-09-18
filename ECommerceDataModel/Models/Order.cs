using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceDataModel.Models;

/// <summary>
/// Represents an order in the eCommerce system
/// </summary>
public class Order
{
    public int OrderId { get; set; }

    [Required]
    [StringLength(50)]
    public string OrderNumber { get; set; } = string.Empty;

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    [Column(TypeName = "decimal(18,2)")]
    public decimal SubTotal { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TaxAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ShippingCost { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [StringLength(255)]
    public string? ShippingAddress { get; set; }

    [StringLength(100)]
    public string? ShippingCity { get; set; }

    [StringLength(20)]
    public string? ShippingPostalCode { get; set; }

    [StringLength(100)]
    public string? ShippingCountry { get; set; }

    [StringLength(255)]
    public string? BillingAddress { get; set; }

    [StringLength(100)]
    public string? BillingCity { get; set; }

    [StringLength(20)]
    public string? BillingPostalCode { get; set; }

    [StringLength(100)]
    public string? BillingCountry { get; set; }

    [StringLength(50)]
    public string? PaymentMethod { get; set; }

    [StringLength(100)]
    public string? PaymentReference { get; set; }

    public DateTime? ShippedDate { get; set; }

    public DateTime? DeliveredDate { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public virtual Customer Customer { get; set; } = null!;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

/// <summary>
/// Enumeration for order status values
/// </summary>
public enum OrderStatus
{
    Pending = 0,
    Confirmed = 1,
    Processing = 2,
    Shipped = 3,
    Delivered = 4,
    Cancelled = 5,
    Returned = 6
}