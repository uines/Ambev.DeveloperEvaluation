using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Cart;

/// <summary>
/// Handler for processing CartCommand requests
/// </summary>
public class CartHandler : IRequestHandler<CartCommand, CartResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CartHandler> _logger;

    /// <summary>
    /// Initializes a new instance of CartHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CartHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<CartHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the CartCommand request
    /// </summary>
    /// <param name="command">The Cart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    public async Task<CartResult> Handle(CartCommand command, CancellationToken cancellationToken)
    {
        ProductSale productSale = _mapper.Map<ProductSale>(command);
        ValidateProductQuantity(productSale.Quantity);
        ApplyDiscount(productSale);

        var result = _mapper.Map<CartResult>(productSale);
        return result;
    }

    private void ValidateProductQuantity(int quantity)
    {
        if (quantity > 20)
        {
            _logger.LogInformation("Customer is attempting to purchase more than 20 identical items.");

            throw new InvalidOperationException("Is not possible to sell more than 20 identical items.");
        }
    }

    private void ApplyDiscount(ProductSale productSale)
    {
        productSale.OriginalUnitPrice = productSale.UnitPrice;
        if (productSale.Quantity >= 4 && productSale.Quantity < 10)
        {
            productSale.DiscountPercentage = 0.10m;
        }
        else if (productSale.Quantity >= 10 && productSale.Quantity <= 20)
        {
            productSale.DiscountPercentage = 0.20m;
        }
        productSale.UnitPrice -= productSale.UnitPrice * productSale.DiscountPercentage;
        productSale.DiscountPercentage *= 100;
    }

}
