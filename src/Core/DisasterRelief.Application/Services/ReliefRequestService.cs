using DisasterRelief.Application.DTOs;
using DisasterRelief.Application.Interfaces;
using DisasterRelief.Domain.Entities;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace DisasterRelief.Application.Services;

public sealed class ReliefRequestService(IReliefRequestRepository repository)
{
    private static readonly GeometryFactory GeometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

    public async Task<IReadOnlyList<ReliefRequestDto>> GetAllAsync(CancellationToken cancellationToken)
        => (await repository.GetAllAsync(cancellationToken)).Select(Map).ToArray();

    public async Task<ReliefRequestDto> CreateAsync(CreateReliefRequest request, CancellationToken cancellationToken)
    {
        var entity = new ReliefRequest
        {
            RequesterName = request.RequesterName,
            Phone = request.Phone,
            UrgencyLevel = request.UrgencyLevel,
            Location = GeometryFactory.CreatePoint(new Coordinate(request.Longitude, request.Latitude))
        };
        await repository.AddAsync(entity, cancellationToken);
        return Map(entity);
    }

    private static ReliefRequestDto Map(ReliefRequest request)
        => new(request.Id, request.RequesterName, request.Phone, request.Location, request.UrgencyLevel, request.Status, request.CreatedAt);
}