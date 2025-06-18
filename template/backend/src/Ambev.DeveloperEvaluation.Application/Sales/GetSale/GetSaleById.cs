﻿using Ambev.DeveloperEvaluation.Application.Sales.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Query for retrieving a specific Sale by its ID.
/// </summary>
public record GetSaleById : IRequest<SaleResult>
{
    /// <summary>
    /// The unique identifier of the Sale.
    /// </summary>
    public Guid Id { get; set; }
}