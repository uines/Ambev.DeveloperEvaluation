namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Represents a product of sale in the system.
/// </summary>
public class UpdateProductsSaleRequest
{
    /// <summary>{
    /// The customer who purchased
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The id of sale
    /// </summary>
    private Guid SaleId { get; set; }

    /// <summary>
    /// The name of product.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The quantity of product
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The unit price of product
    /// </summary>
    public decimal UnitPrice { get; set; }

    public void SetSaleID(Guid id)
    {
        SaleId = id;
    }
}
