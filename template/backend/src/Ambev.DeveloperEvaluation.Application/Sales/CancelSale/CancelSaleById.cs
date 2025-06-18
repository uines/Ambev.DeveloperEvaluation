using Ambev.DeveloperEvaluation.Application.Sales.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Command for retrieving sales
/// </summary>
public record CancelSaleById : IRequest<CancelSaleResult>
{
    /// <summary>
    /// The customer who purchased
    /// </summary>
    public Guid Id { get; set; }
}