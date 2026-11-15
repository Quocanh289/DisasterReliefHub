using NetTopologySuite.Geometries;

namespace DisasterRelief.Domain.Entities;

public sealed class Volunteer
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string[] Skills { get; set; } = [];
    public Point? Location { get; set; }
    public bool IsAvailable { get; set; } = true;
    public User? User { get; set; }
}