using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, SaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper, ILogger<CreateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the CreateSaleCommand request
    /// </summary>
    /// <param name="command">The CreateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    public async Task<SaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        Sale sale = _mapper.Map<Sale>(command);
        //sale.Products = command.Products.Select(p => {
        //        ValidateProductQuantity(command.CustomerId, p.Quantity);
        //        var productSale = new ProductSale { }
        //            saleId: sale.Id,
        //            name: p.Name,
        //            quantity: p.Quantity,
        //            unitPrice: p.UnitPrice
        //        );
        //        ApplyDiscount(productSale);
        //        return productSale;
        //    }
        //).ToList();

        sale.Products = command.Products.Select(p => {
            ValidateProductQuantity(command.CustomerId, p.Quantity);
            var productSale = new ProductSale
            {
                SaleId = sale.Id,
                Name = p.Name,
                Quantity = p.Quantity,
                UnitPrice = p.UnitPrice
            };
            ApplyDiscount(productSale);
            return productSale;
        }).ToList();

        sale.CalculateTotalSaleAmount();
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        var result = _mapper.Map<SaleResult>(createdSale);
        return result;
    }

    private void ValidateProductQuantity(Guid customer, int quantity)
    {
        if (quantity > 20)
        {
            _logger.LogInformation("Customer {Customer} is attempting to purchase more than 20 identical items.", customer);

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
