using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale in the system.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// The date of the sale
    /// </summary>
    public DateTime Date { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The customer who purchased
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The total purchase price
    /// </summary>
    public decimal TotalSaleAmount { get; set; }

    /// <summary>
    /// Where the sale was made
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// The products of sale
    /// </summary>
    public List<ProductSale> Products { get; set; } = new List<ProductSale>();

    /// <summary>
    /// Sale is canceled true or false
    /// </summary>
    public bool IsCanceled { get; set; }

    public decimal OriginalTotalPrice { get; set; }

    /// <summary>
    /// Initializes a new instance of the Sale class.
    /// </summary>
    public Sale()
    {
        Id = Guid.NewGuid(); 
    }

    /// <summary>
    /// Calculates the total sale amount based on products
    /// </summary>
    public void CalculateTotalSaleAmount()
    {
        TotalSaleAmount = Products.Sum(p => p.Quantity * p.UnitPrice);
        OriginalTotalPrice = Products.Sum(p => p.Quantity * p.OriginalUnitPrice);
    }
}
