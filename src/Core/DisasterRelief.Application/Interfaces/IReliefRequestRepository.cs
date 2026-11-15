using DisasterRelief.Domain.Entities;

namespace DisasterRelief.Application.Interfaces;

public interface IReliefRequestRepository
{
    Task<IReadOnlyList<ReliefRequest>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ReliefRequest request, CancellationToken cancellationToken);
}