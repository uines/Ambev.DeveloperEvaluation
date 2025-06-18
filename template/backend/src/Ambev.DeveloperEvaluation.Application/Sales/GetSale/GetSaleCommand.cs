using Ambev.DeveloperEvaluation.Application.Sales.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Command for retrieving sales
/// </summary>
public record GetSaleCommand : IRequest<List<SaleResult>>
{
    /// <summary>
    /// The customer who purchased
    /// </summary>
    public Guid CustomerId { get; set; }
}