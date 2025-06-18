using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using System.Collections.Generic; // Required for KeyNotFoundException
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISaleRepository using Entity Framework Core
/// </summary>
public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new Sale in the database
    /// </summary>
    /// <param name="Sale">The Sale to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Sale</returns>
    public async Task<Sale> CreateAsync(Sale Sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(Sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Sale;
    }

    public async Task<List<Sale>> GetAllByCustomerAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _context.Sales.Where(o => o.CustomerId == id).Include(o => o.Products).ToListAsync();
        return result;
    }

    /// <summary>
    /// Retrieves a Sale by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The Sale if found, null otherwise</returns>
    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(s => s.Products) 
            .FirstOrDefaultAsync(o=> o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Deletes a Sale from the database
    /// </summary>
    /// <param name="id">The unique identifier of the Sale to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the Sale was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var Sale = await GetByIdAsync(id, cancellationToken);
        if (Sale == null)
            return false;

        _context.Sales.Remove(Sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> CancelByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await GetByIdAsync(id, cancellationToken);
        if (sale == null)
            return false;

        sale.IsCanceled = true;

        _context.Sales.Update(sale);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Sale> UpdateByAsync(Sale saleInput, CancellationToken cancellationToken = default)
    {
        var existingSale = await _context.Sales
                                         .Include(s => s.Products) 
                                         .FirstOrDefaultAsync(s => s.Id == saleInput.Id, cancellationToken);

        if (existingSale == null)
            throw new KeyNotFoundException($"Sale with ID {saleInput.Id} not found.");

        existingSale.Date = saleInput.Date;
        existingSale.CustomerId = saleInput.CustomerId; 
        existingSale.Branch = saleInput.Branch;
        existingSale.IsCanceled = saleInput.IsCanceled; 
        existingSale.Products.Clear();

        foreach (var inputProduct in saleInput.Products)
        {
            inputProduct.SaleId = existingSale.Id; 
            existingSale.Products.Add(inputProduct);
        }

        existingSale.CalculateTotalSaleAmount(); 
        await _context.SaveChangesAsync(cancellationToken);
        return existingSale;
    }
}
