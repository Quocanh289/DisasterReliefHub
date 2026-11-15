using NetTopologySuite.Geometries;

namespace DisasterRelief.Domain.Entities;

public sealed class Warehouse
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public Point? Location { get; set; }
    public int Capacity { get; set; }
    public ICollection<Inventory> InventoryItems { get; set; } = new List<Inventory>();
}