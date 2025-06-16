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
    public decimal OriginalUnitPrice { get; private set; }

    /// <summary>
    /// Discount percentage applied.
    /// </summary>
    public decimal DiscountPercentage { get; private set; }

    public ProductSale(Guid saleId, string name, int quantity, decimal unitPrice) : this()
    {
        if (quantity > 20)
            throw new ArgumentOutOfRangeException(nameof(quantity), "It's not possible to sell above 20 identical items.");
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        if (unitPrice <= 0)
            throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price must be greater than zero.");

        SaleId = saleId;
        Name = name;
        Quantity = quantity;
        OriginalUnitPrice = unitPrice;
        UnitPrice = unitPrice; // Initial unit price

        if (quantity >= 10) DiscountPercentage = 0.20m; // discount 20%
        else if (quantity >= 4) DiscountPercentage = 0.10m; // discount 10%
        else DiscountPercentage = 0m; // No discount for less than 4 items

        UnitPrice -= UnitPrice * DiscountPercentage; // Apply discount
        TotalAmount = Quantity * UnitPrice;
    }

    // Construtor privado para EF Core e AutoMapper
    private ProductSale() : base() { }
}
