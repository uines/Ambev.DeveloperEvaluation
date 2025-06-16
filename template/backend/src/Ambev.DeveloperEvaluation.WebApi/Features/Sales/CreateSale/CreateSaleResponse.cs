using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// API response model for CreateUser operation
/// </summary>
public class CreateSaleResponse
{
    /// <summary>
    /// The id of the created sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The date of the sale
    /// </summary>
    public DateTime Date { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The customer who purchased
    /// </summary>
    public string Customer { get; set; } = string.Empty;

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
    public List<ResponseProductSaleDTO> Products { get; set; } = new List<ResponseProductSaleDTO>();

    /// <summary>
    /// Sale is canceled true or false
    /// </summary>
    public bool IsCanceled { get; set; }

    public decimal OriginTotalPrice { get; set; }
}
