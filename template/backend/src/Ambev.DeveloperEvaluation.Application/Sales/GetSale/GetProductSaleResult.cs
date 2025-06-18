using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Data Transfer Object for ProductSale.
    /// </summary>
    public class GetProductSaleResult
    {

        /// <summary>
        /// The id of the sale.
        /// </summary>
        public Guid Id { get; set; }
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
}
