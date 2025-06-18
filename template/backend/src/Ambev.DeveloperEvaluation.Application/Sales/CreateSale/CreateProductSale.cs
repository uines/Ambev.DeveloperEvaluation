using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Data Transfer Object for ProductSale.
    /// </summary>
    public class CreateProductSale
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
    }
}
