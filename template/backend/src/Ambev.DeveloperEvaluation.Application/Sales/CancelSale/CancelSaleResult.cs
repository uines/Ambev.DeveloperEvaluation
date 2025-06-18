namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Represents the response of sale canceled.
/// </summary>
/// <remarks>
/// This response contains the sale canceled,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CancelSaleResult
{

    public Guid Id { get; set; }

    public bool IsCanceled { get; set; }
}