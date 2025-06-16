using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.DTO
{
    /// <summary>
    /// Data Transfer Object for Sale.
    /// </summary>
    public class CreateSaleDTO
    {
        /// <summary>
        /// The id of the sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The date of the sale.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The customer who purchased.
        /// </summary>
        public string Customer { get; set; } = string.Empty;

        /// <summary>
        /// The total purchase price.
        /// </summary>
        public decimal TotalSaleAmount { get; set; }

        /// <summary>
        /// Where the sale was made.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Indicates if the sale is canceled.
        /// </summary>
        public bool IsCanceled { get; set; } = false;

        /// <summary>
        /// The products included in the sale.
        /// </summary>
        public List<CreateProductSaleDTO> Products { get; set; } = new List<CreateProductSaleDTO>();
    }
}
