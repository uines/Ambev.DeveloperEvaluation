using Ambev.DeveloperEvaluation.Application.Sales.DTO;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleResult
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
        public List<ResponseProductSaleDTO> Products { get; set; } = new List<ResponseProductSaleDTO>();

        public decimal OriginTotalPrice { get; set; }
    }
}