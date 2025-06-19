using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart;

/// <summary>
/// Represents a request to create a new user in the system.
/// </summary>
public class CartResponse
{

    /// <summary>
    /// The id of product
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The quantity of product
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price of product
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Original unit price before any discount.
    /// </summary>
    public decimal OriginalUnitPrice { get; private set; }

    /// <summary>
    /// Discount percentage applied.
    /// </summary>
    public decimal DiscountPercentage { get; private set; }
}