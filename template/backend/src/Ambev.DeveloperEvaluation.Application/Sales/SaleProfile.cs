using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales;

/// <summary>
/// Profile for mapping between list Sales entity and GetSaleResponse
/// </summary>
public class SaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetSale operation
    /// </summary>
    public SaleProfile()
    {
        CreateMap<Sale, SaleResult>();
        CreateMap<ProductSale, ProductSaleResult>()
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice));
        
        CreateMap<CreateSaleCommand, Sale>();
        CreateMap<CreateProductSale, ProductSale>().ReverseMap();
        CreateMap<Sale, SaleResult>();
        CreateMap<ProductSale, ProductSaleResult>();
        CreateMap<UpdateSaleCommand, Sale>();
        CreateMap<UpdateProductsSaleRequest, ProductSale>();
    }
}
