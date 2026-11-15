using NetTopologySuite.Geometries;

namespace DisasterRelief.Application.DTOs;

public sealed record WarehouseDto(Guid Id, string Name, string Address, Point? Location, int Capacity);
public sealed record CreateWarehouseRequest(string Name, string Address, double Longitude, double Latitude, int Capacity);