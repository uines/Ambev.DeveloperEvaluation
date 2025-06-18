using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sale entity operations
/// </summary>
public interface ISaleRepository
{
    Task<Sale> CreateAsync(Sale Sale, CancellationToken cancellationToken = default);

    Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<Sale>> GetAllByCustomerAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> CancelByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Sale> UpdateByAsync(Sale sale, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
