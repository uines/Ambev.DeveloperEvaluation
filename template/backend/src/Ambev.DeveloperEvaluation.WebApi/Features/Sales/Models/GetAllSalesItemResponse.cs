namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Models;

public class GetAllSalesItemResponse
{
    public Guid Id { get; set; }
    public DateTime SaleDate { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalSaleAmount { get; set; }
    public bool IsCancelled { get; set; }
}