using System.ComponentModel.DataAnnotations;

namespace ECommerceDataModel.Models;

/// <summary>
/// Represents a customer in the eCommerce system
/// </summary>
public class Customer
{
    public int CustomerId { get; set; }

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [StringLength(255)]
    public string? Address { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(20)]
    public string? PostalCode { get; set; }

    [StringLength(100)]
    public string? Country { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}