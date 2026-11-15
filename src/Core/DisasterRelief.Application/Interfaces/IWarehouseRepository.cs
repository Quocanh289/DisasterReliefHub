using DisasterRelief.Domain.Entities;

namespace DisasterRelief.Application.Interfaces;

public interface IWarehouseRepository
{
    Task<IReadOnlyList<Warehouse>> GetAllAsync(CancellationToken cancellationToken);
    Task<Warehouse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken);
}