namespace Ambev.DeveloperEvaluation.WebApi.Features.Cart;

/// <summary>
/// Represents a request to create a new user in the system.
/// </summary>
public class CartRequest
{
    /// <summary>
    /// The id of product
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
}
