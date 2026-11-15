using DisasterRelief.Application.Interfaces;
using DisasterRelief.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DisasterRelief.Infrastructure.Persistence;

internal sealed class WarehouseRepository(DisasterReliefDbContext dbContext) : IWarehouseRepository
{
    public async Task<IReadOnlyList<Warehouse>> GetAllAsync(CancellationToken cancellationToken)
        => await dbContext.Warehouses.AsNoTracking().OrderBy(item => item.Name).ToListAsync(cancellationToken);

    public Task<Warehouse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => dbContext.Warehouses.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id, cancellationToken);

    public async Task AddAsync(Warehouse warehouse, CancellationToken cancellationToken)
    {
        dbContext.Warehouses.Add(warehouse);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

internal sealed class ReliefRequestRepository(DisasterReliefDbContext dbContext) : IReliefRequestRepository
{
    public async Task<IReadOnlyList<ReliefRequest>> GetAllAsync(CancellationToken cancellationToken)
        => await dbContext.ReliefRequests.AsNoTracking().OrderByDescending(item => item.CreatedAt).ToListAsync(cancellationToken);

    public async Task AddAsync(ReliefRequest request, CancellationToken cancellationToken)
    {
        dbContext.ReliefRequests.Add(request);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

internal sealed class UserRepository(DisasterReliefDbContext dbContext) : IUserRepository
{
    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        => dbContext.Users.FirstOrDefaultAsync(user => user.Email == email, cancellationToken);

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}