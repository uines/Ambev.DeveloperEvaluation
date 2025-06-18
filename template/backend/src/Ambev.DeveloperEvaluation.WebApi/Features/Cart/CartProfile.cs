using Ambev.DeveloperEvaluation.Application.Cart;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart;

/// <summary>
/// Profile for mapping between Application and API CreateSale responses
/// </summary>
public class CartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale feature
    /// </summary>
    public CartProfile()
    {
        CreateMap<CartResult, CartResponse>();
        CreateMap<CartRequest, CartCommand>();
        CreateMap<CartCommand, ProductSale>();
        CreateMap<ProductSale, CartResult>();
    }
}
