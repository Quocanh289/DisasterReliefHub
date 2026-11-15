using DisasterRelief.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DisasterRelief.Infrastructure.Persistence;

public sealed class DisasterReliefDbContext(DbContextOptions<DisasterReliefDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<ReliefRequest> ReliefRequests => Set<ReliefRequest>();
    public DbSet<Inventory> Inventory => Set<Inventory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgis");
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(user => user.Id);
            entity.HasIndex(user => user.Email).IsUnique();
            entity.Property(user => user.FullName).HasMaxLength(200).IsRequired();
            entity.Property(user => user.Email).HasMaxLength(320).IsRequired();
            entity.Property(user => user.PasswordHash).IsRequired();
            entity.Property(user => user.Role).HasConversion<string>();
        });
        modelBuilder.Entity<Volunteer>(entity =>
        {
            entity.HasKey(volunteer => volunteer.Id);
            entity.Property(volunteer => volunteer.Location).HasColumnType("geography (point)");
            entity.HasOne(volunteer => volunteer.User).WithOne(user => user.Volunteer).HasForeignKey<Volunteer>(volunteer => volunteer.UserId);
        });
        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(warehouse => warehouse.Id);
            entity.Property(warehouse => warehouse.Name).HasMaxLength(200).IsRequired();
            entity.Property(warehouse => warehouse.Address).HasMaxLength(500).IsRequired();
            entity.Property(warehouse => warehouse.Location).HasColumnType("geography (point)");
        });
        modelBuilder.Entity<ReliefRequest>(entity =>
        {
            entity.HasKey(request => request.Id);
            entity.Property(request => request.Location).HasColumnType("geography (point)");
            entity.Property(request => request.UrgencyLevel).HasConversion<string>();
            entity.Property(request => request.Status).HasConversion<string>();
        });
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(item => item.Id);
            entity.Property(item => item.ItemName).HasMaxLength(200).IsRequired();
            entity.HasOne(item => item.Warehouse).WithMany(warehouse => warehouse.InventoryItems).HasForeignKey(item => item.WarehouseId);
        });
    }
}