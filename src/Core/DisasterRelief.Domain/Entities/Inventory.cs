namespace DisasterRelief.Domain.Entities;

public sealed class Inventory
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid WarehouseId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int MinimumThreshold { get; set; }
    public Warehouse? Warehouse { get; set; }
}