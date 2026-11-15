using DisasterRelief.Domain.Enums;
using NetTopologySuite.Geometries;

namespace DisasterRelief.Domain.Entities;

public sealed class ReliefRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string RequesterName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public Point? Location { get; set; }
    public UrgencyLevel UrgencyLevel { get; set; } = UrgencyLevel.Medium;
    public ReliefRequestStatus Status { get; set; } = ReliefRequestStatus.Pending;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}