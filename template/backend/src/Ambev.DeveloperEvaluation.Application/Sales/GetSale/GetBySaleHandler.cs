using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Handler for processing GetSaleById queries.
/// </summary>
public class GetBySaleHandler : IRequestHandler<GetSaleById, SaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    /// <summary>
    /// Initializes a new instance of the <see cref="GetBySaleHandler"/> class.
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public GetBySaleHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<SaleResult> Handle(GetSaleById request, CancellationToken cancellationToken)
    {
        var saleEntity = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        var result = _mapper.Map<SaleResult>(saleEntity);
        return result;
    }
}
