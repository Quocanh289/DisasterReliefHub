using DisasterRelief.Application.DTOs;
using DisasterRelief.Application.Interfaces;
using DisasterRelief.Domain.Entities;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace DisasterRelief.Application.Services;

public sealed class WarehouseService(IWarehouseRepository repository)
{
    private static readonly GeometryFactory GeometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

    public async Task<IReadOnlyList<WarehouseDto>> GetAllAsync(CancellationToken cancellationToken)
        => (await repository.GetAllAsync(cancellationToken)).Select(Map).ToArray();

    public async Task<WarehouseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var warehouse = await repository.GetByIdAsync(id, cancellationToken);
        return warehouse is null ? null : Map(warehouse);
    }

    public async Task<WarehouseDto> CreateAsync(CreateWarehouseRequest request, CancellationToken cancellationToken)
    {
        var warehouse = new Warehouse
        {
            Name = request.Name,
            Address = request.Address,
            Capacity = request.Capacity,
            Location = GeometryFactory.CreatePoint(new Coordinate(request.Longitude, request.Latitude))
        };
        await repository.AddAsync(warehouse, cancellationToken);
        return Map(warehouse);
    }

    private static WarehouseDto Map(Warehouse warehouse)
        => new(warehouse.Id, warehouse.Name, warehouse.Address, warehouse.Location, warehouse.Capacity);
}