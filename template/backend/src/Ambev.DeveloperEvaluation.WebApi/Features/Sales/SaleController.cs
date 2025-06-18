using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SaleController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of SaleController
        /// </summary>
        /// <param name="mediator">The mediator instance</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public SaleController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateSaleCommand>(request);
            command.CustomerId = GetCurrentUserId();

            var response = await _mediator.Send(command, cancellationToken);
            var createdResponse = _mapper.Map<CreateSaleResponse>(response);
            return CreatedAtAction(string.Empty, new ApiResponseWithData<CreateSaleResponse>
            {
                Success = true,
                Message = "Sale successfully",
                Data = createdResponse
            });
        }

        /// <summary>
        /// Retrieves all sales
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>All sales</returns>
        [HttpGet("Me")]
        [ProducesResponseType(typeof(ApiResponseWithData<List<SaleResult>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllSales(CancellationToken cancellationToken)
        {
            var command = new GetSaleCommand();
            command.CustomerId = GetCurrentUserId();
            var salesResult  = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<List<SaleResult>>
            {
                Success = true,
                Data = salesResult 
            });
        }

        /// <summary>
        /// Retrieves sale by id
        /// </summary>
        /// <param name="customerId">CustomerId</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>All sales</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponseWithData<SaleResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSaleById(Guid id, CancellationToken cancellationToken)
        {
            var sale = new GetSaleById();
            sale.Id = id;
            var queryResult = await _mediator.Send(sale, cancellationToken);

            if (queryResult == null)
                return NotFound(new ApiResponse { Success = false, Message = "Sale not found." });

            return Ok(new ApiResponseWithData<SaleResult>
            {
                Success = true,
                Data = _mapper.Map<SaleResult>(queryResult)
            });
        }

        [HttpPost("{id:guid}/cancel")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelSale(Guid id, CancellationToken cancellationToken)
        {
            var command = new CancelSaleCommand { Id = id };
            var wasCancelled = await _mediator.Send(command, cancellationToken);
            return Ok(new ApiResponse { Success = true, Message = "Sale cancelled successfully." });
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSale(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new ApiResponseWithData<SaleResult>
            {
                Success = true,
                Data = _mapper.Map<SaleResult>(result)
            });
        }
    }
}
