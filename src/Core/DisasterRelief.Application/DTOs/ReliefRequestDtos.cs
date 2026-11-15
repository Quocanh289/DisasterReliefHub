using DisasterRelief.Domain.Enums;
using NetTopologySuite.Geometries;

namespace DisasterRelief.Application.DTOs;

public sealed record ReliefRequestDto(Guid Id, string RequesterName, string Phone, Point? Location, UrgencyLevel UrgencyLevel, ReliefRequestStatus Status, DateTimeOffset CreatedAt);
public sealed record CreateReliefRequest(string RequesterName, string Phone, double Longitude, double Latitude, UrgencyLevel UrgencyLevel);