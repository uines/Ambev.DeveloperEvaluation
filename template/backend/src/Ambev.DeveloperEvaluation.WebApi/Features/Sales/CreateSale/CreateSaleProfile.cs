using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DTO;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Application and API CreateSale responses
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateSale feature
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<ResponseProductSaleDTO, ProductSale>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Quantity * src.UnitPrice));
        CreateMap<CreateSaleCommand, Sale>();
        CreateMap<CreateProductSaleDTO, ProductSale>().ReverseMap();
        CreateMap<Sale, CreateSaleResult>();
        CreateMap<ProductSale, ResponseProductSaleDTO>();
        CreateMap<CreateSaleResult, CreateSaleResponse>();
    }
}
