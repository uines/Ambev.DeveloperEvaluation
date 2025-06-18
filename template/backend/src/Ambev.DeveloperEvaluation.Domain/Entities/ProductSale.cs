﻿using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product of sale in the system.
/// </summary>
public class ProductSale : BaseEntity
{
    /// <summary>
    /// The id of sale
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// The name of product.
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
    /// The total amount of the products being the quantity * the unit price
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Original unit price before any discount.
    /// </summary>
    public decimal OriginalUnitPrice { get; set; }

    /// <summary>
    /// Discount percentage applied.
    /// </summary>
    public decimal DiscountPercentage { get; set; }
}
